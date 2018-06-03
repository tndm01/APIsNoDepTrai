using System.Linq;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Model;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface ISizeRepository : IRepository<Size>
    {
        IEnumerable<SizeMappingModel> SearchSizes(string code);
    }

    public class SizeRepository : RepositoryBase<Size>, ISizeRepository
    {
        public SizeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<SizeMappingModel> SearchSizes(string code)
        {
            var result = (from size in DbContext.Sizes.AsNoTracking()
                          where size.Name.ToLower().Contains(code) && size.Name.ToUpper().Contains(code)
                          select new SizeMappingModel
                          {
                              ID = size.ID,
                              Name = size.Name,
                          }).ToList();
            return result;
        }
    }
}