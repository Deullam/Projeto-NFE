using Microsoft.AspNet.OData.Query;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using TheSolutionBrothers.Nfe.API.Controllers.Common;
using TheSolutionBrothers.Nfe.API.Filters;
using TheSolutionBrothers.NFe.Application.Features.Senders;
using TheSolutionBrothers.NFe.Application.Features.Senders.Commands;
using TheSolutionBrothers.NFe.Application.Features.Senders.Queries;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Senders;

namespace TheSolutionBrothers.Nfe.API.Controllers.Senders
{
	[RoutePrefix("api/senders")]
	public class SendersController : ApiControllerBase
	{
		private readonly ISenderService _senderService;

		public SendersController(ISenderService senderService) : base()
		{
			_senderService = senderService;
		}

		[HttpGet]
		[ODataQueryOptionsValidate]
		public IHttpActionResult Get(ODataQueryOptions<Sender> queryOptions)
		{
			var queryString = Request.GetQueryNameValuePairs()
									.Where(x => x.Key.Equals("size"))
									.FirstOrDefault();

			var query = default(IQueryable<Sender>);
			int size = 0;
			if (queryString.Key != null && int.TryParse(queryString.Value, out size))
			{
				query = _senderService.GetAll(new SenderGetAllQuery(size));
			}
			else
				query = _senderService.GetAll();

			return HandleQueryable<Sender, SenderViewModel>(query, queryOptions);
		}

		[HttpGet]
		[Route("{id:long}")]
		public IHttpActionResult GetById(long id)
		{
			return HandleCallback(() => _senderService.GetById(id));
		}

		[HttpPost]
		public IHttpActionResult Post(SenderRegisterCommand command)
		{
			var validator = command.Validate();
			if (!validator.IsValid)
				return HandleValidationFailure(validator.Errors);

			return HandleCallback(() => _senderService.Add(command));
		}

		[HttpPut]
		public IHttpActionResult Put(SenderUpdateCommand command)
		{
			var validator = command.Validate();

			if (!validator.IsValid)
			{
				return HandleValidationFailure(validator.Errors);
			}
			return HandleCallback(() => _senderService.Update(command));
		}

		[HttpDelete]
		public IHttpActionResult Delete(SenderDeleteCommand command)
		{
			var validator = command.Validate();

			if (!validator.IsValid)
			{
				return HandleValidationFailure(validator.Errors);
			}
			return HandleCallback(() => _senderService.Remove(command));
		}
	}
}