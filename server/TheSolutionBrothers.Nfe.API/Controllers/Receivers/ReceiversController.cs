using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using TheSolutionBrothers.Nfe.API.Controllers.Common;
using TheSolutionBrothers.Nfe.API.Filters;
using TheSolutionBrothers.NFe.Application.Features.Receivers;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Queries;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;

namespace TheSolutionBrothers.Nfe.API.Controllers.Receivers
{
    [RoutePrefix("api/receivers")]
    public class ReceiversController : ApiControllerBase
    {
        private readonly IReceiverService _receiverService;

        public ReceiversController(IReceiverService receiverService) : base()
        {
            _receiverService =  receiverService;
        }

        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Receiver> queryOptions)
        {
            var queryString = Request.GetQueryNameValuePairs()
                                    .Where(x => x.Key.Equals("size"))
                                    .FirstOrDefault();

            var query = default(IQueryable<Receiver>);
            int size = 0;
            if (queryString.Key != null && int.TryParse(queryString.Value, out size))
            {
                query = _receiverService.GetAll(new ReceiverGetAllQuery(size));
            }
            else
                query = _receiverService.GetAll();

            return HandleQueryable<Receiver, ReceiverViewModel>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:long}")]
        public IHttpActionResult GetById(long id)
        {
            return HandleCallback(() => _receiverService.GetById(id));
        }

        [HttpPost]
        public IHttpActionResult Post(ReceiverRegisterCommand command)
        {
            var validator = command.Validate();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _receiverService.Add(command));
        }

        [HttpPut]
        public IHttpActionResult Put(ReceiverUpdateCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
            {
                return HandleValidationFailure(validator.Errors);
            }
            return HandleCallback(() => _receiverService.Update(command));
        }

        [HttpDelete]
        public IHttpActionResult Delete(ReceiverDeleteCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
            {
                return HandleValidationFailure(validator.Errors);
            }
            return HandleCallback(() => _receiverService.Remove(command));
        }
    }
}
