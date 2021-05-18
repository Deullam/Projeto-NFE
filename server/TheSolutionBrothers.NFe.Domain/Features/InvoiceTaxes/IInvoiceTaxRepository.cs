using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes
{
	public interface IInvoiceTaxRepository
	{
		InvoiceTax Add(InvoiceTax entity);
		InvoiceTax GetByInvoice(long id);
	}
}
