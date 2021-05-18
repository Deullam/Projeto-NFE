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
        //public void Test_InvoiceIntegration_Update_CarrierWithUninformedName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "05992192034" };
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierPhysicalNameEmpty(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);
            
        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierUninformedNameException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierWithInvalidPersonType_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CPF cpfReceiver = new CPF() { Value = "05992192034" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalPersonType(ObjectMother.GetExistentValidAddress(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierUninformedPersonTypeException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierWithInvalidFreightResponsability_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CPF cpfReceiver = new CPF() { Value = "05992192034" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalFreightResponsabilityEmpty(ObjectMother.GetExistentValidAddress(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierUninformedFreightResponsabilityException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierWithNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "05992192034" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierPhysicalNameLenghtOverflow(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierNameLenghtOverflowException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierWithNullCpf_ShouldThrowException()
        //{
        //    CPF cpfReceiver = new CPF() { Value = "05992192034" };
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierCPFNull(ObjectMother.GetExistentValidAddress());

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierNullCPFException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierCpfWithUninformedValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetInvalidCPFWithUninformedValue();
        //    CPF cpfReceiver = new CPF() { Value = "05992192034" };
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
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
        //public void Test_InvoiceIntegration_Update_CarrierCpfWithInvalidValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetInvalidCPFWithInvalidValue();
        //    CPF cpfReceiver = new CPF() { Value = "05992192034" };
        //    CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
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
        //public void Test_InvoiceIntegration_Update_CarrierWithUninformedCompanyName_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = new CNPJ() { Value = "36536333000187" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalCompanyNameEmpty(ObjectMother.GetExistentValidAddress(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierUninformedCompanyNameException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierWithCompanyNameLengthOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = new CNPJ() { Value = "36536333000187" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalCompanyNameLenghtOverflow(ObjectMother.GetExistentValidAddress(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierCompanyNameLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierWithUninformedStateRegistration_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = new CNPJ() { Value = "36536333000187" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierStateRegistrationEmpty(ObjectMother.GetExistentValidAddress(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierUninformedStateRegistrationException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierWithStateRegistrationLengthOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = new CNPJ() { Value = "36536333000187" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierStateRegistrationLenghtOverflow(ObjectMother.GetExistentValidAddress(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierStateRegistrationLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierWithNullCNPJ_ShouldThrowException()
        //{
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalNullCNPJ(ObjectMother.GetExistentValidAddress());

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierNullCNPJException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierCNPJWithUninformedValue_ShouldThrowException()
        //{
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierCNPJEmpty(ObjectMother.GetExistentValidAddress(), ObjectMother.GetInvalidCNPJWithUninformedValue());

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CNPJUninformedValueException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierCNPJWithInvalidValue_ShouldThrowException()
        //{
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierCNPJ(ObjectMother.GetExistentValidAddress(), ObjectMother.GetInvalidCNPJWithInvalidValue());

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CNPJInvalidValueException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierWithNullAddress_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = new CNPJ() { Value = "36536333000187" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalNullAddress(cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<CarrierNullAddressException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithNegativeNumber_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithNegativeNumber(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressNegativeNumberException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithUninformedStreetName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedStreetName(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedStreetNameException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithStreetNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithStreetNameLengthOverflow(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressStreetNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithUninformedNeighborhood_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedNeighborhood(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedNeighborhoodException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithNeighborhoodLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithNeighborhoodNameLengthOverflow(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressNeighborhoodLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithUninformedCity_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedCity(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedCityException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithCityLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithCityNameLengthOverflow(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressCityLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithUninformedState_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedState(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedStateException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithStateLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithStateNameLengthOverflow(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressStateLengthOverflowException>();
        //}
        
        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithUninformedCountry_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedCountry(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Update(invoice);
        //    action.Should().Throw<AddressUninformedCountryException>();
        //}

        //[Test]
        //public void Test_InvoiceIntegration_Update_CarrierAddressWithCountryLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cpfCarrier);

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
