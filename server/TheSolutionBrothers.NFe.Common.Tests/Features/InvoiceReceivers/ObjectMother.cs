using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
		public static InvoiceReceiver GetNewValidInvoiceReceiver(Invoice invoice, Receiver receiver)
		{
			return new InvoiceReceiver()
			{
				Invoice = invoice,
				Receiver = receiver
			};
		}

		public static InvoiceReceiver GetExistentValidInvoiceReceiver(Invoice invoice, Receiver receiver)
		{
			return new InvoiceReceiver()
			{
				Id = 1,
				Invoice = invoice,
				Receiver = receiver
			};
		}

		public static InvoiceReceiver GetInvalidInvoiceReceiverNullInvoice(Receiver receiver)
		{
			return new InvoiceReceiver()
			{
				Id = 1,
				Invoice = null,
				Receiver = receiver
			};
		}

		public static InvoiceReceiver GetInvalidInvoiceReceiverNullReceiver(Invoice invoice)
		{
			return new InvoiceReceiver()
			{
				Id = 1,
				Invoice = invoice,
				Receiver = null
			};
		}

	}
}
