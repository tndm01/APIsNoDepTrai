using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Model;
using TeduShop.Model.Models;

namespace TeduShop.Data.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        IEnumerable<SupplierMappingModel> SearchSuppliers(string code);
    }

    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
        public IEnumerable<SupplierMappingModel> SearchSuppliers(string code)
        {
            var result = (from supplier in DbContext.Suppliers.AsNoTracking()
                          where supplier.Name.ToLower().Contains(code) && supplier.Name.ToUpper().Contains(code)
                          select new SupplierMappingModel
                          {
                              ID = supplier.ID,
                              Name = supplier.Name,
                              Address = supplier.Address,
                              Created = supplier.Created,
                              Email = supplier.Email,
                              Mobile = supplier.Mobile,
                              Note = supplier.Note,
                              Phone = supplier.Phone,
                              Status = supplier.Status,
                              Tax = supplier.Tax
                          }).ToList();
            return result;
        }
    }
}
