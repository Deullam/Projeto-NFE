using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers
{
	public class InvoiceReceiverNullInvoiceException : BusinessException
	{
		public InvoiceReceiverNullInvoiceException() : base(ErrorCodes.Unauthorized, "A nota não pode ser nula.")
		{
		}
	}
}
