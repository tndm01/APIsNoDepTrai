using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IImportService
    {
        IEnumerable<Import> GetAll();

        void Save();

        Import GetById(int id);

        Import Delete(int id);

        Import Add(Import Import);

        void Update(Import Import);
    }

    public class ImportService : IImportService
    {
        private readonly IImportRepository _importRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ImportService(IImportRepository ImportRepository, IUnitOfWork unitOfWork)
        {
            _importRepository = ImportRepository;
            _unitOfWork = unitOfWork;
        }

        public Import Add(Import Import)
        {
            return _importRepository.Add(Import);
        }

        public Import Delete(int id)
        {
            return _importRepository.Delete(id);
        }

        public IEnumerable<Import> GetAll()
        {
            return _importRepository.GetAll();
        }

        public Import GetById(int id)
        {
            return _importRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Import Import)
        {
            _importRepository.Update(Import);
        }
    }
}