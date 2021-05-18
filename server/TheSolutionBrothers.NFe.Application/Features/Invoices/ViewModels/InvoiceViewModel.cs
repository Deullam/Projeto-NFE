using System;
using System.Collections.Generic;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels
{
	public class InvoiceViewModel
	{
		public virtual long Id { get; set; }
		public virtual string NatureOperation { get; set; }
		public virtual string KeyAccess { get; set; }
		public virtual int Number { get; set; }
		public virtual InvoiceStatus Status { get; set; }
		public virtual string EntryDate { get; set; }
        public virtual string IssueDate { get; set; }
        public virtual InvoiceTaxViewModel InvoiceTax { get; set; }
		public virtual SenderViewModel Sender { get; set; }
		public virtual ReceiverViewModel Receiver { get; set; }
		public virtual CarrierViewModel Carrier { get; set; }
        public virtual List<InvoiceItemViewModel> InvoiceItems { get; set; }

        public virtual string SenderName { get; set; }
        public virtual string CarrierName { get; set; }
        public virtual string ReceiverName { get; set; }

    }
}
