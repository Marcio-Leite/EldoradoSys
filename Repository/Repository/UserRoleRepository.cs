using BaseRepository.BaseRepository;
using BaseRepository.Persistence;
using Domain.Identity.Domain;
using Repository.Interfaces;

namespace Repository.Repository
{
    public class UserRoleRepository: BaseRepository<ApplicationUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(PersistenceDbContext context) : base(context)
        {

        }
    }
}