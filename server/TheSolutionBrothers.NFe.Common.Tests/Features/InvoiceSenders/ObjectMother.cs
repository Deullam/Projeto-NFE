using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static InvoiceSender GetNewInvoiceSenderOk(Invoice invoice, Sender sender)
        {
            return new InvoiceSender
            {
                Invoice = invoice,
                Sender = sender
            };
        }

        public static InvoiceSender GetExistentInvoinceSenderOk(Invoice invoice, Sender sender)
        {
            return new InvoiceSender
            {
                Id = 1,
                Invoice = invoice,
                Sender = sender
            };
        }

        public static InvoiceSender GetInvalidInvoiceSenderWithInvoiceNull(Sender sender)
        {
            return new InvoiceSender
            {
                Id = 1,
                Invoice = null,
                Sender = sender
            };
        }

        public static InvoiceSender GetInvalidInvoiceSenderWithSenderNull(Invoice invoice)
        {
            return new InvoiceSender
            {
                Id = 1,
                Invoice = invoice,
                Sender = null
            };
        }

        public static InvoiceSender GetInvalidInvoiceSenderWithInvalidSender(Invoice invoice, Sender sender)
        {
            return new InvoiceSender
            {
                Id = 1,
                Invoice = invoice,
                Sender = sender
            };
        }
    }
}
