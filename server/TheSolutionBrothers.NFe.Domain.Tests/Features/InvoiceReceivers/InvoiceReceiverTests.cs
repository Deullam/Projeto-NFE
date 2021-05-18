using FluentAssertions;
using Moq;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using NUnit.Framework;
using System;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.InvoiceReceivers
{
    [TestFixture]
    public class InvoiceReceiverTests
    {
		private Mock<Invoice> _fakeInvoice;
		private Mock<Receiver> _fakeReceiver;

		[SetUp]
		public void Initialize()
		{
			_fakeInvoice = new Mock<Invoice>();
			_fakeReceiver = new Mock<Receiver>();
		}

		[Test]
		public void Test_InvoiceReceiver_Validate_ShouldBeOk()
		{
			InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(_fakeInvoice.Object, _fakeReceiver.Object);
			_fakeReceiver.Setup(a => a.Validate());
			invoiceReceiver.Validate();
		}

		[Test]
		public void Test_InvoiceReceiver_ValidateWithNullInvoice_ShouldThrowException()
		{
			InvoiceReceiver invoiceReceiver = ObjectMother.GetInvalidInvoiceReceiverNullInvoice(_fakeReceiver.Object);
			_fakeReceiver.Setup(a => a.Validate());
			Action action = invoiceReceiver.Validate;
			action.Should().Throw<InvoiceReceiverNullInvoiceException>();
		}

		[Test]
		public void Test_InvoiceReceiver_ValidateWithNullReceiver_ShouldThrowException()
		{
			InvoiceReceiver invoiceReceiver = ObjectMother.GetInvalidInvoiceReceiverNullReceiver(_fakeInvoice.Object);
			Action action = invoiceReceiver.Validate;
			action.Should().Throw<InvoiceReceiverNullReceiverException>();
		}

		[Test]
		public void Test_InvoiceReceiver_ValidateWithInvalidReceiver_ShouldThrowException()
		{
			InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(_fakeInvoice.Object, _fakeReceiver.Object);
			_fakeReceiver.Setup(a => a.Validate()).Throws<ReceiverUninformedNameException>(); ;
			Action action = invoiceReceiver.Validate;
			action.Should().Throw<ReceiverUninformedNameException>();
		}

	}
}
