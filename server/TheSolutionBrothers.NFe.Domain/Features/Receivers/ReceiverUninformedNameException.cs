using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
	public class ReceiverUninformedNameException : BusinessException
	{
		public ReceiverUninformedNameException() : base(ErrorCodes.Unauthorized, "Nome não informado.")
		{
		}
	}
}
