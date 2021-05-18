using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using System.Linq;
using System.Data.Entity;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Senders
{
	public class SenderRepository : ISenderRepository
	{
		private ContextNfe _context;

		public SenderRepository(ContextNfe context)
		{
			_context = context;
		}

		#region ADD

		public Sender Add(Sender product)
		{
			var newCustomer = _context.Senders.Add(product);
			_context.SaveChanges();
			return newCustomer;
		}

		#endregion

		#region GET
		public IQueryable<Sender> GetAll(int size)
		{
			return this._context.Senders.Include("Address").Take(size);
		}

		public IQueryable<Sender> GetAll()
		{
			return this._context.Senders.Include("Address");
		}

		public Sender GetById(long entityId)
		{
			return _context.Senders.Include("Address").FirstOrDefault(c => c.Id == entityId);
		}
		#endregion

		#region REMOVE
		public bool Remove(long entityId)
		{
			var entity = _context.Senders.FirstOrDefault(p => p.Id == entityId);
			if (entity == null)
				throw new NotFoundException();
			_context.Entry(entity).State = EntityState.Deleted;
			return _context.SaveChanges() > 0;
		}
		#endregion

		#region UPDATE

		public bool Update(Sender sender)
		{
			_context.Entry(sender).State = EntityState.Modified;
			return _context.SaveChanges() > 0;
		}

		#endregion
	}
}
