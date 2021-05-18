using System;
using FluentAssertions;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using NUnit.Framework;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using Moq;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using System.Collections.Generic;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.InvoiceTaxes
{
	[TestFixture]
	public class InvoiceTaxesTests
	{
		private Mock<Invoice> _fakeInvoice;

		[SetUp]
		public void Initialize()
		{
			_fakeInvoice = new Mock<Invoice>();
		}
        
        [Test]
        public void Test_InvoiceTax_TotalValueInvoice_ShouldBeOk()
        {
            double expectedTotalValueInvoice = 30;

            InvoiceTax invoiceTax = ObjectMother.GetNewValidInvoiceTax(_fakeInvoice.Object);
            invoiceTax.TotalValueInvoice.Should().Be(expectedTotalValueInvoice);
        }

        [Test]
        public void Test_InvoiceTax_CalculateValues_ShouldBeOk()
        {
            double expectedIpiValue = 10;
            double expectedIcmsValue = 4;
            double expectedTotalValue = 100;

            Mock<InvoiceItem> fakeInvoiceItem = new Mock<InvoiceItem>();

            fakeInvoiceItem.Setup(i => i.Id).Returns(1);
            fakeInvoiceItem.Setup(i => i.IpiValue).Returns(expectedIpiValue);
            fakeInvoiceItem.Setup(i => i.IcmsValue).Returns(expectedIcmsValue);
            fakeInvoiceItem.Setup(i => i.TotalValue).Returns(expectedTotalValue);

            IList<InvoiceItem> items = new List<InvoiceItem>()
            {
                fakeInvoiceItem.Object
            };

            InvoiceTax invoiceTax = ObjectMother.GetNewValidInvoiceTax(_fakeInvoice.Object);
            invoiceTax.CalculateValues(items);

            invoiceTax.IpiValue.Should().Be(expectedIpiValue);
            invoiceTax.IcmsValue.Should().Be(expectedIcmsValue);
            invoiceTax.TotalValueProducts.Should().Be(expectedTotalValue);
        }

        [Test]
		public void Test_InvoiceTax_Validate_ShouldBeOk()
		{
			InvoiceTax invoiceTax = ObjectMother.GetExistentValidInvoiceTax(_fakeInvoice.Object);
			invoiceTax.Validate();
			invoiceTax.TotalValueInvoice.Should().Equals(30);
		}

		[Test]
		public void Test_InvoiceTax_ValidateWithIcmsValueEqualZero_ShouldThrowException()
		{
			InvoiceTax invoiceTax = ObjectMother.GetInvalidInvoiceTaxWithIcmsValueEqualZero(_fakeInvoice.Object);
			Action action = invoiceTax.Validate;
			action.Should().Throw<InvoiceTaxNonPositiveIcmsValueException>();
		}

		[Test]
		public void Test_InvoiceTax_ValidateWithIcmsValueLessThanZero_ShouldThrowException()
		{
			InvoiceTax invoiceTax = ObjectMother.GetInvalidInvoiceTaxWithIcmsValueLessThanZero(_fakeInvoice.Object);
			Action action = invoiceTax.Validate;
			action.Should().Throw<InvoiceTaxNonPositiveIcmsValueException>();
		}

		[Test]
		public void Test_InvoiceTax_ValidateWithIpiValueEqualZero_ShouldThrowException()
		{
			InvoiceTax invoiceTax = ObjectMother.GetInvalidInvoiceTaxWithIpiValueEqualZero(_fakeInvoice.Object);
			Action action = invoiceTax.Validate;
			action.Should().Throw<InvoiceTaxNonPositiveIpiValueException>();
		}

		[Test]
		public void Test_InvoiceTax_ValidateWithIpiValueLessThanZero_ShouldThrowException()
		{
			InvoiceTax invoiceTax = ObjectMother.GetInvalidInvoiceTaxWithIpiValueLessThanZero(_fakeInvoice.Object);
			Action action = invoiceTax.Validate;
			action.Should().Throw<InvoiceTaxNonPositiveIpiValueException>();
		}

		[Test]
		public void Test_InvoiceTax_ValidateWithTotalValueProductsEqualZero_ShouldThrowException()
		{
			InvoiceTax invoiceTax = ObjectMother.GetInvalidInvoiceTaxWithTotalValueProductsEqualZero(_fakeInvoice.Object);
			Action action = invoiceTax.Validate;
			action.Should().Throw<InvoiceTaxNonPositiveTotalValueProductsException>();
		}

		[Test]
		public void Test_InvoiceTax_ValidateWithTotalValueProductsLessThanZero_ShouldThrowException()
		{
			InvoiceTax invoiceTax = ObjectMother.GetInvalidInvoiceTaxWithTotalValueProductsLessThanZero(_fakeInvoice.Object);
			Action action = invoiceTax.Validate;
			action.Should().Throw<InvoiceTaxNonPositiveTotalValueProductsException>();
		}

		[Test]
		public void Test_InvoiceTax_ValidateWithFreightLessThanZero_ShouldThrowException()
		{
			InvoiceTax invoiceTax = ObjectMother.GetInvalidInvoiceTaxWithNegativeFreight(_fakeInvoice.Object);
			Action action = invoiceTax.Validate;
			action.Should().Throw<InvoiceTaxNegativeFreightException>();
		}

        [Test]
		public void Test_InvoiceTax_ValidateWithNullInvoice_ShouldThrowException()
		{
			InvoiceTax invoiceTax = ObjectMother.GetInvalidInvoiceTaxWithInvoiceNull();
			Action action = invoiceTax.Validate;
			action.Should().Throw<InvoiceTaxNullInvoiceException>();
		}


	}
}
