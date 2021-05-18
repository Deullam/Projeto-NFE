using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheSolutionBrothers.Nfe.API.Controllers.Common;
using TheSolutionBrothers.Nfe.API.Filters;
using TheSolutionBrothers.NFe.Application.Features.Products;
using TheSolutionBrothers.NFe.Application.Features.Products.Commands;
using TheSolutionBrothers.NFe.Application.Features.Products.Queries;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Products;

namespace TheSolutionBrothers.Nfe.API.Controllers.Products
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiControllerBase
    {
        private readonly IProductService _Productservice;

        public ProductsController(IProductService Productservice) : base()
        {
            _Productservice = Productservice;
        }

        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Product> queryOptions)
        {
            var queryString = Request.GetQueryNameValuePairs()
                                    .Where(x => x.Key.Equals("size"))
                                    .FirstOrDefault();

            var query = default(IQueryable<Product>);
            int size = 0;
            if (queryString.Key != null && int.TryParse(queryString.Value, out size))
            {
                query = _Productservice.GetAll(new ProductGetAllQuery(size));
            }
            else
                query = _Productservice.GetAll();

            return HandleQueryable<Product, ProductViewModel>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:long}")]
        public IHttpActionResult GetById(long id)
        {
            return HandleCallback(() => _Productservice.GetById(id));
        }

        [HttpPost]
        public IHttpActionResult Post(ProductRegisterCommand command)
        {
            var validator = command.Validate();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _Productservice.Add(command));
        }

        [HttpPut]
        public IHttpActionResult Put(ProductUpdateCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
            {
                return HandleValidationFailure(validator.Errors);
            }
            return HandleCallback(() => _Productservice.Update(command));
        }

        [HttpDelete]
        public IHttpActionResult Delete(ProductDeleteCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
            {
                return HandleValidationFailure(validator.Errors);
            }
            return HandleCallback(() => _Productservice.Remove(command));
        }
    }
}
