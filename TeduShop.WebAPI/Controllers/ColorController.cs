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
    [RoutePrefix("api/color")]
    [Authorize]
    public class ColorController : ApiControllerBase
    {
        private ILogService _logService;
        private IColorService _colorService;

        public ColorController(IErrorService errorService, ILogService logService, IColorService colorService) : base(errorService)
        {
            _logService = logService;
            _colorService = colorService;
        }

        #region Create

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, ColorViewModel model)
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
                    var newColorModel = new Color();
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    newColorModel.UpdateColor(model);
                    _colorService.Add(newColorModel);
                    _colorService.Save();
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.CREATE_COLOR,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    var responseData = Mapper.Map<Color, ColorViewModel>(newColorModel);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        #endregion Create

        #region Update

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, ColorViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var oldColorModel = _colorService.GetById(model.ID);
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    oldColorModel.UpdateColor(model);
                    _colorService.Update(oldColorModel);
                    _colorService.Save();
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.UPDATE_COLOR,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    var responseData = Mapper.Map<Color, ColorViewModel>(oldColorModel);
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
                var oldColor = _colorService.Delete(id);
                _colorService.Save();
                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.DELETE_COLOR,
                    Created = DateTime.Now
                };
                _logService.Create(log);
                _logService.Save();
                var responseData = Mapper.Map<Color, ColorViewModel>(oldColor);
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
                var model = _colorService.GetAll();
                if (!string.IsNullOrEmpty(filter))
                {
                    model = model.Where(x => x.Name.Contains(filter) || x.ColorCode.Contains(filter));
                }
                totalRow = model.Count();
                var query = model.OrderBy(x => x.ID).Skip(page - 1 * pageSize).Take(pageSize).ToList();
                var responseData = Mapper.Map<List<Color>, List<ColorViewModel>>(query);
                PaginationSet<ColorViewModel> pagedSet = new PaginationSet<ColorViewModel>()
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
                var model = _colorService.GetById(id);
                var responseData = Mapper.Map<Color, ColorViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        #endregion Get by Id
    }
}