using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TheSolutionBrothers.NFe.Infra.PDF.Tests.Features.Invoices
{
    public class InvoiceRepositoryPDFTests
    {

        private IInvoiceRepositoryPDF _invoiceRepositoryPDF;
        private InvoicePdfGenerator _generator;

        [SetUp]
        public void Initialize()
        {
            _generator = new InvoicePdfGenerator();
            _invoiceRepositoryPDF = new InvoiceRepositoryPDF(_generator);
        }

        [Test]
        public void Test_InvoiceRepositoryPDF_Export_LegalEntities_ShouldBeOk()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe Destinatário PJ - Teste de integração do repositório PDF de nota fiscal.pdf";

            CPF cpfCarrier = ObjectMother.GetValidCPF();
            CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
            CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

            Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
            Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
            Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

            IList<InvoiceItem> items = new List<InvoiceItem>();

            Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

            Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
            InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
            items.Add(item);

            double freightValue = 20;
            invoice.Issue(freightValue);

            _invoiceRepositoryPDF.Export(invoice, path);

            FileAssert.Exists(path);
        }

        [Test]
        public void Test_InvoiceRepositoryPDF_Export_PhysicalEntities_ShouldBeOk()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe Destinatário PF - Teste de integração do repositório PDF de nota fiscal.pdf";

            CPF cpfCarrier = new CPF() { Value = "15564641000" };
            CPF cpfReceiver = new CPF() { Value = "17497392085" };
            CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

            Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
            Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
            Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

            IList<InvoiceItem> items = new List<InvoiceItem>();

            Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

            Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
            InvoiceItem item1 = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
            items.Add(item1);

            double freightValue = 20;
            invoice.Issue(freightValue);

            _invoiceRepositoryPDF.Export(invoice, path);

            FileAssert.Exists(path);
        }

		[Test]
		public void Test_InvoiceRepositoryPDF_Export_ManyItems_ShouldBeOk()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe Muitos itens - Teste de integração do repositório XML de nota fiscal.pdf";

			CPF cpfCarrier = new CPF() { Value = "15564641000" };
			CPF cpfReceiver = new CPF() { Value = "17497392085" };
			CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

			Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
			Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
			Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

			IList<InvoiceItem> items = new List<InvoiceItem>();

			Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

			Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
			InvoiceItem item1 = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
			InvoiceItem item2 = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
			items.Add(item1);
			items.Add(item2);

			double freightValue = 20;
			invoice.Issue(freightValue);

			_invoiceRepositoryPDF.Export(invoice, path);

			FileAssert.Exists(path);
		}

		[Test]
		public void Test_InvoiceRepositoryPDF_Export_InvoiceNull_ShouldThrowException()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste de integração do repositório PDF de nota fiscal.pdf";

			Action action = () => _invoiceRepositoryPDF.Export(null, path);
			action.Should().Throw<InvoiceRepositoryPDFNullInvoiceException>();

		}

		[Test]
		public void Test_InvoiceRepositoryPDF_Export_OpenInvoice_ShouldThrowException()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste de integração do repositório PDF de nota fiscal.pdf";

			CPF cpfCarrier = ObjectMother.GetValidCPF();
			CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
			CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

			Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
			Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
			Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

			IList<InvoiceItem> items = new List<InvoiceItem>();

			Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
			Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
			InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
			items.Add(item);

			Action action = () => _invoiceRepositoryPDF.Export(invoice, path);

			action.Should().Throw<InvoiceRepositoryPDFStatusEqualsOpenException>();
		}

		[Test]
		public void Test_InvoiceRepositoryPDF_Export_InvalidInvoice_ShouldThrowException()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe Destinatário PJ - Teste de integração do repositório PDF de nota fiscal.pdf";

			CPF cpfCarrier = ObjectMother.GetValidCPF();
			CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
			CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

			Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
			Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
			Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

			KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
			InvoiceSender invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(null, sender);
			InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(null, receiver);
			InvoiceCarrier invoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(carrier, null);
			InvoiceTax invoiceTax = ObjectMother.GetExistentValidInvoiceTax(null);

			Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithAnyInvoiceItem(keyAccess, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax);

			Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());

			Action action = () => _invoiceRepositoryPDF.Export(invoice, path);
			action.Should().Throw<InvoiceEmptyInvoiceItemsException>();
        }

        [Test]
        public void Test_InvoiceRepositoryPDF_Export_NullPath_ShouldThrowException()
        {
            string path = null;

            CPF cpfCarrier = ObjectMother.GetValidCPF();
            CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
            CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

            Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
            Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
            Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

            IList<InvoiceItem> items = new List<InvoiceItem>();

            Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

            Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
            InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
            items.Add(item);

            double freightValue = 20;
            invoice.Issue(freightValue);

            Action action = () => _invoiceRepositoryPDF.Export(invoice, path);
            action.Should().Throw<InvalidPathException>();
        }

        [Test]
        public void Test_InvoiceRepositoryPDF_Export_EmptyPath_ShouldThrowException()
        {
            string path = "";

            CPF cpfCarrier = ObjectMother.GetValidCPF();
            CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
            CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

            Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
            Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
            Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

            IList<InvoiceItem> items = new List<InvoiceItem>();

            Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

            Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
            InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
            items.Add(item);

            double freightValue = 20;
            invoice.Issue(freightValue);

            Action action = () => _invoiceRepositoryPDF.Export(invoice, path);
            action.Should().Throw<InvalidPathException>();
        }

    }
}
