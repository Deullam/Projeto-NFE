using FluentAssertions;
using Moq;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.InvoiceSenders
{

    [TestFixture]
    public class InvoiceSenderTests
    {
        private Mock<Invoice> _fakeInvoice;
        private Mock<Sender> _fakeSender;

        [SetUp]
        public void Initialize()
        {
            _fakeInvoice = new Mock<Invoice>();
            _fakeSender = new Mock<Sender>();
        }

        [Test]
        public void Tets_InvoiceSender_Validate_ShouldBeOk()
        {
            InvoiceSender invoiceSender = ObjectMother.GetNewInvoiceSenderOk(_fakeInvoice.Object, _fakeSender.Object);
            invoiceSender.Validate();
        }

        [Test]
        public void Tets_InvoiceSender_ValidateWithNullInvoice_ShouldThrowException()
        {
            InvoiceSender invoiceSender = ObjectMother.GetInvalidInvoiceSenderWithInvoiceNull(_fakeSender.Object);
            Action action = () => invoiceSender.Validate();
            action.Should().Throw<InvoiceSenderNullInvoiceException>();
        }

        [Test]
        public void Tets_InvoiceSender_ValidateWithNullSender_ShouldThrowException()
        {
            InvoiceSender invoiceSender = ObjectMother.GetInvalidInvoiceSenderWithSenderNull(_fakeInvoice.Object);
            Action action = () => invoiceSender.Validate();
            action.Should().Throw<InvoiceSenderNullSenderException>();
        }

        [Test]
        public void Tets_InvoiceSender_ValidateWithInvalidSender_ShouldThrowException()
        {
            _fakeSender.Setup(sender => sender.Validate()).Throws<SenderCompanyNameLenghtOverflowException>();
            InvoiceSender invoiceSender = ObjectMother.GetInvalidInvoiceSenderWithInvalidSender(_fakeInvoice.Object, _fakeSender.Object);
            Action action = () => invoiceSender.Validate();
            action.Should().Throw<SenderCompanyNameLenghtOverflowException>();
        }
    }
}
