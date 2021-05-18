using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using TheSolutionBrothers.Nfe.API.Exceptions;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.IoC.Features.Containers;

namespace TheSolutionBrothers.Nfe.API.Controllers.Common
{
    public class ApiControllerBase : ApiController
    {

        private IMapper _mapper = Mapper.Instance;

        protected IHttpActionResult HandleCallback<TSuccess>(Func<TSuccess> func)
        {
            try
            {
                return Ok(func());
            }
            catch (Exception e)
            {
                return HandleFailure(e);
            }
        }

        protected IHttpActionResult HandleQuery<TResult>(IQueryable<TResult> query)
        {
            return Ok(query.ToList());
        }


        protected IHttpActionResult HandleQueryable<TSource>(IQueryable<TSource> query)
        {
            return Ok(query.ToList());
        }

        protected IHttpActionResult HandleFailure<T>(T exceptionToHandle) where T : Exception
        {
            var exceptionPayload = ExceptionPayload.New(exceptionToHandle);
            return exceptionToHandle is BusinessException ?
                Content(HttpStatusCode.BadRequest, exceptionPayload) :
                Content(HttpStatusCode.InternalServerError, exceptionPayload);
        }

        protected IHttpActionResult HandleValidationFailure<T>(IList<T> validationFailure) where T : ValidationFailure
        {
            return Content(HttpStatusCode.BadRequest, validationFailure);
        }

        protected IHttpActionResult HandleQueryable<TSource, TResult>(IQueryable<TSource> query, ODataQueryOptions<TSource> queryOptions)
        {
            // Fazemos .ToList para obter os dados antes de converter (precisamos dos dados para conversão)
            // Após isso, é obtido um queryable dos resultdos convertendo para o tipo principal. Não há mais operação no banco.
            var odataQuery = queryOptions.ApplyTo(query).Cast<TSource>();
            var dataQuery = odataQuery.ToList().AsQueryable().ProjectTo<TResult>();
            //dataQuery.Expression.Argume
            var pageResult = new PageResult<TResult>(dataQuery,
                                    Request.ODataProperties().NextLink,
                                    Request.ODataProperties().TotalCount);
            // Esse .ToList() é performado no ProjectTo e não mais no EF
            return Ok(pageResult);
        }

    }
}