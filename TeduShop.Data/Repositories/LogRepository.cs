using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Infrastructure;
using TeduShop.Model.Models;
using TeduShop.Web.Models.Log;

namespace TeduShop.Data.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        IEnumerable<LogViewModel> GetDataLogByUserId();
    }
    public class LogRepository: RepositoryBase<Log>, ILogRepository
    {
        public LogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
            
        }

        public IEnumerable<LogViewModel> GetDataLogByUserId()
        {
            // user = new AppUser();
            var result = (from logs in DbContext.Logs
                          join user in DbContext.Users
                          on logs.AppUserId equals user.Id
                          select new LogViewModel()
                          {
                              Content = logs.Content,
                              Created = logs.Created,
                              UserName = user.FullName,
                              Img = user.Avatar
                          }).ToList();
            return result;
        }
    }
}
