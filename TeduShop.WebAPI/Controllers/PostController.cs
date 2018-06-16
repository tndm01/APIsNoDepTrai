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
    [RoutePrefix("api/post")]
    public class PostController : ApiControllerBase
    {
        private ILogService _logService;
        private IPostService _postService;
        public PostController(IErrorService errorService, ILogService logService, IPostService postService) : base(errorService)
        {
            this._logService = logService;
            this._postService = postService;
        }

        #region Create

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PostViewModel model)
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
                    var newPostModel = new Post();
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    newPostModel.UpdatePost(model);
                    newPostModel.CreatedBy = claims.FirstOrDefault().Value;
                    newPostModel.CreatedDate = DateTime.Now;
                    _postService.Add(newPostModel);
                    _postService.SaveChanges();
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.CREATE_POST,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    var responseData = Mapper.Map<Post, PostViewModel>(newPostModel);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return response;
            });
        }

        #endregion Create

        #region Update

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, PostViewModel model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var oldPostModel = _postService.GetById(model.ID);
                    var identity = (ClaimsIdentity)User.Identity;
                    IEnumerable<Claim> claims = identity.Claims;
                    oldPostModel.UpdatedBy = claims.FirstOrDefault().Value;
                    oldPostModel.UpdatedDate = DateTime.Now;
                    oldPostModel.UpdatePost(model);
                    _postService.Update(oldPostModel);
                    _postService.SaveChanges();
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.UPDATE_POST,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    var responseData = Mapper.Map<Post, PostViewModel>(oldPostModel);
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
                var oldPost = _postService.Delete(id);
                _postService.SaveChanges();
                Log log = new Log()
                {
                    AppUserId = claims.FirstOrDefault().Value,
                    Content = Notification.DELETE_POST,
                    Created = DateTime.Now
                };
                _logService.Create(log);
                _logService.Save();
                var responseData = Mapper.Map<Post, PostViewModel>(oldPost);
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
                var model = _postService.GetAll();
                if (!string.IsNullOrEmpty(filter))
                {
                    model = model.Where(x => x.Name.Contains(filter));
                }
                totalRow = model.Count();
                var query = model.OrderBy(x => x.ID).Skip(page - 1 * pageSize).Take(pageSize).ToList();
                var responseData = Mapper.Map<List<Post>, List<PostViewModel>>(query);
                PaginationSet<PostViewModel> pagedSet = new PaginationSet<PostViewModel>()
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
                var model = _postService.GetById(id);
                var responseData = Mapper.Map<Post, PostViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        #endregion Get by Id
    }
}