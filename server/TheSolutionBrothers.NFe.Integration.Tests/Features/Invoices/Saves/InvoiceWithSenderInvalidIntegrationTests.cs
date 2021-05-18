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
        //public void Test_InvoiceService_AddInvoiceWithSenderWithUninformedFancyName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithUninformedFancyName(ObjectMother.GetExistentValidAddress(), cnpjSender);


        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderUninformedFancyNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_AddInvoiceWithSenderWithFancyNameLenghtOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithFancyNameLenghtOverflow(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderFancyNameLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_AddInvoiceWithSenderWithUninformedCompanyName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithUninformedCompanyName(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderUninformedCompanyNameException>();
        //}

        //[Test]
        //public void Test_InvoiceService_AddInvoiceWithSenderWithCompanyNameLenghtOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithCompanyNameLenghtOverflow(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderCompanyNameLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_InvoiceWithSenderWithNullCnpj_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();

        //    Sender sender = ObjectMother.GetInvalidSenderWithNullCnpj(ObjectMother.GetExistentValidAddress());
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderNullCnpjException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_SenderWithCnpjWithValueNull_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithInvalidCnpj_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "4385934890384953845" };

        //    Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithUninformedStateRegistration_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithUninformedStateRegistration(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderUninformedStateRegistrationException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_SenderWithStateRegistrationLenghtOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithStateRegistrationLenghtOverflow(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderStateRegistrationLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_SenderWithUninformedMunicipalRegistration_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithUninformedMunicipalRegistration(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderUninformedMunicipalRegistrationException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_SenderWithMunicipalRegistrationLenghtOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithMunicipalRegistrationLenghtOverflow(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderMunicipalRegistrationLenghtOverflowException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_SenderWithAddressNull_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithNullAddress(cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //    Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //    IList<InvoiceItem> items = new List<InvoiceItem>();

        //    Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

        //    Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //    InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
        //    items.Add(item);

        //    Action action = () => _invoiceService.Add(invoiceToSave);
        //    action.Should().Throw<SenderNullAddressException>();
        //}

        //[Test]
        //public void Test_InvoiceService_Add_SenderWithAddressWithNegativeNumber_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithNegativeNumber(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithUninformedStreetName_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithUninformedStreetName(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithStreetNameLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithStreetNameLengthOverflow(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithUninformedNeighborhood_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithUninformedNeighborhood(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithNeighborhoodLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithNeighborhoodNameLengthOverflow(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithUninformedCity_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithUninformedCity(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithCityLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithCityNameLengthOverflow(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithUninformedState_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithUninformedState(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithStateLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithStateNameLengthOverflow(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithUninformedCountry_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithUninformedCountry(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
        //public void Test_InvoiceService_Add_SenderWithAddressWithCountryLengthOverflow_ShouldThrowException()
        //{
        //    CPF cpfCarrier = ObjectMother.GetValidCPF();
        //    CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //    CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //    Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow(), cnpjSender);
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
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
