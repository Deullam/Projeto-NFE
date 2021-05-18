using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
	public class ReceiverNameLengthOverflowException : BusinessException
	{
		public ReceiverNameLengthOverflowException() : base(ErrorCodes.Unauthorized, "O nome deve ter no máximo 60 caracteres")
		{
		}
	}
}
