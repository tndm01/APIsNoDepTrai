using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IImportDetailRepository : IRepository<ImportDetail>
    {

    }
    public class ImportDetailRepository : RepositoryBase<ImportDetail>, IImportDetailRepository
    {
        public ImportDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
