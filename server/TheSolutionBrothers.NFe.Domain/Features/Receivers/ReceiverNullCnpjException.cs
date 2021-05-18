using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
	public class ReceiverNullCnpjException : BusinessException
	{
		public ReceiverNullCnpjException() : base(ErrorCodes.Unauthorized, "Cnpj não informado.")
		{
		}
	}
}
