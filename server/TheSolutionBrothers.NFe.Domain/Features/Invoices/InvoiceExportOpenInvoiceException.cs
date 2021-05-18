using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Invoices
{
	public class InvoiceExportOpenInvoiceException : BusinessException
	{
		public InvoiceExportOpenInvoiceException() : base(ErrorCodes.Unauthorized, "Impossível gerar xml de uma nota não emitida.")
		{
		}
	}
}
