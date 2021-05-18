using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
	public class ReceiverUninformedStateRegistrationException : BusinessException
	{
		public ReceiverUninformedStateRegistrationException() : base(ErrorCodes.Unauthorized, "Inscrição estadual não informado.")
		{
		}
	}
}
