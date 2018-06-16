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
    [RoutePrefix("api/page")]
    public class PageController : ApiControllerBase
    {
        private ILogService _logService;
        private IPageService _pageService;

        public PageController(IErrorService errorService, IPageService pageService, ILogService logService) : base(errorService)
        {
            this._pageService = pageService;
            this._logService = logService;
        }

        #region Create

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PageViewModel model)
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
                    var newPageModel = new Page();
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    newPageModel.UpdatePage(model);
                    newPageModel.CreatedBy = claims.FirstOrDefault().Value;
                    newPageModel.CreatedDate = DateTime.Now;
                    _pageService.Add(newPageModel);
                    _pageService.Save();
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.CREATE_PAGE,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    var responseData = Mapper.Map<Page, PageViewModel>(newPageModel);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        #endregion Create

        #region Update

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, PageViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var oldPageModel = _pageService.GetById(model.ID);
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    oldPageModel.UpdatedBy = claims.FirstOrDefault().Value;
                    oldPageModel.UpdatedDate = DateTime.Now;
                    oldPageModel.UpdatePage(model);
                    _pageService.Update(oldPageModel);
                    _pageService.Save();
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.UPDATE_PAGE,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    var responseData = Mapper.Map<Page, PageViewModel>(oldPageModel);
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
                var oldPage = _pageService.Delete(id);
                _pageService.Save();
                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.DELETE_PAGE,
                    Created = DateTime.Now
                };
                _logService.Create(log);
                _logService.Save();
                var responseData = Mapper.Map<Page, PageViewModel>(oldPage);
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
                var model = _pageService.GetAll();
                if (!string.IsNullOrEmpty(filter))
                {
                    model = model.Where(x => x.Name.Contains(filter) || x.Alias.Contains(filter));
                }
                totalRow = model.Count();
                var query = model.OrderBy(x => x.ID).Skip(page - 1 * pageSize).Take(pageSize).ToList();
                var responseData = Mapper.Map<List<Page>, List<PageViewModel>>(query);
                PaginationSet<PageViewModel> pagedSet = new PaginationSet<PageViewModel>()
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
                var model = _pageService.GetById(id);
                var responseData = Mapper.Map<Page, PageViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        #endregion Get by Id

        #region Search AutoComplete
        [Route("SearchpageByKey")]
        [HttpGet]
        public HttpResponseMessage SearchProductByKey(HttpRequestMessage request, string code)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _pageService.SearchPages(code);
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
        #endregion
    }
}