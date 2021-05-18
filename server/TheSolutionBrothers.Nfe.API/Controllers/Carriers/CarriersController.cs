using Microsoft.AspNet.OData.Query;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using TheSolutionBrothers.Nfe.API.Controllers.Common;
using TheSolutionBrothers.Nfe.API.Filters;
using TheSolutionBrothers.NFe.Application.Features.Carriers;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Queries;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;

namespace TheSolutionBrothers.Nfe.API.Controllers.Carriers
{

    [RoutePrefix("api/carriers")]
    public class CarriersController : ApiControllerBase
    {
        private readonly ICarrierService _carrierService;

        public CarriersController(ICarrierService carrierService) : base()
        {
            _carrierService = carrierService;
        }

        [HttpGet]
        [ODataQueryOptionsValidate]
        public IHttpActionResult Get(ODataQueryOptions<Carrier> queryOptions)
        {
            var queryString = Request.GetQueryNameValuePairs()
                                    .Where(x => x.Key.Equals("size"))
                                    .FirstOrDefault();

            var query = default(IQueryable<Carrier>);
            int size = 0;
            if (queryString.Key != null && int.TryParse(queryString.Value, out size))
            {
                query = _carrierService.GetAll(new CarrierGetAllQuery(size));
            }
            else
                query = _carrierService.GetAll();

            var teste = query.ToList();
            return HandleQueryable<Carrier, CarrierViewModel>(query, queryOptions);
        }

        [HttpGet]
        [Route("{id:long}")]
        public IHttpActionResult GetById(long id)
        {
            return HandleCallback(() => _carrierService.GetById(id));
        }

        [HttpPost]
        public IHttpActionResult Post(CarrierRegisterCommand command)
        {
            var validator = command.Validate();
            if (!validator.IsValid)
                return HandleValidationFailure(validator.Errors);

            return HandleCallback(() => _carrierService.Add(command));
        }

        [HttpPut]
        public IHttpActionResult Put(CarrierUpdateCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
            {
                return HandleValidationFailure(validator.Errors);
            }
            return HandleCallback(() => _carrierService.Update(command));
        }

        [HttpDelete]
        public IHttpActionResult Delete(CarrierDeleteCommand command)
        {
            var validator = command.Validate();

            if (!validator.IsValid)
            {
                return HandleValidationFailure(validator.Errors);
            }
            return HandleCallback(() => _carrierService.Remove(command));
        }
    }

}
