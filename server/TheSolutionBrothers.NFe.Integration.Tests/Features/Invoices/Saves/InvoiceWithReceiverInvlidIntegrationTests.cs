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
        //public void Test_InvoiceService_Add_ReceiverPhysicalWithUninformedName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "00746507030151" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverEmptyName(ObjectMother.GetExistentValidAddress(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverUninformedNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalWithNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "00746507030151" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverNameLength(ObjectMother.GetExistentValidAddress(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalWithNullCpf_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "00746507030151" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverNullCpf(ObjectMother.GetExistentValidAddress());
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverNullCpfException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalCpfUninformedValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
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
        //public void Test_InvoiceService_Add_ReceiverPhysicalCpfInvalidValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "352352323423423234" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetExistentValidAddress(), cpfReceiver);
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
        //public void Test_InvoiceService_Add_ReceiverPhysicalWithNullAddress_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverNullAddress(cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverUninformedAddressException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithNegativeNumber_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithNegativeNumber(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressNegativeNumberException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithUninformedStreetName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithUninformedStreetName(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedStreetNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithStreetNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithStreetNameLengthOverflow(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressStreetNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithUninformedNeighborhood_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithUninformedNeighborhood(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedNeighborhoodException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithNeighborhoodLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithNeighborhoodNameLengthOverflow(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressNeighborhoodLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithUninformedCity_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithUninformedCity(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedCityException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithCityLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithCityNameLengthOverflow(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressCityLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithUninformedState_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithUninformedState(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedStateException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithStateLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithStateNameLengthOverflow(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressStateLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithUninformedCountry_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithUninformedCountry(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedCountryException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverPhysicalAddressWithCountryLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CPF cpfReceiver = new CPF() { Value = "01252333960" };
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cpfReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressCountryLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalWithUninformedName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullName(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverUninformedNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalWithNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverNameLength(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalWithNullCnpj_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullCnpj(ObjectMother.GetExistentValidAddress());
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverNullCnpjException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalCnpjWithUninformedValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetInvalidCNPJWithUninformedValue();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyCnpj(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CNPJUninformedValueException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalCnpjWithInvalidValue_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetInvalidCNPJWithInvalidValue();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyCnpj(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<CNPJInvalidValueException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalWithUninformedCompanyName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyCompanyName(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverUninformedCompanyNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalWithCompanyNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverCompanyNameLengthOverflow(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverCompanyNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalWithUninformedStateRegistration_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyStateRegistration(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverUninformedStateRegistrationException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalWithStateRegistrationLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverStateRegistrationLengthOverflow(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverStateRegistrationLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalWithNullAddress_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullAddress(cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<ReceiverUninformedAddressException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithNegativeNumber_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithNegativeNumber(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressNegativeNumberException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithUninformedStreetName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedStreetName(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedStreetNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithStreetNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithStreetNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressStreetNameLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithUninformedNeighborhood_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedNeighborhood(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedNeighborhoodException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithNeighborhoodLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithNeighborhoodNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressNeighborhoodLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithUninformedCity_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedCity(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedCityException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithCityLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithCityNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressCityLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithUninformedState_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedState(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedStateException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithStateLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithStateNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressStateLengthOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithUninformedCountry_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithUninformedCountry(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<AddressUninformedCountryException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_ReceiverLegalAddressWithCountryLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

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
