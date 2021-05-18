using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers
{
	public class InvoiceReceiverNullReceiverException : BusinessException
	{
		public InvoiceReceiverNullReceiverException() : base(ErrorCodes.Unauthorized, "O destinatário não pode ser nulo.")
		{
		}
	}
}
