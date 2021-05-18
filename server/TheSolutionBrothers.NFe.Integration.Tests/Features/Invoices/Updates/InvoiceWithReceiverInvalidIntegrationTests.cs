using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Application.Features.Invoices;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
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
        //public void Test_InvoiceIntegration_Update_ReceiverWithUninformedName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "05992192034" };
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverEmptyName(ObjectMother.GetExistentValidAddress(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
            
        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<ReceiverUninformedNameException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverWithNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "05992192034" };
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverNameLength(ObjectMother.GetExistentValidAddress(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<ReceiverNameLengthOverflowException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverWithNullCpf_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverNullCpf(ObjectMother.GetExistentValidAddress());
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<ReceiverNullCpfException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverCpfWithUninformedValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), ObjectMother.GetInvalidCPFWithUninformedValue());
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CPFUninformedValueException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverCpfWithInvalidValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), ObjectMother.GetInvalidCPFWithInvalidValue());
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CPFInvalidValueException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverWithUninformedCompanyName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyCompanyName(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<ReceiverUninformedCompanyNameException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverWithCompanyNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverCompanyNameLengthOverflow(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<ReceiverCompanyNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverWithUninformedStateRegistration_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyStateRegistration(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<ReceiverUninformedStateRegistrationException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverWithStateRegistrationLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverStateRegistrationLengthOverflow(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<ReceiverStateRegistrationLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverWithNullCNPJ_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullCnpj(ObjectMother.GetExistentValidAddress());
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<ReceiverNullCnpjException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverCNPJWithUninformedValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetInvalidCNPJWithUninformedValue();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CNPJUninformedValueException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverCNPJWithInvalidValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetInvalidCNPJWithInvalidValue();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CNPJInvalidValueException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverWithNullAddress_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullAddress(cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<ReceiverUninformedAddressException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithNegativeNumber_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithNegativeNumber(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressNegativeNumberException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithUninformedStreetName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedStreetName(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedStreetNameException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithStreetNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithStreetNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressStreetNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithUninformedNeighborhood_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedNeighborhood(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedNeighborhoodException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithNeighborhoodLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithNeighborhoodNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressNeighborhoodLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithUninformedCity_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedCity(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedCityException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithCityLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithCityNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressCityLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithUninformedState_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedState(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedStateException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithStateLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithStateNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressStateLengthOverflowException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithUninformedCountry_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedCountry(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedCountryException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_ReceiverAddressWithCountryLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressCountryLengthOverflowException>();
        //}

    }
    
}
