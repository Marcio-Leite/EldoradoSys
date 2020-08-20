// using BaseRepository.BaseRepository;
// using BaseRepository.Persistence;
// using Domain;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Internal;
// using Repository.Interfaces;
// using System;
// using System.Collections.Generic;
// using System.Linq.Dynamic.Core;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace Repository.Repository
// {
//     public class AccountRepository : BaseRepository<Account>, IAccountRepository
//     { 
//         public AccountRepository(PersistenceDbContext context) : base(context)
//         {
//
//         }
//
//         // public Task<bool> CheckIfAccountExistsByDescription(string description)
//         // {
//         //     return Db.Accounts.AnyAsync(x => x.Description == description);
//         // }
//         //
//         // public bool CheckIfAccountExistsByDescriptionTeste(string description)
//         // {
//         //     var account = Db.Accounts.Where("Description = @desc", description).FirstOrDefaultAsync();
//         //
//         //     if (account != null) return true;
//         //     return false;
//         // }
//     }
// }
