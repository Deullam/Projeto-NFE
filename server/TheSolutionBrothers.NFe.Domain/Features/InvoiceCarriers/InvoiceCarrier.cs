using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using System;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers
{

    public class InvoiceCarrier : Entity
    {

        [XmlIgnore]
        public Invoice Invoice { get; set; }
        public virtual Carrier Carrier { get; set; }

        public void SetReceiverAsCarrier(Receiver receiver)
        {
            if (receiver == null)
            {
                throw new InvoiceCarrierNullReceiverException();
            }

            receiver.Validate();

            this.Carrier = new Carrier()
            {
                Id = receiver.Id,
                Name = receiver.Name,
                CompanyName = receiver.CompanyName,
                CPF = receiver.Cpf,
                CNPJ = receiver.Cnpj,
                StateRegistration = receiver.StateRegistration,
                Address = receiver.Address,
                FreightResponsability = FreightResponsability.RECEIVER,
                PersonType = receiver.Type
           };
        }

        public override void Validate()
        {
            if (Invoice == null)
            {
                throw new InvoiceCarrierNullInvoiceException();
            }
            if (Carrier == null)
            {
                throw new InvoiceCarrierNullCarrierException();
            }
            Carrier.Validate();
        }

    }
}
