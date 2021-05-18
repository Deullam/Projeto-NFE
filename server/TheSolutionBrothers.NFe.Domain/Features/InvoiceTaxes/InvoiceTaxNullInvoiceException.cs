using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers
{
	public class InvoiceTaxNullInvoiceException : BusinessException
	{
		public InvoiceTaxNullInvoiceException() : base(ErrorCodes.Unauthorized, "A nota não pode ser nula.")
		{
		}
	}
}
