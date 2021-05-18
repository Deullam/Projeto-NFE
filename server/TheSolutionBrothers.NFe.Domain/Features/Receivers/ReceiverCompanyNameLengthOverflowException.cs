using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
	public class ReceiverCompanyNameLengthOverflowException : BusinessException
	{
		public ReceiverCompanyNameLengthOverflowException() : base(ErrorCodes.Unauthorized, "A razão social deve ter no máximo 60 caracteres")
		{
		}
    }
}
