using BaseRepository.BaseRepository;
using BaseRepository.Persistence;
using Domain;
using Domain.Identity.Domain;
using Repository.Interfaces;

namespace Repository.Repository
{
    public class UserRepository : BaseRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(PersistenceDbContext context) : base(context)
        {

        }
    }
}