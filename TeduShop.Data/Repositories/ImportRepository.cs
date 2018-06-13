using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IImportRepository : IRepository<Import>
    {
    }

    public class ImportRepository : RepositoryBase<Import>, IImportRepository
    {
        public ImportRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}