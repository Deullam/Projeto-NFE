using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
	public class ReceiverUninformedCompanyNameException : BusinessException
	{
		public ReceiverUninformedCompanyNameException() : base(ErrorCodes.Unauthorized, "Razão social não informada.")
		{
		}
	}
}
