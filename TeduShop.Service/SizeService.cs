using System;
using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Model;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ISizeService
    {
        IEnumerable<Size> GetAll();

        void Save();

        Size GetById(int id);

        Size Delete(int id);

        Size Add(Size Size);

        void Update(Size Size);

        IEnumerable<SizeMappingModel> SearchSizes(string code);
    }

    public class SizeService : ISizeService
    {
        private readonly ISizeRepository _SizeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SizeService(ISizeRepository SizeRepository, IUnitOfWork unitOfWork)
        {
            _SizeRepository = SizeRepository;
            _unitOfWork = unitOfWork;
        }

        public Size Add(Size Size)
        {
            return _SizeRepository.Add(Size);
        }

        public Size Delete(int id)
        {
            return _SizeRepository.Delete(id);
        }

        public IEnumerable<Size> GetAll()
        {
            return _SizeRepository.GetAll();
        }

        public Size GetById(int id)
        {
            return _SizeRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<SizeMappingModel> SearchSizes(string code)
        {
            return _SizeRepository.SearchSizes(code);
        }

        public void Update(Size Size)
        {
            _SizeRepository.Update(Size);
        }
    }
}