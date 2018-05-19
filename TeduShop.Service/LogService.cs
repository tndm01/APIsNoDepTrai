using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Data.Repositories;
using TeduShop.Model.Models;
using TeduShop.Web.Models.Log;

namespace TeduShop.Service
{
    public interface ILogService
    {
        Log Create(Log log);
        IEnumerable<Log> GetAll();
        IEnumerable<LogViewModel> GetAllDataByUserName();
        void Save();
    }
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IUnitOfWork _unitOfWork;
        public LogService(ILogRepository logRepository, IUnitOfWork unitOfWork)
        {
            _logRepository = logRepository;
            _unitOfWork = unitOfWork;
        }

        public Log Create(Log log)
        {
            return _logRepository.Add(log);
        }

        public IEnumerable<Log> GetAll()
        {
            return _logRepository.GetAll();
        }

        public IEnumerable<LogViewModel> GetAllDataByUserName()
        {
            return _logRepository.GetDataLogByUserId();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
