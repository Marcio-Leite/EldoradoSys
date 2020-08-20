using BaseRepository.Interfaces;
using Domain.Identity.Domain;

namespace Repository.Interfaces
{
    public interface IUserRepository  : IRepository<ApplicationUser>
    {
        
    }
}