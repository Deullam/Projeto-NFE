using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
	public class ReceiverStateRegistrationLengthOverflowException : BusinessException
	{
		public ReceiverStateRegistrationLengthOverflowException() : base(ErrorCodes.Unauthorized, "A inscrição estadual deve ter no máximo 15 caracteres")
		{
		}
	}
}
