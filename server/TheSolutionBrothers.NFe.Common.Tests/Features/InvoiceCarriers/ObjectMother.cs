using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        
        public static InvoiceCarrier GetNewValidInvoiceCarrier(Invoice invoice)
        {
            return new InvoiceCarrier
            {
                Invoice = invoice
            };

        }

        public static InvoiceCarrier GetNewValidInvoiceCarrier(Carrier carrier, Invoice invoice)
        {
            return new InvoiceCarrier
            {
                Carrier = carrier,
                Invoice = invoice
            };

        }

        public static InvoiceCarrier GetExistentInvoiceCarrier(Carrier carrier, Invoice invoice)
        {
            return new InvoiceCarrier
            {   Id = 1,
                Carrier = carrier,
                Invoice = invoice
            };
        }

        public static InvoiceCarrier GetInvalidInvoiceCarrierWithNullCarrier(Carrier carrier, Invoice invoice)
        {
            return new InvoiceCarrier
            {
                Carrier = null,
                Invoice = invoice,

            };
        }
        public static InvoiceCarrier GetInvalidInvoiceCarrierWithNullInvoice(Carrier carrier, Invoice invoice)
        {
            return new InvoiceCarrier
            {
                Carrier = carrier,
                Invoice = null,

            };
        }

        public static InvoiceCarrier GetInvalidInvoiceCarrierWithInvalidCarrier(Carrier carrier, Invoice invoice)
        {
            return new InvoiceCarrier
            {
                Id = 1,
                Carrier = carrier,
                Invoice = invoice
            };
        }

    }
}
