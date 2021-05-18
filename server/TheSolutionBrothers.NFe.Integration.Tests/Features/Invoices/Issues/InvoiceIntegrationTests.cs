using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using System;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Invoices
{
    [TestFixture]
    public partial class InvoiceIntegrationTests
    {

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_ShouldBeOk()
   //     {
   //         int validNumber = 100;
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         invoice.Number = validNumber;

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(invoice, product);
   //         items.Add(item);

   //         double freightValue = 10;
   //         _invoiceService.Issue(invoice, freightValue);

   //         int expectedItemsCount = 1;

   //         Invoice invoiceFromDatabase = _invoiceService.Get(invoice.Id);
   //         invoiceFromDatabase.Status.Should().Be(invoice.Status);
   //         invoiceFromDatabase.KeyAccess.Value.Should().Be("00000000000000000000000000000000000000000100");
   //         invoiceFromDatabase.InvoiceCarrier.Should().NotBeNull();
   //         invoiceFromDatabase.InvoiceSender.Should().NotBeNull();
   //         invoiceFromDatabase.InvoiceReceiver.Should().NotBeNull();
   //         invoiceFromDatabase.InvoiceTax.Should().NotBeNull();
   //         invoiceFromDatabase.InvoiceItems.Count.Should().Be(expectedItemsCount);
   //         invoiceFromDatabase.InvoiceItems[0].Id.Should().Be(invoice.InvoiceItems[0].Id);
   //         invoiceFromDatabase.Carrier.Id.Should().Be(invoice.Carrier.Id);
   //         invoiceFromDatabase.Receiver.Id.Should().Be(invoice.Receiver.Id);
   //         invoiceFromDatabase.Sender.Id.Should().Be(invoice.Sender.Id);
   //     }
        
   //     [Test]
   //     public void Test_InvoiceIntegration_IssueWithNullCarrier_ShouldBeOk()
   //     {
   //         int validNumber = 100;
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = null;

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         invoice.Number = validNumber;

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(invoice, product);
   //         items.Add(item);

   //         double freightValue = 10;
   //         _invoiceService.Issue(invoice, freightValue);

   //         int expectedItemsCount = 1;

   //         Invoice invoiceFromDatabase = _invoiceService.Get(invoice.Id);
   //         invoiceFromDatabase.Status.Should().Be(invoice.Status);
   //         invoiceFromDatabase.KeyAccess.Value.Should().Be("00000000000000000000000000000000000000000100");
   //         invoiceFromDatabase.InvoiceCarrier.Should().NotBeNull();
   //         invoiceFromDatabase.InvoiceSender.Should().NotBeNull();
   //         invoiceFromDatabase.InvoiceReceiver.Should().NotBeNull();
   //         invoiceFromDatabase.InvoiceTax.Should().NotBeNull();
   //         invoiceFromDatabase.InvoiceItems.Count.Should().Be(expectedItemsCount);
   //         invoiceFromDatabase.InvoiceItems[0].Id.Should().Be(invoice.InvoiceItems[0].Id);
   //         invoiceFromDatabase.Carrier.Should().BeNull();
   //         invoiceFromDatabase.Receiver.Id.Should().Be(invoice.Receiver.Id);
   //         invoiceFromDatabase.Sender.Id.Should().Be(invoice.Sender.Id);
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_ExistentNumber_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(invoice, product);
   //         items.Add(item);

   //         double freightValue = 10;
   //         Action action = () =>_invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceExistentNumberException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_InvoiceWithInvalidId_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<IdentifierUndefinedException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_InvoiceIssued_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
   //         InvoiceSender invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(null, sender);
   //         InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(null, receiver);
   //         InvoiceCarrier invoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(carrier, null);
   //         InvoiceTax invoiceTax = ObjectMother.GetExistentValidInvoiceTax(null);

   //         Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(keyAccess, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceIssueIssuedInvoiceException>();

   //     }

   //     #region Carrier

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_CarrierCpfWithValueInvalid_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetInvalidCPFWithInvalidValue();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<CPFInvalidValueException>();
   //     }


   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_CarrierCpfWithUninformedValue_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetInvalidCPFWithUninformedValue();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<CPFUninformedValueException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_CarrierPhysicalWithNullAddress_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetInvalidCarrierPhysicalNullAddress(cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<CarrierNullAddressException>();
   //     }

   //     #endregion Carrier


   //     #region Receiver

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_ReceiverCnpjWithInvalidValue_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetInvalidCNPJWithInvalidValue();
   //         CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<CNPJInvalidValueException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_ReceiverCnpjWithUninformedValue_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetInvalidCNPJWithUninformedValue();
   //         CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<CNPJUninformedValueException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_LegalReceiverWithNullAddress_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullAddress(cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<ReceiverUninformedAddressException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_LegalReceiverWithEmptyStateRegistration_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyStateRegistration(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<ReceiverUninformedStateRegistrationException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_LegalReceiverWithUninformedCompanyName_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullCompanyName(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<ReceiverUninformedCompanyNameException>();
   //     }

   //     #endregion Receiver

   //     #region Sender
   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_SenderCnpjWithInvalidValue_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = ObjectMother.GetInvalidCNPJWithInvalidValue();

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<CNPJInvalidValueException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_SenderCnpjWithUninformedValue_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = ObjectMother.GetInvalidCNPJWithUninformedValue();

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<CNPJUninformedValueException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_SenderWithNullAddress_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

			//Sender sender = ObjectMother.GetInvalidSenderWithNullAddress(cnpjSender);
			//Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<SenderNullAddressException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_SenderWithUninformedCompanyName_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetInvalidSenderWithUninformedCompanyName(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<SenderUninformedCompanyNameException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_SenderWithUninformedFancyName_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetInvalidSenderWithUninformedFancyName(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<SenderUninformedFancyNameException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_SenderWithUninformedMunicipalRegistration_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetInvalidSenderWithUninformedMunicipalRegistration(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<SenderUninformedMunicipalRegistrationException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_SenderWithUninformedStateRegistration_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetInvalidSenderWithUninformedStateRegistration(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<SenderUninformedStateRegistrationException>();
   //     }

   //     #endregion Sender

   //     #region Product

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_ProductWithNullTaxProduct_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         Product product = ObjectMother.GetInvalidProductWithNullTaxProduct();
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
   //         items.Add(item);

   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<ProductNullTaxProductException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_ProductWithUniformedCode_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         Product product = ObjectMother.GetInvalidProductWithUniformedCode(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
   //         items.Add(item);

   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<ProductUninformedCodeException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_ProductWithUninformedDescription_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         Product product = ObjectMother.GetInvalidProductWithUninformedDescription(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
   //         items.Add(item);

   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<ProductUninformedDescriptionException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_ProductWithCurrentValueLowerThanZero_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         Product product = ObjectMother.GetInvalidProductWithCurrentValueLowerThanZero(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
   //         items.Add(item);

   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<ProductCurrentValueLowerOrEqualThanZeroException>();
   //     }

   //     #endregion Product

   //     #region  InvoiceItem

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_InvoiceItemWithUninformedAmount_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetInvalidInvoiceItemWithUninformedAmount(invoice, product);
   //         items.Add(item);

   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceItemUninformedAmountException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_InvoiceItemWithInvalidAmount_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetInvalidInvoiceItemWithInvalidAmount(invoice, product);
   //         items.Add(item);

   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceItemInvalidAmountException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_InvoiceItemWithNullInvoice_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetInvalidInvoiceItemWithInvoiceNull(product);
   //         items.Add(item);

   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceItemNullInvoiceException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_InvoiceItemWithNullProduct_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         InvoiceItem item = ObjectMother.GetInvalidInvoiceItemWithProductNull(invoice);
   //         items.Add(item);

   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceItemNullProductException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_ConsolidatedInvoiceItemWithUninformedAmount_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         IList<InvoiceItem> items = new List<InvoiceItem>();

   //         Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetInvalidConsolidatedInvoiceItemWithUninformedAmount(invoice, product);
   //         items.Add(item);

   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceItemUninformedAmountException>();
   //     }

   //     #endregion InvoiceItem

   //     #region Invoice
   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_OpenedInvoiceWithEntryDateAfterNow_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithEntryDateAfterNow(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceEntryDateAfterNowException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_OpenedInvoiceWithNonPositiveNumber_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNonPositiveNumber(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceNonPositiveNumberException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_OpenedInvoiceWithUniformedNatureOperation_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithUniformedNatureOperation(sender, receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceUninformedNatureOperationException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_OpenedInvoiceWithNullSender_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();

   //         Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNullSender(receiver, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceNullSenderException>();
   //     }

   //     [Test]
   //     public void Test_InvoiceIntegration_Issue_OpenedInvoiceWithNullReceiver_ShouldThrowException()
   //     {
   //         CPF cpfCarrier = ObjectMother.GetValidCPF();
   //         CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

   //         Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
   //         Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

   //         Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
   //         InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);
   //         IList<InvoiceItem> items = new List<InvoiceItem>() { item };

   //         Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNullReceiver(sender, carrier, items);
   //         double freightValue = 10;

   //         Action action = () => _invoiceService.Issue(invoice, freightValue);
   //         action.Should().Throw<InvoiceNullReceiverException>();
   //     }

   //     #endregion Invoice
    }
}
