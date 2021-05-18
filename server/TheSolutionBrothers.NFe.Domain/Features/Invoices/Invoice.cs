using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TheSolutionBrothers.NFe.Domain.Features.Invoices
{
    public class Invoice
    {
		public virtual long Id { get; set; }
        public string NatureOperation { get; set; }
        public virtual KeyAccess KeyAccess { get; set; }
        public int Number { get; set; }
        public virtual InvoiceStatus Status { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public InvoiceTax InvoiceTax { get; set; }
        public Sender Sender { get; set; }
        public Receiver Receiver { get; set; }
        public Carrier Carrier { get; set; }
        public InvoiceSender InvoiceSender { get; set; }
        public InvoiceReceiver InvoiceReceiver { get; set; }
        public InvoiceCarrier InvoiceCarrier { get; set; }
        public IList<InvoiceItem> InvoiceItems { get; set; }

		public long SenderId { get; set; }
		public long ReceiverId { get; set; }
		public long CarrierId { get; set; }

		public Invoice()
		{
			this.KeyAccess = new KeyAccess();
		}

        public virtual void Issue(double freightValue)
        {
            this.Validate();

            this.Status = InvoiceStatus.ISSUED;
            this.IssueDate = DateTime.Now;

            this.KeyAccess = new KeyAccess();
            this.KeyAccess.GenerateKeyAccess(this.Number);

            ConsolidateReceiver();
            ConsolidateSender();
            ConsolidateCarrier();
            ConsolidateInvoiceTax(freightValue);

            foreach (InvoiceItem item in InvoiceItems)
            {
                item.Consolidate();
            }

            this.InvoiceTax.CalculateValues(this.InvoiceItems);

            this.Validate();
        }

        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(NatureOperation))
            {
                throw new InvoiceUninformedNatureOperationException();
            }
            else if (NatureOperation.Length > 70)
            {
                throw new InvoiceNaruteOperationLengthOverflowException();
            }

            if (Number < 1)
            {
                throw new InvoiceNonPositiveNumberException();
            }

            if (EntryDate.CompareTo(DateTime.Now) > 0)
            {
                throw new InvoiceEntryDateAfterNowException();
            }

            if (Sender == null)
            {
                throw new InvoiceNullSenderException();
            }

            Sender.Validate();


            if (Receiver == null)
            {
                throw new InvoiceNullReceiverException();
            }

            Receiver.Validate();


            if (IsReceiverEqualThanSender())
            {
                throw new InvoiceReceiverEqualThanSenderException();
            }

            if (Carrier != null)
            {
                Carrier.Validate();
            }

            if (InvoiceItems == null || InvoiceItems.Count == 0)
            {
                throw new InvoiceEmptyInvoiceItemsException();
            }

            foreach (InvoiceItem item in InvoiceItems)
            {
                item.Validate();
            }

            if (Status.Equals(InvoiceStatus.ISSUED)) {
                if (KeyAccess == null)
                {
                    throw new InvoiceNullKeyAccessException();
                }

                KeyAccess.Validate();

                if (!IssueDate.HasValue)
                {
                    throw new InvoiceNullIssueDateException();
                }
                else if (IssueDate.Value.CompareTo(DateTime.Now) > 0)
                {
                    throw new InvoiceIssueDateAfterNowException();
                }
                else if (IssueDate.Value.CompareTo(EntryDate) < 0)
                {
                    throw new InvoiceIssueDateBeforeEntryDateException();
                }

                if (InvoiceCarrier == null)
                {
                    throw new InvoiceNullInvoiceCarrierException();
                }
                else
                {
                    InvoiceCarrier.Validate();
                }

                if (InvoiceSender == null)
                {
                    throw new InvoiceNullInvoiceSenderException();
                }

                InvoiceSender.Validate();

                if (InvoiceReceiver == null)
                {
                    throw new InvoiceNullInvoiceReceiverException();
                }

                InvoiceReceiver.Validate();

                if (InvoiceTax == null)
                {
                    throw new InvoiceNullInvoiceTaxException();
                }

                InvoiceTax.Validate();
            }
        }

        private void ConsolidateSender()
        {
            this.InvoiceSender = new InvoiceSender()
            {
                Invoice = this,
                Sender = this.Sender
            };
        }

        private void ConsolidateReceiver()
        {
            this.InvoiceReceiver = new InvoiceReceiver()
            {
                Invoice = this,
                Receiver = this.Receiver
            };
        }

        private void ConsolidateCarrier()
        {
            if (this.Carrier == null)
            {
                this.InvoiceCarrier = new InvoiceCarrier()
                {
                    Invoice = this
                };
                this.InvoiceCarrier.SetReceiverAsCarrier(this.Receiver);
            }
            else
            {
                this.InvoiceCarrier = new InvoiceCarrier()
                {
                    Invoice = this,
                    Carrier = this.Carrier
                };
            }
        }

        public void ConsolidateInvoiceTax(double freightValue)
        {

            this.InvoiceTax = new InvoiceTax()
            {
                Invoice = this,
                Freight = freightValue
            };
        }

        public bool IsReceiverEqualThanSender()
        {
            if (Receiver.Type == PersonType.LEGAL)
            {
                return Receiver.Cnpj.Equals(Sender.Cnpj);
            }

            return false;
        }
        
    }

}
