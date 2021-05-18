using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers
{
    public class InvoiceReceiver : Entity
    {

        [XmlIgnore]
        public Invoice Invoice { get; set; }
        public virtual Receiver Receiver { get; set; }

        public override void Validate()
        {
			if(Invoice == null)
			{
				throw new InvoiceReceiverNullInvoiceException();
			}

			if (Receiver == null)
			{
				throw new InvoiceReceiverNullReceiverException();
			}
			Receiver.Validate();
		}
    }
}
