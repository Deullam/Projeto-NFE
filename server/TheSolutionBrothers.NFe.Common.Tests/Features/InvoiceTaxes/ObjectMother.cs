using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
	public static partial class ObjectMother
	{
		public static InvoiceTax GetNewValidInvoiceTax(Invoice invoice)
		{
			return new InvoiceTax()
			{
				Invoice = invoice,
				IcmsValue = 10,
				Freight = 0,
				IpiValue = 10,
				TotalValueProducts = 10
			};
		}

		public static InvoiceTax GetExistentValidInvoiceTax(Invoice invoice)
		{
			return new InvoiceTax()
			{
				Id = 1,
				Invoice = invoice,
				IcmsValue = 10,
				Freight = 0,
				IpiValue = 10,
				TotalValueProducts = 10
			};
		}

		public static InvoiceTax GetInvalidInvoiceTaxWithIcmsValueLessThanZero(Invoice invoice)
		{
			return new InvoiceTax()
			{
				Id = 1,
				Invoice = invoice,
				IcmsValue = -10,
				Freight = 0,
				IpiValue = 10,
				TotalValueProducts = 10
			};
		}

		public static InvoiceTax GetInvalidInvoiceTaxWithIcmsValueEqualZero(Invoice invoice)
		{
			return new InvoiceTax()
			{
				Id = 1,
				Invoice = invoice,
				IcmsValue = 0,
				Freight = 0,
				IpiValue = 10,
				TotalValueProducts = 10
			};
		}

		public static InvoiceTax GetInvalidInvoiceTaxWithNegativeFreight(Invoice invoice)
		{
			return new InvoiceTax()
			{
				Id = 1,
				Invoice = invoice,
				IcmsValue = 10,
				Freight = -10,
				IpiValue = 10,
				TotalValueProducts = 10
			};
		}

		public static InvoiceTax GetInvalidInvoiceTaxWithIpiValueLessThanZero(Invoice invoice)
		{
			return new InvoiceTax()
			{
				Id = 1,
				Invoice = invoice,
				IcmsValue = 10,
				Freight = 0,
				IpiValue = -10,
				TotalValueProducts = 10
			};
		}

		public static InvoiceTax GetInvalidInvoiceTaxWithIpiValueEqualZero(Invoice invoice)
		{
			return new InvoiceTax()
			{
				Id = 1,
				Invoice = invoice,
				IcmsValue = 10,
				Freight = 0,
				IpiValue = 0,
				TotalValueProducts = 10
			};
		}

		public static InvoiceTax GetInvalidInvoiceTaxWithTotalValueProductsLessThanZero(Invoice invoice)
		{
			return new InvoiceTax()
			{
				Id = 1,
				Invoice = invoice,
				IcmsValue = 10,
				Freight = 0,
				IpiValue = 10,
				TotalValueProducts = -10
			};
		}

		public static InvoiceTax GetInvalidInvoiceTaxWithTotalValueProductsEqualZero(Invoice invoice)
		{
			return new InvoiceTax()
			{
				Id = 1,
				Invoice = invoice,
				IcmsValue = 10,
				Freight = 10,
				IpiValue = 10,
				TotalValueProducts = 0
			};
		}

		public static InvoiceTax GetInvalidInvoiceTaxWithInvoiceNull()
		{
			return new InvoiceTax()
			{
				Id = 1,
				Invoice = null,
				IcmsValue = 10,
				Freight = 10,
				IpiValue = 10,
				TotalValueProducts = 0
			};
        }

    }
}
