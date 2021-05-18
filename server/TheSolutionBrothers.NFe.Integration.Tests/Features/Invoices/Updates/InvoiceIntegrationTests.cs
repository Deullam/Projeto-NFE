using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Application.Features.Invoices;
using TheSolutionBrothers.NFe.Common.Tests.Base;
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
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceTaxes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Invoices
{

    [TestFixture]
    public partial class InvoiceIntegrationTests
    {

        //[Test]
        //public void Test_InvoiceIntegration_Update_ShouldBeOk()
        //{
        //    long existentId = 3;
        //    int validNumber = 10;
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
        //    invoice.Id = existentId;
        //    invoice.Number = validNumber;
        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    _invoiceService.Update(invoice);

        //    int expectedItemsCount = 1;

        //    Invoice invoiceFromDatabase = _invoiceService.Get(invoice.Id);
        //    invoiceFromDatabase.EntryDate.Should().NotBeAfter(DateTime.Now);
        //    invoiceFromDatabase.Status.Should().Be(invoice.Status);
        //    invoiceFromDatabase.IssueDate.Should().BeNull();
        //    invoiceFromDatabase.KeyAccess.Should().BeNull();
        //    invoiceFromDatabase.InvoiceCarrier.Should().BeNull();
        //    invoiceFromDatabase.InvoiceSender.Should().BeNull();
        //    invoiceFromDatabase.InvoiceReceiver.Should().BeNull();
        //    invoiceFromDatabase.InvoiceTax.Should().BeNull();
        //    invoiceFromDatabase.InvoiceItems.Count.Should().Be(expectedItemsCount);
        //    invoiceFromDatabase.InvoiceItems[0].Id.Should().Be(invoice.InvoiceItems[0].Id);
        //    invoiceFromDatabase.Carrier.Id.Should().Be(invoice.Carrier.Id);
        //    invoiceFromDatabase.Receiver.Id.Should().Be(invoice.Receiver.Id);
        //    invoiceFromDatabase.Sender.Id.Should().Be(invoice.Sender.Id);
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_NewProduct_ShouldBeOk()
        //{
        //    int validNumber = 10;
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, null, items);
        //    invoice.Number = validNumber;
        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetNewInvoiceItemOk(invoice, product);
        //    items.Add(item);

        //    _invoiceService.Update(invoice);

        //    int expectedItemsCount = 2;

        //    Invoice invoiceFromDatabase = _invoiceService.Get(invoice.Id);
        //    invoiceFromDatabase.Status.Should().Be(invoice.Status);
        //    invoiceFromDatabase.IssueDate.HasValue.Should().BeFalse();
        //    invoiceFromDatabase.KeyAccess.Should().BeNull();
        //    invoiceFromDatabase.InvoiceCarrier.Should().BeNull();
        //    invoiceFromDatabase.InvoiceSender.Should().BeNull();
        //    invoiceFromDatabase.InvoiceReceiver.Should().BeNull();
        //    invoiceFromDatabase.InvoiceTax.Should().BeNull();
        //    invoiceFromDatabase.InvoiceItems.Count.Should().Be(expectedItemsCount);
        //    invoiceFromDatabase.Carrier.Should().BeNull();
        //    invoiceFromDatabase.Receiver.Id.Should().Be(invoice.Receiver.Id);
        //    invoiceFromDatabase.Sender.Id.Should().Be(invoice.Sender.Id);
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_NullCarrier_ShouldBeOk()
        //{
        //    int validNumber = 10;
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, null, items);
        //    invoice.Number = validNumber;
        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    _invoiceService.Update(invoice);

        //    int expectedItemsCount = 1;

        //    Invoice invoiceFromDatabase = _invoiceService.Get(invoice.Id);
        //    invoiceFromDatabase.Status.Should().Be(invoice.Status);
        //    invoiceFromDatabase.IssueDate.HasValue.Should().BeFalse();
        //    invoiceFromDatabase.KeyAccess.Should().BeNull();
        //    invoiceFromDatabase.InvoiceCarrier.Should().BeNull();
        //    invoiceFromDatabase.InvoiceSender.Should().BeNull();
        //    invoiceFromDatabase.InvoiceReceiver.Should().BeNull();
        //    invoiceFromDatabase.InvoiceTax.Should().BeNull();
        //    invoiceFromDatabase.InvoiceItems.Count.Should().Be(expectedItemsCount);
        //    invoiceFromDatabase.InvoiceItems[0].Id.Should().Be(invoice.InvoiceItems[0].Id);
        //    invoiceFromDatabase.Carrier.Should().BeNull();
        //    invoiceFromDatabase.Receiver.Id.Should().Be(invoice.Receiver.Id);
        //    invoiceFromDatabase.Sender.Id.Should().Be(invoice.Sender.Id);
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_InvalidId_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<IdentifierUndefinedException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ExistentNumber_ShouldThrowException()
        //{
        //    int existentNumber = 4;
        //    long existentId = 1;
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);
        //    invoice.Id = existentId;
        //    invoice.Number = existentNumber;
        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceExistentNumberException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_Issued_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
        //    InvoiceSender invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(null, sender);
        //    InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(null, receiver);
        //    InvoiceCarrier invoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(carrier, null);
        //    InvoiceTax invoiceTax = ObjectMother.GetExistentValidInvoiceTax(null);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(keyAccess, sender, receiver, carrier, invoiceSender, 
        //                                                invoiceReceiver, invoiceCarrier, invoiceTax, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceUpdateIssuedInvoiceException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_UninformedNaruteOperation_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    long existentId = 1;

        //    Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithUniformedNatureOperation(sender, receiver, carrier, items);
        //    invoice.Id = existentId;

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceUninformedNatureOperationException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_NaruteOperationLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    long existentId = 1;

        //    Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNatureOperationLengthOverflow(sender, receiver, carrier, items);
        //    invoice.Id = existentId;

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceNaruteOperationLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_NonPositiveNumber_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    long existentId = 1;

        //    Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNonPositiveNumber(sender, receiver, carrier, items);
        //    invoice.Id = existentId;

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceNonPositiveNumberException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_EntryDateAfterNow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    long existentId = 1;

        //    Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithEntryDateAfterNow(sender, receiver, carrier, items);
        //    invoice.Id = existentId;

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceEntryDateAfterNowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_NullSender_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    long existentId = 1;

        //    Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNullSender(receiver, carrier, items);
        //    invoice.Id = existentId;

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceNullSenderException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_NullReceiver_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    long existentId = 1;

        //    Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNullReceiver(sender, carrier, items);
        //    invoice.Id = existentId;

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceNullReceiverException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverEqualThanSender_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
            
        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceReceiverEqualThanSenderException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_EmptyInvoiceItems_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
            
        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<InvoiceEmptyInvoiceItemsException>();
        //}

    }
    
}
