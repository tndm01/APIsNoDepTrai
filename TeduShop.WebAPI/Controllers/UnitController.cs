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
    [RoutePrefix("api/unit")]
    [Authorize]
    public class UnitController : ApiControllerBase
    {
        // GET: Unit
        private readonly ILogService _logService;

        private readonly IUnitService _UnitService;

        public UnitController(IErrorService errorService, ILogService logService, IUnitService UnitService) : base(errorService)
        {
            _logService = logService;
            _UnitService = UnitService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;

                var model = _UnitService.GetAll();

                if (!string.IsNullOrEmpty(filter))
                {
                    model = model.Where(x => x.Name.Contains(filter));
                }

                totalRow = model.Count();

                var query = model.OrderBy(x => x.UnitId).Skip(page - 1 * pageSize).Take(pageSize).ToList();

                var responseData = Mapper.Map<List<Unit>, List<UnitViewModel>>(query);

                PaginationSet<UnitViewModel> pagedSet = new PaginationSet<UnitViewModel>()
                {
                    PageIndex = page,
                    PageSize = pageSize,
                    TotalRows = totalRow,
                    Items = responseData,
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, UnitViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var identity = (ClaimsIdentity)User.Identity;

                IEnumerable<Claim> claims = identity.Claims;

                var newUnitModel = new Unit();

                newUnitModel.UpdateUnit(model);

                _UnitService.Add(newUnitModel);

                _UnitService.Save();

                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.CREATE_DVT,
                    Created = DateTime.Now
                };

                _logService.Create(log);

                _logService.Save();

                var responseData = Mapper.Map<Unit, UnitViewModel>(newUnitModel);

                response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, UnitViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var identity = (ClaimsIdentity)User.Identity;

                IEnumerable<Claim> claims = identity.Claims;

                var newUnitModel = new Unit();

                newUnitModel.UpdateUnit(model);

                _UnitService.Update(newUnitModel);

                _UnitService.Save();

                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.UPDATE_DVT,
                    Created = DateTime.Now
                };

                _logService.Create(log);

                _logService.Save();

                var responseData = Mapper.Map<Unit, UnitViewModel>(newUnitModel);

                response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var identity = (ClaimsIdentity)User.Identity;

                IEnumerable<Claim> claims = identity.Claims;

                var UnitData = _UnitService.Delete(id);

                _UnitService.Save();

                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.DELETE_DVT,
                    Created = DateTime.Now
                };

                _logService.Create(log);

                _logService.Save();

                var responseData = Mapper.Map<Unit, UnitViewModel>(UnitData);

                response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("detail/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _UnitService.GetById(id);

                var responseData = Mapper.Map<Unit, UnitViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        #region Search AutoComplete
        [Route("SearchUnitByKey")]
        [HttpGet]
        public HttpResponseMessage SearchProductByKey(HttpRequestMessage request, string code)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _UnitService.SearchUnits(code);
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
        #endregion
    }
}