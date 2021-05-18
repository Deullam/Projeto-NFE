using System.Linq;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Queries;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;

namespace TheSolutionBrothers.NFe.Application.Features.Receivers
{
	public interface IReceiverService
	{
		long Add(ReceiverRegisterCommand receiver);
		bool Update(ReceiverUpdateCommand receiver);
        ReceiverViewModel GetById(long id);
        IQueryable<Receiver> GetAll();
        IQueryable<Receiver> GetAll(ReceiverGetAllQuery query);

        bool Remove (ReceiverDeleteCommand receiver);
	}
}
