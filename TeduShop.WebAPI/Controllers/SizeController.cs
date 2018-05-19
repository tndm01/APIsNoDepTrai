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
    [RoutePrefix("api/size")]
    [Authorize]
    public class SizeController : ApiControllerBase
    {
        // GET: Size
        private readonly ILogService _logService;
        private readonly ISizeService _sizeService;
        public SizeController(IErrorService errorService, ILogService logService, ISizeService sizeService) : base(errorService)
        {
            _logService = logService;
            _sizeService = sizeService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;

                var model = _sizeService.GetAll();

                if (!string.IsNullOrEmpty(filter))
                {
                    model = model.Where(x => x.Name.ToLower().Contains(filter) || x.Name.ToUpper().Contains(filter));
                }

                totalRow = model.Count();

                var query = model.OrderBy(x => x.ID).Skip(page - 1 * pageSize).Take(pageSize).ToList();

                var responseData = Mapper.Map<List<Size>, List<SizeViewModel>>(query);

                PaginationSet<SizeViewModel> pagedSet = new PaginationSet<SizeViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, SizeViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var identity = (ClaimsIdentity)User.Identity;

                IEnumerable<Claim> claims = identity.Claims;

                var newSizeModel = new Size();

                newSizeModel.UpdateSize(model);

                _sizeService.Add(newSizeModel);

                _sizeService.Save();

                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.CREATE_SIZE,
                    Created = DateTime.Now
                };

                _logService.Create(log);

                _logService.Save();

                var responseData = Mapper.Map<Size, SizeViewModel>(newSizeModel);

                response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, SizeViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                var identity = (ClaimsIdentity)User.Identity;

                IEnumerable<Claim> claims = identity.Claims;

                var newSizeModel = new Size();

                newSizeModel.UpdateSize(model);

                _sizeService.Update(newSizeModel);

                _sizeService.Save();

                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.UPDATE_SIZE,
                    Created = DateTime.Now
                };

                _logService.Create(log);

                _logService.Save();

                var responseData = Mapper.Map<Size, SizeViewModel>(newSizeModel);

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

                var sizeData = _sizeService.Delete(id);

                _sizeService.Save();

                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.DELETE_SIZE,
                    Created = DateTime.Now
                };

                _logService.Create(log);

                _logService.Save();

                var responseData = Mapper.Map<Size, SizeViewModel>(sizeData);

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
                var model = _sizeService.GetById(id);

                var responseData = Mapper.Map<Size, SizeViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }
    }
}