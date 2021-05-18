using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheSolutionBrothers.Nfe.API.Controllers.Common;
using TheSolutionBrothers.Nfe.API.Filters;
using TheSolutionBrothers.NFe.Application.Features.Invoices;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Commands;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Queries;
using TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.Nfe.API.Controllers.Invoices
{

    [RoutePrefix("api/invoices")]
    public class InvoicesController : ApiControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService) : base()
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Invoice> queryOptions)
        {
            var queryString = Request.GetQueryNameValuePairs()
                                    .Where(x => x.Key.Equals("size"))
                                    .FirstOrDefault();

            var query = default(IQueryable<Invoice>);
            int size = 0;
            if (queryString.Key != null && int.TryParse(queryString.Value, out size))
            {
                query = _invoiceService.GetAll(new InvoiceGetAllQuery(size));
            }
            else
                query = _invoiceService.GetAll();
            var teste = HandleQueryable<Invoice, InvoiceViewModel>(query, queryOptions);
            return teste;
        }

        [HttpGet]
        [Route("{id:long}")]
        public IHttpActionResult GetById(long id)
        {
            return HandleCallback(() => _invoiceService.GetById(id));
        }

        [HttpPost]
        public IHttpActionResult Post(InvoiceRegisterCommand command)
        {
            var validator = command.Validate();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _invoiceService.Add(command));
        }

        [HttpPut]
        public IHttpActionResult Put(InvoiceUpdateCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
            {
                return HandleValidationFailure(validator.Errors);
            }
            return HandleCallback(() => _invoiceService.Update(command));
        }

        [HttpDelete]
        public IHttpActionResult Delete(InvoiceDeleteCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
            {
                return HandleValidationFailure(validator.Errors);
            }
            return HandleCallback(() => _invoiceService.Remove(command));
        }
        
        [HttpPost]
        [Route("{id:long}/items")]
        public IHttpActionResult PostItem(long id, [FromBody] List<InvoiceItemRegisterCommand> commands)
        {
            foreach (InvoiceItemRegisterCommand command in commands)
            {
                var validator = command.Validate();
                if (!validator.IsValid)
                    return HandleValidationFailure(validator.Errors);
            }

            return HandleCallback(() => _invoiceService.AddItems(id, commands));
        }

        [HttpPut]
        [Route("{id:long}/items")]
        public IHttpActionResult PutItem(long id, [FromBody] List<InvoiceItemUpdateCommand> commands)
        {
            foreach (InvoiceItemUpdateCommand command in commands)
            {
                var validator = command.Validate();
                if (!validator.IsValid)
                    return HandleValidationFailure(validator.Errors);
            }

            return HandleCallback(() => _invoiceService.UpdateItems(id, commands));
        }

        [HttpDelete]
        [Route("{id:long}/items")]
        public IHttpActionResult DeleteItem(long id, [FromBody] InvoiceItemDeleteCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
            {
                return HandleValidationFailure(validator.Errors);
            }

            return HandleCallback(() => _invoiceService.DeleteItems(id, command));
        }
    }

}
