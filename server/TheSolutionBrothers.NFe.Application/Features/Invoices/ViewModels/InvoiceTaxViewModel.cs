using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels
{
	public class InvoiceTaxViewModel
	{
		public double IcmsValue { get; set; }
		public double Freight { get; set; }
		public double IpiValue { get; set; }
		public double TotalValueProducts { get; set; }
		public double TotalValueInvoice { get; set; }
	}
}
