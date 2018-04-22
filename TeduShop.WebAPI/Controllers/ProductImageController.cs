using AutoMapper;
using System;
using System.Collections.Generic;
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
using System.Linq;

namespace TeduShop.Web.Controllers
{
    [RoutePrefix("api/productImage")]
    public class ProductImageController : ApiControllerBase
    {
        private IProductImageService _productImageService;
        private ILogService _logService;
        public ProductImageController(IProductImageService productImageService, IErrorService errorService, ILogService logService) : base(errorService)
        {
            _productImageService = productImageService;
            _logService = logService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _productImageService.GetAll(productId);

                IEnumerable<ProductImageViewModel> modelVm = Mapper.Map<IEnumerable<ProductImage>, IEnumerable<ProductImageViewModel>>(model);

                response = request.CreateResponse(HttpStatusCode.OK, modelVm);

                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, ProductImageViewModel productImageVm)
        {
            if (ModelState.IsValid)
            {
                var newImage = new ProductImage();
                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                try
                {
                    newImage.UpdateProductImage(productImageVm);

                    _productImageService.Add(newImage);
                    _productImageService.Save();
                    Log log = new Log()
                    {
                        AppUserId = claims.FirstOrDefault().Value,
                        Content = Notification.CREATE_PRODUCTIMG,
                        Created = DateTime.Now
                    };
                    _logService.Create(log);
                    _logService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, productImageVm);
                }
                catch (Exception dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            _productImageService.Delete(id);
            _productImageService.Save();
            Log log = new Log()
            {
                AppUserId = claims.FirstOrDefault().Value,
                Content = Notification.DELETE_PRODUCTIMG,
                Created = DateTime.Now
            };
            _logService.Create(log);
            _logService.Save();
            return request.CreateResponse(HttpStatusCode.OK, id);
        }
    }
}