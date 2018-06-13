using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IImportDetailService
    {
        IEnumerable<ImportDetail> GetAll();

        void Save();

        ImportDetail GetById(int id);

        ImportDetail Delete(int id);

        ImportDetail Add(ImportDetail ImportDetail);

        void Update(ImportDetail ImportDetail);
    }

    public class ImportDetailService : IImportDetailService
    {
        private readonly IImportDetailRepository _importDetailRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ImportDetailService(IImportDetailRepository ImportDetailRepository, IUnitOfWork unitOfWork)
        {
            _importDetailRepository = ImportDetailRepository;
            _unitOfWork = unitOfWork;
        }

        public ImportDetail Add(ImportDetail ImportDetail)
        {
            return _importDetailRepository.Add(ImportDetail);
        }

        public ImportDetail Delete(int id)
        {
            return _importDetailRepository.Delete(id);
        }

        public IEnumerable<ImportDetail> GetAll()
        {
            return _importDetailRepository.GetAll();
        }

        public ImportDetail GetById(int id)
        {
            return _importDetailRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ImportDetail ImportDetail)
        {
            _importDetailRepository.Update(ImportDetail);
        }
    }
}