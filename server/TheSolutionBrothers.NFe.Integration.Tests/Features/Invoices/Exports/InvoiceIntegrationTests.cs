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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Invoices
{

	[TestFixture]
	public partial class InvoiceIntegrationTests
	{
		//[Test]
		//public void Test_InvoiceIntegration_ExportToXML_ShouldBeOk()
		//{
		//	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML NFe - Teste integração ExportToXML Invoice (Valores aleatórios).xml";

		//	CPF cpfCarrier = ObjectMother.GetValidCPF();
		//	CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
		//	CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

		//	Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
		//	Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
		//	Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

		//	IList<InvoiceItem> items = new List<InvoiceItem>();

		//	Invoice invoice = ObjectMother.GetExistentValidOpenedInvoiceWithNumberNonexistent(sender, receiver, carrier, items);
		//	Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
		//	InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
		//	items.Add(item);

		//	double freightValue = 10;

		//	_invoiceService.Issue(invoice, freightValue);

		//	Invoice invoiceissued = _invoiceService.Get(invoice.Id);

		//	_invoiceService.ExportToXML(invoiceissued, path);
  //          FileAssert.Exists(path);
  //          File.Exists(path).Should().BeTrue();
  //      }

		//[Test]
		//public void Test_InvoiceIntegration_ExportToXML_ManyItems_ShouldBeOk()
		//{
		//	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML NFe - Teste integração ExportToXML Invoice (Valores aleatórios).xml";

		//	CPF cpfCarrier = ObjectMother.GetValidCPF();
		//	CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
		//	CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

		//	Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
		//	Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
		//	Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

		//	IList<InvoiceItem> items = new List<InvoiceItem>();

		//	Invoice invoice = ObjectMother.GetNewValidOpenedInvoiceWithNumberNonexistent(sender, receiver, carrier, items);
		//	Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());

		//	InvoiceItem item1 = ObjectMother.GetNewInvoiceItemOk(invoice, product);
		//	InvoiceItem item2 = new InvoiceItem
		//									{
		//										Amount = 14,
		//										Invoice = invoice,
		//										Product = product
		//									}; 
		//	items.Add(item1);
		//	items.Add(item2);

		//	double freightValue = 432;

		//	invoice = _invoiceService.Add(invoice);

		//	_invoiceService.Issue(invoice, freightValue);

		//	Invoice invoiceissued = _invoiceService.Get(invoice.Id);

		//	_invoiceService.ExportToXML(invoiceissued, path);
  //          FileAssert.Exists(path);
  //          File.Exists(path).Should().BeTrue();
		//}

		//[Test]
		//public void Test_InvoiceIntegration_ExportToXML_InvoiceInvalidId_ShouldThrowException()
		//{
		//	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML NFe - Teste integração ExportToXML Invoice (Valores aleatórios).xml";

		//	CPF cpfCarrier = ObjectMother.GetValidCPF();
		//	CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
		//	CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

		//	Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
		//	Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
		//	Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

		//	IList<InvoiceItem> items = new List<InvoiceItem>();

		//	Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);
		//	Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
		//	InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
		//	items.Add(item);

		//	Action action = () => _invoiceService.ExportToXML(invoice, path);
		//	action.Should().Throw<IdentifierUndefinedException>();
		//}

		//[Test]
		//public void Test_InvoiceIntegration_ExportToXML_WithInvoiceOpen_ShouldThrowException()
		//{
		//	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML NFe - Teste integração ExportToXML Invoice (Valores aleatórios).xml";

		//	CPF cpfCarrier = ObjectMother.GetValidCPF();
		//	CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
		//	CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

		//	Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
		//	Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
		//	Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

		//	IList<InvoiceItem> items = new List<InvoiceItem>();

		//	Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
		//	Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
		//	InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
		//	items.Add(item);

		//	Action action = () => _invoiceService.ExportToXML(invoice, path);
		//	action.Should().Throw<InvoiceExportOpenInvoiceException>();
		//}

		//[Test]
		//public void Test_InvoiceIntegration_ExportToXML_WithReceiverCNPJUninformedValue_ShouldThrowException()
		//{
		//	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML NFe - Teste integração ExportToXML Invoice (Valores aleatórios).xml";

		//	CPF cpfCarrier = ObjectMother.GetValidCPF();
		//	CNPJ cnpjReceiver = ObjectMother.GetInvalidCNPJWithUninformedValue();
		//	CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

		//	Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
		//	Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
		//	Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

		//	IList<InvoiceItem> items = new List<InvoiceItem>();

		//	KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
		//	InvoiceSender invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(null, sender);
		//	InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(null, receiver);
		//	InvoiceCarrier invoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(carrier, null);
		//	InvoiceTax invoiceTax = ObjectMother.GetExistentValidInvoiceTax(null);

		//	Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(keyAccess, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, items);
		//	Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
		//	InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
		//	items.Add(item);

		//	Action action = () => _invoiceService.ExportToXML(invoice, path);
		//	action.Should().Throw<CNPJUninformedValueException>();
		//}

  //      [Test]
  //      public void Test_InvoiceIntegration_ExportToXML_WithInvalidPath_ShouldThrowException()
  //      {
  //          string path = "";

  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
  //          InvoiceSender invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(null, sender);
  //          InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(null, receiver);
  //          InvoiceCarrier invoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(carrier, null);
  //          InvoiceTax invoiceTax = ObjectMother.GetExistentValidInvoiceTax(null);

  //          Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(keyAccess, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, items);
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
  //          items.Add(item);

  //          Action action = () => _invoiceService.ExportToXML(invoice, path);
  //          action.Should().Throw<InvoiceExportInvalidPathException>();
  //      }

  //      //Testes de PDF...

  //      [Test]
  //      public void Test_InvoiceIntegration_ExportToPDF_ShouldBeOk()
  //      {
  //          string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste integração ExportToPDF Invoice (Valores aleatórios).pdf";

  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoice = ObjectMother.GetExistentValidOpenedInvoiceWithNumberNonexistent(sender, receiver, carrier, items);
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
  //          items.Add(item);

  //          double freightValue = 10;

  //          _invoiceService.Issue(invoice, freightValue);

  //          Invoice invoiceissued = _invoiceService.Get(invoice.Id);

  //          _invoiceService.ExportToPDF(invoiceissued, path);
  //          Assert.That(File.Exists(path));
  //      }

  //      [Test]
  //      public void Test_InvoiceIntegration_ExportToPDF_ManyItems_ShouldBeOk()
  //      {
  //          string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste integração ExportToPDF Invoice (Valores aleatórios).pdf";

  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoice = ObjectMother.GetNewValidOpenedInvoiceWithNumberNonexistent(sender, receiver, carrier, items);
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());

  //          InvoiceItem item1 = ObjectMother.GetNewInvoiceItemOk(invoice, product);
  //          InvoiceItem item2 = new InvoiceItem
  //          {
  //              Amount = 14,
  //              Invoice = invoice,
  //              Product = product
  //          };
  //          items.Add(item1);
  //          items.Add(item2);

  //          double freightValue = 432;

  //          invoice = _invoiceService.Add(invoice);

  //          _invoiceService.Issue(invoice, freightValue);

  //          Invoice invoiceissued = _invoiceService.Get(invoice.Id);

  //          _invoiceService.ExportToPDF(invoiceissued, path);
  //          Assert.That(File.Exists(path));
  //      }

  //      [Test]
  //      public void Test_InvoiceIntegration_ExportToPDF_InvoiceInvalidId_ShouldThrowException()
  //      {
  //          string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste integração ExportToPDF Invoice (Valores aleatórios).pdf";

  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
  //          items.Add(item);

  //          Action action = () => _invoiceService.ExportToPDF(invoice, path);
  //          action.Should().Throw<IdentifierUndefinedException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceIntegration_ExportToPDF_WithInvoiceOpen_ShouldThrowException()
  //      {
  //          string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste integração ExportToPDF Invoice (Valores aleatórios).pdf";

  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
  //          items.Add(item);

  //          Action action = () => _invoiceService.ExportToPDF(invoice, path);
  //          action.Should().Throw<InvoiceExportOpenInvoiceException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceIntegration_ExportToPDF_WithReceiverCNPJUninformedValue_ShouldThrowException()
  //      {
  //          string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste integração ExportToPDF Invoice (Valores aleatórios).pdf";

  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetInvalidCNPJWithUninformedValue();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
  //          InvoiceSender invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(null, sender);
  //          InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(null, receiver);
  //          InvoiceCarrier invoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(carrier, null);
  //          InvoiceTax invoiceTax = ObjectMother.GetExistentValidInvoiceTax(null);

  //          Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(keyAccess, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, items);
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
  //          items.Add(item);

  //          Action action = () => _invoiceService.ExportToPDF(invoice, path);
  //          action.Should().Throw<CNPJUninformedValueException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceIntegration_ExportToPDF_WithInvalidPath_ShouldThrowException()
  //      {
  //          string path = "";

  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
  //          InvoiceSender invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(null, sender);
  //          InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(null, receiver);
  //          InvoiceCarrier invoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(carrier, null);
  //          InvoiceTax invoiceTax = ObjectMother.GetExistentValidInvoiceTax(null);

  //          Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(keyAccess, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, items);
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
  //          items.Add(item);

  //          Action action = () => _invoiceService.ExportToPDF(invoice, path);
  //          action.Should().Throw<InvoiceExportInvalidPathException>();
  //      }
    }
}
