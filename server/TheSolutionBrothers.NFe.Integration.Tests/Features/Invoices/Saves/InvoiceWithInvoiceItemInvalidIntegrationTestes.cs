using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
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
        //public void Test_InvoiceService_Add_InvoiceItemWithUninformedAmount_ShouldThrowException()
        //{
        //    int uninformedAmount = 0;
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);
        //    invoiceToSave.InvoiceItems.First().Amount = uninformedAmount;

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<InvoiceItemUninformedAmountException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemWithInvalidAmount_ShouldThrowException()
        //{
        //    int invalidAmount = -1;
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);
        //    invoiceToSave.InvoiceItems.First().Amount = invalidAmount;

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<InvoiceItemInvalidAmountException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemWithNullInvoice_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);
        //    invoiceToSave.InvoiceItems.First().Invoice = null;

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<InvoiceItemNullInvoiceException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemWithNullProduct_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);
        //    invoiceToSave.InvoiceItems.First().Product = null;

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<InvoiceItemNullProductException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemProductWithUniformedCode_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetInvalidProductWithUniformedCode(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ProductUninformedCodeException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemProductWithCodeLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetInvalidProductWithCodeLengthOverflow(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ProductCodeLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemProductWithUninformedDescription_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetInvalidProductWithUninformedDescription(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ProductUninformedDescriptionException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemProductWithDescriptionLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetInvalidProductWithDescriptionLengthOverflow(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ProductDescriptionLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemProductWithCurrentValueEqualThanZero_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetInvalidProductWithCurrentValueEqualThanZero(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ProductCurrentValueLowerOrEqualThanZeroException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemProductWithCurrentValueLowerThanZero_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetInvalidProductWithCurrentValueLowerThanZero(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ProductCurrentValueLowerOrEqualThanZeroException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceItemProductWithNullTaxProduct_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetInvalidProductWithNullTaxProduct();
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ProductNullTaxProductException>();
        //}
    }
}
