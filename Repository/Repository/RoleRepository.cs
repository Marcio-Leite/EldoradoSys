using BaseRepository.BaseRepository;
using BaseRepository.Persistence;
using Domain.Identity.Domain;
using Repository.Interfaces;

namespace Repository.Repository
{
    public class RoleRepository: BaseRepository<ApplicationRole>, IRoleRepository
    {
        public RoleRepository(PersistenceDbContext context) : base(context)
        {

        }
    }
}