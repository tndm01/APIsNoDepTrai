using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TeduShop.Model.Models;
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
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _logService.GetAll();
                IEnumerable<LogViewModel> modelVm = Mapper.Map<IEnumerable<Log>, IEnumerable<LogViewModel>>(model);
                response = request.CreateResponse(HttpStatusCode.OK, modelVm);
                return response;
            });
        }
        // GET: Log
    }
}