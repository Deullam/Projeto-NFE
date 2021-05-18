using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using System.Linq;
using System.Data.Entity;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Carriers
{
    public class CarrierRepository : ICarrierRepository
    {

        private ContextNfe _context;

        public CarrierRepository(ContextNfe context)
        {
            _context = context;
        }

        #region ADD

        public Carrier Add(Carrier product)
        {
            var newCustomer = _context.Carriers.Add(product);
            _context.SaveChanges();
            return newCustomer;
        }

        #endregion

        #region GET
        public IQueryable<Carrier> GetAll(int size)
        {
            return this._context.Carriers.Include("Address").Take(size);
        }

        public IQueryable<Carrier> GetAll()
        {
            return this._context.Carriers.Include("Address");
        }

        public Carrier GetById(long productId)
        {
            return _context.Carriers.Include("Address").FirstOrDefault(c => c.Id == productId);
        }
        #endregion

        #region REMOVE
        public bool Remove(long entityId)
        {
            var entity = _context.Carriers.FirstOrDefault(p => p.Id == entityId);
            if (entity == null)
                throw new NotFoundException();
            _context.Entry(entity).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }
        #endregion

        #region UPDATE

        public bool Update(Carrier product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        #endregion

    }
}
