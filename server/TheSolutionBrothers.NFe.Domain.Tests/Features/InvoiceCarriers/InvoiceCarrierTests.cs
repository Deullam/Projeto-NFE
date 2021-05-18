using FluentAssertions;
using Moq;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using NUnit.Framework;
using System;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.InvoiceCarriers
{

    [TestFixture]
    public class InvoiceCarrierTests
    {
        Mock<Carrier> _fakeCarrier;
        Mock<Invoice> _fakeInvoice;

        [SetUp]
        public void Initialize()
        {
            _fakeCarrier = new Mock<Carrier>();
            _fakeInvoice = new Mock<Invoice>();
        }

        [Test]
        public void Tests_InvoiceCarrier_SetReceiverAsCarrier_ShouldBeOk()
        {
            Mock<Receiver> fakeReceiver = new Mock<Receiver>();

            fakeReceiver.Setup(s => s.Id).Returns(1);
            fakeReceiver.Setup(s => s.Name).Returns("Teste");
            fakeReceiver.Setup(s => s.Cpf).Returns(ObjectMother.GetValidCPF());
            fakeReceiver.Setup(s => s.Type).Returns(PersonType.PHYSICAL);
            fakeReceiver.Setup(s => s.Address).Returns(ObjectMother.GetNewValidAddress());

            Receiver receiver = fakeReceiver.Object;

            InvoiceCarrier invoiceCarrier = ObjectMother.GetNewValidInvoiceCarrier(_fakeInvoice.Object);
            invoiceCarrier.SetReceiverAsCarrier(receiver);

            fakeReceiver.Verify(s => s.Validate());

            invoiceCarrier.Carrier.Id.Should().Be(receiver.Id);
            invoiceCarrier.Carrier.Name.Should().Be(receiver.Name);
            invoiceCarrier.Carrier.CPF.Should().Be(receiver.Cpf);
            invoiceCarrier.Carrier.PersonType.Should().Be(receiver.Type);
            invoiceCarrier.Carrier.Address.Should().Be(receiver.Address);
            invoiceCarrier.Carrier.FreightResponsability.Should().Be(FreightResponsability.RECEIVER);
        }

        [Test]
        public void Tests_InvoiceCarrier_SetReceiverAsCarrierWithNullReceiver_ShouldThrowException()
        {
            Receiver receiver = null;

            InvoiceCarrier invoiceCarrier = ObjectMother.GetNewValidInvoiceCarrier(_fakeInvoice.Object);
            Action action = () => invoiceCarrier.SetReceiverAsCarrier(receiver);
            action.Should().Throw<InvoiceCarrierNullReceiverException>();
        }

        [Test]
        public void Tests_InvoiceCarrier_SetReceiverAsCarrierWithInvalidReceiver_ShouldThrowException()
        {
            Mock<Receiver> fakeReceiver = new Mock<Receiver>();

            fakeReceiver.Setup(s => s.Validate()).Throws<ReceiverCompanyNameLengthOverflowException>();

            Receiver receiver = fakeReceiver.Object;

            InvoiceCarrier invoiceCarrier = ObjectMother.GetNewValidInvoiceCarrier(_fakeInvoice.Object);
            Action action = () => invoiceCarrier.SetReceiverAsCarrier(receiver);
            action.Should().Throw<ReceiverCompanyNameLengthOverflowException>();
        }

        [Test]
        public void Tests_InvoiceCarrier_Validade_ShouldBeOk()
        {
            InvoiceCarrier invoiceCarrier = ObjectMother.GetNewValidInvoiceCarrier(_fakeCarrier.Object, _fakeInvoice.Object);
            invoiceCarrier.Validate();
        }

        [Test]
        public void Tests_IncoiceCarrier_ValidateWithNullInvoice_ShouldBeThrow()
        {
            _fakeCarrier.Setup(c => c.Validate()).Throws<InvoiceCarrierNullInvoiceException>();
            InvoiceCarrier invoiceCarrier = ObjectMother.GetInvalidInvoiceCarrierWithNullInvoice(_fakeCarrier.Object, _fakeInvoice.Object);
            Action comparation = () => invoiceCarrier.Validate();
            comparation.Should().Throw<InvoiceCarrierNullInvoiceException>();

        }

        [Test]
        public void Tests_IncoiceCarrier_ValidateWithNullCarrier_ShouldBeThrow()
        {
            _fakeCarrier.Setup(c => c.Validate()).Throws<InvoiceCarrierNullCarrierException>();
            InvoiceCarrier invoiceCarrier = ObjectMother.GetInvalidInvoiceCarrierWithNullCarrier(_fakeCarrier.Object, _fakeInvoice.Object);
            Action comparation = () => invoiceCarrier.Validate();
            comparation.Should().Throw<InvoiceCarrierNullCarrierException>();
        }

        [Test]
        public void Tests_IncoiceCarrier_ValidateWithInvalidCarrier_ShouldBeThrow()
        {
            _fakeCarrier.Setup(c => c.Validate()).Throws<CarrierCompanyNameLenghtOverflowException>();
            InvoiceCarrier invoiceCarrier = ObjectMother.GetNewValidInvoiceCarrier(_fakeCarrier.Object, _fakeInvoice.Object);
            Action comparation = () => invoiceCarrier.Validate();
            comparation.Should().Throw<CarrierCompanyNameLenghtOverflowException>();

        }


    }
}
