using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using System.Linq;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Queries;

namespace TheSolutionBrothers.NFe.Application.Features.Carriers
{
    public interface ICarrierService
    {

        long Add(CarrierRegisterCommand receiver);
        bool Update(CarrierUpdateCommand receiver);
        CarrierViewModel GetById(long id);
        IQueryable<Carrier> GetAll();
        IQueryable<Carrier> GetAll(CarrierGetAllQuery query);
        bool Remove(CarrierDeleteCommand receiver);

    }
}
