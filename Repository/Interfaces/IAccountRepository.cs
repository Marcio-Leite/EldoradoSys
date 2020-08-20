using BaseRepository.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<bool> CheckIfAccountExistsByDescription(string description);
    }
}
