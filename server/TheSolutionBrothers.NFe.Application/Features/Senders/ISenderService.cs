using System.Linq;
using TheSolutionBrothers.NFe.Application.Features.Senders.Commands;
using TheSolutionBrothers.NFe.Application.Features.Senders.Queries;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Senders;

namespace TheSolutionBrothers.NFe.Application.Features.Senders
{
    public interface ISenderService
    {
		long Add(SenderRegisterCommand sender);
		bool Update(SenderUpdateCommand sender);
		SenderViewModel GetById(long id);
		IQueryable<Sender> GetAll();
		IQueryable<Sender> GetAll(SenderGetAllQuery query);

		bool Remove(SenderDeleteCommand sender);
	}
}
