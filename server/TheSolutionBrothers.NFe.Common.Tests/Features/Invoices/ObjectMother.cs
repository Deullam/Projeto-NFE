using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using System;
using System.Collections.Generic;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Commands;
using TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Queries;
using TheSolutionBrothers.NFe.Domain.Features.Products;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {

        public static Invoice GetNewValidOpenedInvoice(Sender sender, Receiver receiver, Carrier carrier, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                Status = InvoiceStatus.OPEN,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceItems = items
            };
        }

		public static Invoice GetNewValidOpenedInvoiceWithNumberNonexistent(Sender sender, Receiver receiver, Carrier carrier, IList<InvoiceItem> items)
		{
			return new Invoice()
			{
				EntryDate = DateTime.Now.AddMinutes(-5),
				Status = InvoiceStatus.OPEN,
				Number = 47382,
				NatureOperation = "Teste de natureza de operação",
				Sender = sender,
				Receiver = receiver,
				Carrier = carrier,
				InvoiceItems = items
			};
		}

		public static Invoice GetNewValidIssuedInvoice(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier, 
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetExistentValidOpenedInvoice(Sender sender, Receiver receiver, Carrier carrier, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                Id = 1,
                EntryDate = DateTime.Now.AddMinutes(-5),
                Status = InvoiceStatus.OPEN,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceItems = items
            };
        }

		public static Invoice GetExistentValidOpenedInvoiceWithNumberNonexistent(Sender sender, Receiver receiver, Carrier carrier, IList<InvoiceItem> items)
		{
			return new Invoice()
			{
				Id = 1,
				EntryDate = DateTime.Now.AddMinutes(-5),
				Status = InvoiceStatus.OPEN,
				Number = 10,
				NatureOperation = "Teste de natureza de operação",
				Sender = sender,
				Receiver = receiver,
				Carrier = carrier,
				InvoiceItems = items
			};
		}

		public static Invoice GetExistentValidIssuedInvoice(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                Id = 2,
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithAnyInvoiceItem(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = null
            };
        }

        public static Invoice GetInvalidOpenedInvoiceWithEntryDateAfterNow(Sender sender, Receiver receiver, Carrier carrier, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
				Id = 1,
                EntryDate = DateTime.Now.AddMinutes(5),
                Status = InvoiceStatus.OPEN,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidOpenedInvoiceWithNonPositiveNumber(Sender sender, Receiver receiver, Carrier carrier, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
				Id = 1,
                EntryDate = DateTime.Now.AddMinutes(-5),
                Status = InvoiceStatus.OPEN,
                Number = 0,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidOpenedInvoiceWithUniformedNatureOperation(Sender sender, Receiver receiver, Carrier carrier, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
				Id = 1,
                EntryDate = DateTime.Now.AddMinutes(-5),
                Status = InvoiceStatus.OPEN,
                Number = 1,
                NatureOperation = null,
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceItems = items
            };
        }
        
        public static Invoice GetInvalidOpenedInvoiceWithNatureOperationLengthOverflow(Sender sender, Receiver receiver, Carrier carrier, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                Status = InvoiceStatus.OPEN,
                Number = 1,
                NatureOperation = "Teste de natureza de operação Teste de natureza de operação Teste de natureza de operação Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidOpenedInvoiceWithNullSender(Receiver receiver, Carrier carrier, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
				Id = 1,
                EntryDate = DateTime.Now.AddMinutes(-5),
                Status = InvoiceStatus.OPEN,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = null,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidOpenedInvoiceWithNullReceiver(Sender sender, Carrier carrier, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
				Id = 1, 
                EntryDate = DateTime.Now.AddMinutes(-5),
                Status = InvoiceStatus.OPEN,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = null,
                Carrier = carrier,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithInvalidKeyAccess(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithNullKeyAccess(Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = null,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithNullIssueDate(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = null,
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithIssueDateAfterNow(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithIssueDateBeforeEntryDate(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-10),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithNullInvoiceSender(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = null,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithNullInvoiceReceiver(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceCarrier invoiceCarrier, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = null,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithNullInvoiceCarrier(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceTax invoiceTax, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = null,
                InvoiceTax = invoiceTax,
                InvoiceItems = items
            };
        }

        public static Invoice GetInvalidIssuedInvoiceWithNullInvoiceTax(KeyAccess keyAccess, Sender sender, Receiver receiver, Carrier carrier,
                    InvoiceSender invoiceSender, InvoiceReceiver invoiceReceiver, InvoiceCarrier invoiceCarrier, IList<InvoiceItem> items)
        {
            return new Invoice()
            {
                EntryDate = DateTime.Now.AddMinutes(-5),
                IssueDate = DateTime.Now.AddMinutes(-1),
                Status = InvoiceStatus.ISSUED,
                KeyAccess = keyAccess,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceSender = invoiceSender,
                InvoiceReceiver = invoiceReceiver,
                InvoiceCarrier = invoiceCarrier,
                InvoiceTax = null,
                InvoiceItems = items
            };
        }

        public static InvoiceRegisterCommand GetValidInvoiceRegisterCommand(List<InvoiceItemRegisterCommand> items)
        {
            return new InvoiceRegisterCommand()
            {
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                SenderId = 1,
                ReceiverId = 1,
                CarrierId = 1,
                InvoiceItems = items,
                Status = InvoiceStatus.OPEN
            };
        }
        
        public static InvoiceRegisterCommand GetInvalidInvoiceRegisterCommandWithUninformedNatureOperation(List<InvoiceItemRegisterCommand> items)
        {
            return new InvoiceRegisterCommand()
            {
                Number = 1,
                NatureOperation = "",
                SenderId = 1,
                ReceiverId = 1,
                CarrierId = 1,
                InvoiceItems = items
            };
        }

        public static InvoiceGetAllQuery GetValidInvoiceGetAllQuery()
        {
            var size = 2;
            return new InvoiceGetAllQuery(size)
            {
            };
        }

        public static InvoiceUpdateCommand GetValidInvoiceUpdateCommand()
        {
            return new InvoiceUpdateCommand()
            {
                Id = 1,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                SenderId = 1,
                ReceiverId = 1,
                CarrierId = 1,
            };
        }

        public static InvoiceUpdateCommand GetInvalidInvoiceUpdateCommandWithoutId()
        {
            return new InvoiceUpdateCommand()
            {
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                SenderId = 1,
                ReceiverId = 1,
                CarrierId = 1,
            };
        }

        public static InvoiceUpdateCommand GetInvalidInvoiceUpdateCommandWithoutUninformedNatureOperation()
        {
            return new InvoiceUpdateCommand()
            {
                Id = 1,
                Number = 1,
                NatureOperation = "",
                SenderId = 1,
                ReceiverId = 1,
                CarrierId = 1,
            };
        }

        public static InvoiceDeleteCommand GetInvoiceDeleteCommand()
        {
            return new InvoiceDeleteCommand()
            {
                InvoiceIds = new long[] { 1, 2 }
            };
        }

        public static InvoiceDeleteCommand GetInvoiceDeleteCommandWithNonExistentIds()
        {
            return new InvoiceDeleteCommand()
            {
                InvoiceIds = new long[] { 100, 200 }
            };
        }

        public static InvoiceDeleteCommand GetInvoiceDeleteCommandWithoutId()
        {
            return new InvoiceDeleteCommand()
            {
                InvoiceIds = new long[] { }
            };
        }

        public static InvoiceViewModel GetInvoiceViewModel(SenderViewModel sender, 
            ReceiverViewModel receiver, 
            CarrierViewModel carrier,
            InvoiceTaxViewModel invoiceTax,
            List<InvoiceItemViewModel> items)
        {
            return new InvoiceViewModel()
            {
                Id = 1,
                EntryDate = DateTime.Now.AddMinutes(-5).ToLongTimeString(),
                IssueDate = DateTime.Now.AddMinutes(-4).ToLongTimeString(),
                KeyAccess = "0000000000000000000000000000000000000000004",
                Status = InvoiceStatus.OPEN,
                Number = 1,
                NatureOperation = "Teste de natureza de operação",
                InvoiceTax = invoiceTax,
                Sender = sender,
                Receiver = receiver,
                Carrier = carrier,
                InvoiceItems = items
            };
        }

    }
}
