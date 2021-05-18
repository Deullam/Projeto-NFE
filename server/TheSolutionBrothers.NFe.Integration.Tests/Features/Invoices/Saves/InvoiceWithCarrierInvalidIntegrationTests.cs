using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
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
        //public void Test_InvoiceService_Add_CarrierWithUninformedPersonType_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierInvalidPersonType(ObjectMother.GetExistentValidAddress());

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierUninformedPersonTypeException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalWithUninformedName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierPhysicalNameEmpty(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierUninformedNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalWithNameLenghtOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierPhysicalNameLenghtOverflow(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierNameLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalWithNullCpf_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierCPFNull(ObjectMother.GetExistentValidAddress());

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierNullCPFException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalCpfWithUninformedValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetInvalidCPFWithUninformedValue();
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

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CPFUninformedValueException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalCpfWithInvalidValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetInvalidCPFWithInvalidValue();
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

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CPFInvalidValueException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalWithUninformedFreightResponsability_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierPhysicalFreightResponsabilityEmpty(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierUninformedFreightResponsabilityException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalWithNullAddress_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalNullAddress(cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierNullAddressException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithNegativeNumber_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithNegativeNumber(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressNegativeNumberException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithUninformedStreetName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedStreetName(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedStreetNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithStreetNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithStreetNameLengthOverflow(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressStreetNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithUninformedNeighborhood_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedNeighborhood(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedNeighborhoodException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithNeighborhoodLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithNeighborhoodNameLengthOverflow(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressNeighborhoodLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithUninformedCity_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedCity(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedCityException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithCityLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithCityNameLengthOverflow(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressCityLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithUninformedState_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedState(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedStateException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithStateLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithStateNameLengthOverflow(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressStateLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithUninformedCountry_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithUninformedCountry(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedCountryException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierPhysicalAddressWithCountryLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressCountryLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalWithUninformedName_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalNameEmpty(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierUninformedNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalWithNameLenghtOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalNameLenghtOverflow(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierNameLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalWithUninformedCompanyName_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalCompanyNameEmpty(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierUninformedCompanyNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalWithCompanyNameLenghtOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalCompanyNameLenghtOverflow(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierCompanyNameLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalWithNullCnpj_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalNullCNPJ(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow());

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierNullCNPJException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalCnpjWithUninformedValue_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetInvalidCNPJWithUninformedValue();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CNPJUninformedValueException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalCnpjWithInvalidValue_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetInvalidCNPJWithInvalidValue();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CNPJInvalidValueException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalWithUninformedStateRegistration_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierStateRegistrationEmpty(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierUninformedStateRegistrationException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalWithStateRegistrationLenghtOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierStateRegistrationLenghtOverflow(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierStateRegistrationLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalWithUninformedFreightResponsability_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalFreightResponsabilityEmpty(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierUninformedFreightResponsabilityException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalWithNullAddress_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetInvalidCarrierLegalNullAddress(cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CarrierNullAddressException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithNegativeNumber_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithNegativeNumber(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressNegativeNumberException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithUninformedStreetName_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithUninformedStreetName(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedStreetNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithStreetNameLengthOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithStreetNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressStreetNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithUninformedNeighborhood_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithUninformedNeighborhood(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedNeighborhoodException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithNeighborhoodLengthOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithNeighborhoodNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressNeighborhoodLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithUninformedCity_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithUninformedCity(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedCityException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithCityLengthOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithCityNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressCityLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithUninformedState_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithUninformedState(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedStateException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithStateLengthOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithStateNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressStateLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithUninformedCountry_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithUninformedCountry(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedCountryException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_CarrierLegalAddressWithCountryLengthOverflow_ShouldThrowException()
        //{
        //    CNPJ cnpjCarrier = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressCountryLengthOverflowException>();
        //}
    }
}
