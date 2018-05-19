using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Models.Log;

namespace TeduShop.Web.Controllers
{
    [RoutePrefix("api/log")]
    public class LogController : ApiControllerBase
    {
        private ILogService _logService;

        public LogController(IErrorService errorService, ILogService logService) : base(errorService)
        {
            _logService = logService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _logService.GetAllDataByUserName();
                if (!string.IsNullOrWhiteSpace(filter))
                    model = model.Where(x => x.UserName.Contains(filter) || x.Content.Contains(filter));
                totalRow = model.Count();
                var modelVm = model.OrderBy(x => x.Content).Skip((page - 1) * pageSize).Take(pageSize);
                PaginationSet<LogViewModel> pagedSet = new PaginationSet<LogViewModel>()
                {
                    PageIndex = page,
                    PageSize = pageSize,
                    TotalRows = totalRow,
                    Items = modelVm,
                };
                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);
                return response;
            });
        }

        // GET: Log
    }
}