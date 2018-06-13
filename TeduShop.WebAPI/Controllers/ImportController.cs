using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using TeduShop.Common.Message;
using TeduShop.Model.Models;
using TeduShop.Service;
using TeduShop.Web.Infrastructure.Core;
using TeduShop.Web.Infrastructure.Extensions;
using TeduShop.Web.Models;

namespace TeduShop.Web.Controllers
{
    [RoutePrefix("api/import")]
    public class ImportController : ApiControllerBase
    {
        private readonly IImportService _importService;
        private readonly IImportDetailService _importDetailService;
        private readonly ILogService _logService;
        public ImportController(IErrorService errorService, 
            IImportService importService, 
            IImportDetailService importDetailService,
            ILogService logService) : base(errorService)
        {
            _importService = importService;
            _importDetailService = importDetailService;
            _logService = logService;
        }

        [Route("add")]
        public HttpResponseMessage Create (HttpRequestMessage request, ImportViewModel importViewModel)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newImportModel = new Import();
                    var newImportDetailModel = new ImportDetail();
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    newImportModel.UpdateImport(importViewModel);
                    _importService.Add(newImportModel);
                    _importService.Save();
                    foreach(var item in importViewModel.ImportDetails)
                    {
                        newImportDetailModel.UpdateImportDetail(item);
                        _importDetailService.Add(newImportDetailModel);
                        _importDetailService.Save();
                    }
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.CREATE_IMPORT,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    var responseData = Mapper.Map<Import, ImportViewModel>(newImportModel);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }
    }
}