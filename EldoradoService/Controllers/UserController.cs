using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Identity;
using Domain.Identity.Domain;
using EldoradoService.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Repository.Interfaces;
using Repository.UnitOfWork;
using ServiceStack;
using PasswordHasher = ServiceStack.Auth.PasswordHasher;
using Register = Domain.Identity.Register;

namespace EldoradoService.Controllers
{

[Microsoft.AspNetCore.Mvc.Route("identity_server")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private PasswordHasher hash = new PasswordHasher();
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IUserRoleRepository _userRoleRepository;
        private readonly IUnitOfWork _uow;
        
        public UserController(IConfiguration configuration, IUnitOfWork unitOfWork, IUserRepository userRepository, 
            IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            _configuration = configuration;
            _uow = unitOfWork;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Login([FromBody]Register.LoginEntity user)
        {
            bool needRehash;
            var userDbSearch = await _userRepository.Query("UserName = @Email", new {Email = user.Email});

            var userDb = userDbSearch.FirstOrDefault();
            bool passwordIsCorrect = hash.VerifyPassword(userDb.Password, user.Password, out needRehash);
            
            if (!userDbSearch.Any() || !passwordIsCorrect)
                return NotFound(new { message = "Usuário ou senha inválidos" });
            
            var userRolesDb = (await _userRoleRepository.Query("ApplicationUserId = @ApplicationUserId", new {ApplicationUserId = userDb.ApplicationUserId})).ToList();
            var roles = new List<ApplicationRole>();
            foreach (var rolesFind in userRolesDb)
            {
                var roleDb = await _roleRepository.GetById(rolesFind.ApplicationRoleId);
                roles.Add(roleDb);
            }

            var token = TokenService.GenerateToken(userDb, roles);
            var expirationTime = DateTime.Now.AddHours(HashingOptions.ExpirationInHours);
            user.Password = null;

            return Ok(new LoginResponse(userDb.ApplicationUserId, userDb.Email, userDb.UserName, userDb.Name, userDb.LastName,
                roles, token, expirationTime.ToString()));
        }
        
        [HttpGet]
        [Authorize]
        [Microsoft.AspNetCore.Mvc.Route("user_data")]
        public async Task<ActionResult<dynamic>> UserData()
        {
            var userId = User.Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid").FirstOrDefault();
            var expirationTime = User.Claims.Where(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/expiration").FirstOrDefault();
            //var expirationDate = new DateTime(Convert.ToInt64(exp)*1000); //unix timestamp is in seconds, javascript in milliseconds
            
            if (userId == null || expirationTime == null) return NotFound(new { message = "Token informado inválido" });
            
            var userDb = (await _userRepository.Query("id = @UserId", new {UsrId = userId.Value})).FirstOrDefault();
            
            var userRolesDb = await _userRoleRepository.Query("ApplicationUserId = @ApplicationUserId", 
                new {ApplicationUserId = userDb.ApplicationUserId});
            var roles = new List<ApplicationRole>();
            foreach (var rolesFind in userRolesDb)
            {
                var roleDb = await _roleRepository.GetById(rolesFind.ApplicationRoleId);
                roles.Add(roleDb);
            }
            
            return new LoginResponse(userDb.ApplicationUserId, userDb.Email, userDb.UserName, userDb.Name, userDb.LastName,
                roles, "", expirationTime.Value);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Microsoft.AspNetCore.Mvc.Route("role")]
        public async  Task<IActionResult> AddRoles([FromBody] Role role)
        {
            var newRole = new ApplicationRole(role.Name);

            var roleExists =
                await _roleRepository.Query("name = @Name", new {Name = role.Name});

            if (roleExists.Any())
                return BadRequest("Role " + role.Name + " já registrado.");
            
            _roleRepository.Add(newRole);
            
            bool result = await _uow.Commit();
            if (result)
                return Ok(newRole);
            
            return BadRequest("Ocorreu um erro ao gravar no banco de dados.");
        }

        [HttpPut]
        [Authorize(Roles="Admin")]
        [Microsoft.AspNetCore.Mvc.Route("role_to_user")]
        public async  Task<IActionResult> AddRolesToUser([FromBody] RoleToUser roleToUser)
        {
            var role = await _roleRepository.GetById(roleToUser.RoleId);
            var user = await _userRepository.GetById(roleToUser.UserId);
            
            if (role == null || user == null)
                return BadRequest("Ids informados incorretos.");

            var verifyExists = (await _userRoleRepository.Query(
                    "ApplicationUserId = @ApplicationUserId and ApplicationRoleId = @ApplicationRoleId", 
                    new {ApplicationUserId = user.ApplicationUserId, ApplicationRoleId = role.ApplicationRoleId}))
                .Any();
            
            if (verifyExists)
                return BadRequest("Usuário já tem essa Role");
            
            _userRoleRepository.Add(new ApplicationUserRole(user, role));
            
            _userRepository.Update(user);

            bool result = await _uow.Commit();
            if (result)
                return Ok("Role "+ role.Name + " adicionada para o usuário " + user.UserName);
            
            return BadRequest("Ocorreu um erro ao gravar no banco de dados.");
        }
        
        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("is_authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);
        
        // POST api/user/register
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDb = new ApplicationUser(
                        model.Email, model.Email, model.Name, model.LastName,
                        hash.HashPassword(model.Password), null
                    );

                    var userExists =
                        //await _uow.UserRepository.Query($"UserName = '{model.Email}'");
                        await _userRepository.Query("UserName = @Email", new {Email = model.Email});

                    if (userExists.Any())
                        return BadRequest("E-mail " + model.Email + " já registrado.");

                    var defaultRole = _configuration.GetValue<string>("DefaultRole");
                    var roleDb = await _roleRepository.Query("Name = @Name", new {Name = defaultRole});
                    _userRepository.Add(userDb);
                    _userRoleRepository.Add(new ApplicationUserRole(userDb, roleDb.FirstOrDefault()));


                    if (! await _uow.Commit()) return BadRequest("Ocorreu um erro ao gravar no banco de dados.");
                    var token = TokenService.GenerateToken(userDb, roleDb.ToList());
                    var expirationTime = DateTime.Now.AddHours(HashingOptions.ExpirationInHours);

                    return Ok(new LoginResponse(userDb.ApplicationUserId, userDb.Email, userDb.UserName, userDb.Name,
                        userDb.LastName,
                        roleDb.ToList(), token, expirationTime.ToString()));
                }

                string errorMessage =
                    string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                return BadRequest(errorMessage ?? "Bad Request");
            }
            catch (Exception e)
            {
                return BadRequest("Erro: "+ e.Message);
            }
        }
    }
}