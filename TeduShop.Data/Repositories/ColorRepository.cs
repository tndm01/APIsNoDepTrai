using System.Collections.Generic;
using System.Linq;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Model;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface IColorRepository : IRepository<Color>
    {
        IEnumerable<ColorMappingModel> SearchColors(string code);
    }

    public class ColorRepository : RepositoryBase<Color>, IColorRepository
    {
        public ColorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<ColorMappingModel> SearchColors(string code)
        {
            var result = (from color in DbContext.Colors.AsNoTracking()
                          where color.Name.ToLower().Contains(code) && color.Name.ToUpper().Contains(code)
                          select new ColorMappingModel
                          {
                              ID = color.ID,
                              Name = color.Name,
                              ColorCode = color.ColorCode
                          }).ToList();
            return result;
        }
    }
}