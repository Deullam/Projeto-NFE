using System.Linq;


namespace TheSolutionBrothers.NFe.Domain.Features.Senders
{
    public interface ISenderRepository
    {
		IQueryable<Sender> GetAll();
		IQueryable<Sender> GetAll(int size);
		Sender Add(Sender sender);
		bool Update(Sender sender);
		Sender GetById(long senderId);
		bool Remove(long senderId);
	}
}
