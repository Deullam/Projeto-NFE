using System.Linq;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public interface ICarrierRepository
    {
        IQueryable<Carrier> GetAll();
        IQueryable<Carrier> GetAll(int size);
        Carrier Add(Carrier entity);
        bool Update(Carrier entity);
        Carrier GetById(long entityId);
        bool Remove(long entityId);
    }
}
