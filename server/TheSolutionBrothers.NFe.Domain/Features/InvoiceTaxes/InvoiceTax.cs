using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes
{
	public class InvoiceTax : Entity
	{
        
        public Invoice Invoice { get; set; }
		public double IcmsValue { get; set; }
		public double Freight { get; set; }
		public double IpiValue { get; set; }
		public double TotalValueProducts { get; set; }
		public double TotalValueInvoice
		{
			get
			{
				return IcmsValue + Freight + IpiValue + TotalValueProducts;
			}
		}

		public override void Validate()
		{
			if(Invoice == null)
			{
				throw new InvoiceTaxNullInvoiceException();
			}

			if (IcmsValue <= 0)
			{
				throw new InvoiceTaxNonPositiveIcmsValueException();
			}

            if (Freight < 0)
			{
				throw new InvoiceTaxNegativeFreightException();
			}

            if (IpiValue <= 0)
			{
				throw new InvoiceTaxNonPositiveIpiValueException();
			}

			if (TotalValueProducts <= 0)
			{
				throw new InvoiceTaxNonPositiveTotalValueProductsException();
			}
		}

		public void CalculateValues(IList<InvoiceItem> invoiceItems)
		{
            IcmsValue = 0;
            IpiValue = 0;
            TotalValueProducts = 0;

            foreach (var invoice in invoiceItems)
            {
                IcmsValue += invoice.IcmsValue;
                IpiValue += invoice.IpiValue;
                TotalValueProducts += invoice.TotalValue;
            }
        }
    }
}
