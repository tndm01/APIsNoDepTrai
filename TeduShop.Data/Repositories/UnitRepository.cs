using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Model;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IUnitRepository : IRepository<Unit>
    {
        IEnumerable<UnitMappingModel> SearchUnits(string code);
    }

    public class UnitRepository : RepositoryBase<Unit>, IUnitRepository
    {
        public UnitRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<UnitMappingModel> SearchUnits(string code)
        {
            var result = (from unit in DbContext.Units.AsNoTracking()
                          where unit.Name.ToLower().Contains(code) && unit.Name.ToUpper().Contains(code)
                          select new UnitMappingModel
                          {
                              UnitId = unit.UnitId,
                              Name = unit.Name,
                              UnitCode = unit.UnitCode,
                              Description = unit.Description
                          }).ToList();
            return result;
        }
    }
}