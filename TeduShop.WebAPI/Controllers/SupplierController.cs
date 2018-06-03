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
using TeduShop.Web.Models.Supplier;

namespace TeduShop.Web.Controllers
{
    [RoutePrefix("api/supplier")]
    public class SupplierController : ApiControllerBase
    {
        private ILogService _logService;
        private ISupplierService _SupplierService;

        public SupplierController(IErrorService errorService, ILogService logService, ISupplierService SupplierService) : base(errorService)
        {
            _logService = logService;
            _SupplierService = SupplierService;
        }

        #region Create

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, SupplierViewModel model)
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
                    var newSupplierModel = new Supplier();
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    newSupplierModel.UpdateSupplier(model);
                    newSupplierModel.Created = DateTime.Now;
                    _SupplierService.Add(newSupplierModel);
                    _SupplierService.Save();
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.CREATE_SUPPLIER,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    var responseData = Mapper.Map<Supplier, SupplierViewModel>(newSupplierModel);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        #endregion Create

        #region Update

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, SupplierViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var oldSupplierModel = _SupplierService.GetById(model.ID);
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    oldSupplierModel.UpdateSupplier(model);
                    _SupplierService.Update(oldSupplierModel);
                    _SupplierService.Save();
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.UPDATE_SUPPLIER,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    var responseData = Mapper.Map<Supplier, SupplierViewModel>(oldSupplierModel);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        #endregion Update

        #region Delete

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                var oldSupplier = _SupplierService.Delete(id);
                _SupplierService.Save();
                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.UPDATE_SUPPLIER,
                    Created = DateTime.Now
                };
                _logService.Create(log);
                _logService.Save();
                var responseData = Mapper.Map<Supplier, SupplierViewModel>(oldSupplier);
                response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        #endregion Delete

        #region Get All

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var model = _SupplierService.GetAll();
                if (!string.IsNullOrEmpty(filter))
                {
                    model = model.Where(x => x.Name.Contains(filter) || x.Name.Contains(filter));
                }
                totalRow = model.Count();
                var query = model.OrderBy(x => x.ID).Skip(page - 1 * pageSize).Take(pageSize).ToList();
                var responseData = Mapper.Map<List<Supplier>, List<SupplierViewModel>>(query);
                PaginationSet<SupplierViewModel> pagedSet = new PaginationSet<SupplierViewModel>()
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

        #endregion Get All

        #region Get by Id

        [Route("detail/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _SupplierService.GetById(id);
                var responseData = Mapper.Map<Supplier, SupplierViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        #endregion Get by Id

        #region Search AutoComplete
        [Route("SearchSupplierByKey")]
        [HttpGet]
        public HttpResponseMessage SearchProductByKey(HttpRequestMessage request, string code)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _SupplierService.SearchSuppliers(code);
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
        #endregion
    }
}