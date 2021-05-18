using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using System.Linq;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using System.Data.Entity;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Receivers
{
    public class ReceiverRepository : IReceiverRepository
    {
        private ContextNfe _context;

        public ReceiverRepository(ContextNfe context)
        {
            _context = context;
        }

        #region ADD

        public Receiver Add(Receiver entity)
        {
            var newReceiver = _context.Receivers.Add(entity);
            _context.SaveChanges();
            return newReceiver;
        }

        #endregion

        #region GET
        public IQueryable<Receiver> GetAll(int size)
        {
            return this._context.Receivers.Include("Address").Take(size);
        }

        public IQueryable<Receiver> GetAll()
        {
            return this._context.Receivers.Include("Address");
        }

        public Receiver GetById(long entityid)
        {
            return _context.Receivers.Include("Address").FirstOrDefault(c => c.Id == entityid);
        }
        #endregion

        #region REMOVE
        public bool Remove(long entityId)
        {
            var receiver = _context.Receivers.FirstOrDefault(p => p.Id == entityId);
            if (receiver == null)
                throw new NotFoundException();
            _context.Entry(receiver).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }
        #endregion

        #region UPDATE

        public bool Update(Receiver receiver)
        {
            _context.Entry(receiver).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        #endregion
    }
}
