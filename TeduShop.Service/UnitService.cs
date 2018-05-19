using System.Collections.Generic;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IUnitService
    {
        IEnumerable<Unit> GetAll();

        void Save();

        Unit GetById(int id);

        Unit Delete(int id);

        Unit Add(Unit Unit);

        void Update(Unit Unit);
    }

    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _UnitRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnitService(IUnitRepository UnitRepository, IUnitOfWork unitOfWork)
        {
            _UnitRepository = UnitRepository;
            _unitOfWork = unitOfWork;
        }

        public Unit Add(Unit Unit)
        {
            return _UnitRepository.Add(Unit);
        }

        public Unit Delete(int id)
        {
            return _UnitRepository.Delete(id);
        }

        public IEnumerable<Unit> GetAll()
        {
            return _UnitRepository.GetAll();
        }

        public Unit GetById(int id)
        {
            return _UnitRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Unit Unit)
        {
            _UnitRepository.Update(Unit);
        }
    }
}