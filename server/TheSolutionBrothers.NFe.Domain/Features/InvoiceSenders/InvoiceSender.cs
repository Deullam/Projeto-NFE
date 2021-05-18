using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using System;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders
{ 

    public class InvoiceSender : Entity
    {
        [XmlIgnore]
        public Invoice Invoice { get; set; }
        public virtual Sender Sender { get; set; }

        public override void Validate()
        {
            if (Invoice == null)
                throw new InvoiceSenderNullInvoiceException();
            if (Sender == null)
                throw new InvoiceSenderNullSenderException();
            Sender.Validate();
        }

    }
}
