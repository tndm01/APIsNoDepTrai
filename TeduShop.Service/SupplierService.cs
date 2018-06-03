using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Model;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ISupplierService
    {
        IEnumerable<Supplier> GetAll();
        void Save();
        Supplier GetById(int id);
        Supplier Delete(int id);
        Supplier Add(Supplier Supplier);
        void Update(Supplier Supplier);
        IEnumerable<SupplierMappingModel> SearchSuppliers(string code);
    }
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _SupplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        public SupplierService(ISupplierRepository SupplierRepository, IUnitOfWork unitOfWork)
        {
            _SupplierRepository = SupplierRepository;
            _unitOfWork = unitOfWork;
        }
        public Supplier Add(Supplier Supplier)
        {
            return _SupplierRepository.Add(Supplier);
        }

        public Supplier Delete(int id)
        {
            return _SupplierRepository.Delete(id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _SupplierRepository.GetAll();
        }

        public Supplier GetById(int id)
        {
            return _SupplierRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<SupplierMappingModel> SearchSuppliers(string code)
        {
            return _SupplierRepository.SearchSuppliers(code);
        }

        public void Update(Supplier Supplier)
        {
            _SupplierRepository.Update(Supplier);
        }
    }
}
