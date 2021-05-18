using FluentAssertions;
using Moq;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.InvoiceItems
{

    [TestFixture]
    public class InvoiceItemTests
    {
        private Mock<Invoice> _fakeInvoice;
        private Mock<Product> _fakeProduct;

        [SetUp]
        public void Initialize()
        {
            _fakeInvoice = new Mock<Invoice>();
            _fakeProduct = new Mock<Product>();
        }

        [Test]
        public void Test_InvoiceItem_Validate_ShouldBeOk()
        {
            InvoiceItem invoiceItem = ObjectMother.GetNewInvoiceItemOk(_fakeInvoice.Object, _fakeProduct.Object);
            invoiceItem.Validate();
        }

        [Test]
        public void Test_InvoiceItem_ConsolidateTax_ShouldBeOk()
        {
            double expectedIcmsAliquot = 0.04;
            double expectedIpiAliquot = 0.1;
            double currentValue = 100.00;
            string code = "1234";
            string description = "test of consolidation";
            _fakeProduct.Setup(p => p.TaxProduct.IcmsAliquot).Returns(expectedIcmsAliquot);
            _fakeProduct.Setup(p => p.TaxProduct.IpiAliquot).Returns(expectedIpiAliquot);
            _fakeProduct.Setup(p => p.Code).Returns(code);
            _fakeProduct.Setup(p => p.CurrentValue).Returns(currentValue);
            _fakeProduct.Setup(p => p.Description).Returns(description);

            InvoiceItem invoiceItem = ObjectMother.GetNewInvoiceItemOk(_fakeInvoice.Object, _fakeProduct.Object);
            invoiceItem.Consolidate();

            invoiceItem.IcmsAliquot.Should().Be(expectedIcmsAliquot);
            invoiceItem.IpiAliquot.Should().Be(expectedIpiAliquot);
            invoiceItem.Code.Should().Be(code);
            invoiceItem.UnitValue.Should().Be(currentValue);
            invoiceItem.Description.Should().Be(description);
        }

        [Test]
        public void Test_InvoiceItem_TotalValue_ShoulBeOk()
        {
            double currentValue = 100.00;
            double expecterTotalValue = 200.00;
            double expectedIcmsAliquot = 0.04;
            double expectedIpiAliquot = 0.1;
            _fakeProduct.Setup(p => p.TaxProduct.IcmsAliquot).Returns(expectedIcmsAliquot);
            _fakeProduct.Setup(p => p.TaxProduct.IpiAliquot).Returns(expectedIpiAliquot);
            _fakeProduct.Setup(p => p.CurrentValue).Returns(currentValue);
            InvoiceItem invoiceItem = ObjectMother.GetNewConsolidatedInvoiceItem(_fakeInvoice.Object, _fakeProduct.Object);
            
            invoiceItem.TotalValue.Should().Be(expecterTotalValue);
        }

        [Test]
        public void Test_InvoiceItem_IcmsValue_ShoulBeOk()
        {
            double IcmsAliquot = 0.04;
            double CurrentValue = 100.00;
            _fakeProduct.Setup(p => p.TaxProduct.IcmsAliquot).Returns(IcmsAliquot);
            _fakeProduct.Setup(p => p.CurrentValue).Returns(CurrentValue);
            InvoiceItem invoiceItem = ObjectMother.GetNewConsolidatedInvoiceItem(_fakeInvoice.Object, _fakeProduct.Object);
            double IcmValue =  invoiceItem.TotalValue * IcmsAliquot;
            
            invoiceItem.IcmsValue.Should().Be(IcmValue);
        }

        [Test]
        public void Test_InvoiceItem_IpiValue_ShoulBeOk()
        {
            double IpiAliquot = 0.1;
            double CurrentValue = 100.00;
            _fakeProduct.Setup(p => p.TaxProduct.IpiAliquot).Returns(IpiAliquot);
            _fakeProduct.Setup(p => p.CurrentValue).Returns(CurrentValue);
            InvoiceItem invoiceItem = ObjectMother.GetNewConsolidatedInvoiceItem(_fakeInvoice.Object, _fakeProduct.Object);
            double IpiValue = invoiceItem.TotalValue * IpiAliquot;
            
            invoiceItem.IpiValue.Should().Be(IpiValue);
        }

        [Test]
        public void Test_InvoiceItem_ValidateUninformedAmount_ShouldThrowException()
        {
            InvoiceItem invoiceItem = ObjectMother.GetInvalidInvoiceItemWithUninformedAmount(_fakeInvoice.Object,_fakeProduct.Object);
            Action action = () => invoiceItem.Validate();
            action.Should().Throw<InvoiceItemUninformedAmountException>();
        }

        [Test]
        public void Test_InvoiceItem_ValidateInvalidAmount_ShouldThrowException()
        {
            InvoiceItem invoiceItem = ObjectMother.GetInvalidInvoiceItemWithInvalidAmount(_fakeInvoice.Object, _fakeProduct.Object);
            Action action = () => invoiceItem.Validate();
            action.Should().Throw<InvoiceItemInvalidAmountException>();
        }

        [Test]
        public void Test_InvoiceItem_ValidateNullInvoice_ShouldThrowException()
        {
            InvoiceItem invoiceItem = ObjectMother.GetInvalidInvoiceItemWithInvoiceNull(_fakeProduct.Object);
            Action action = () => invoiceItem.Validate();
            action.Should().Throw<InvoiceItemNullInvoiceException>();
        }

        [Test]
        public void Test_InvoiceItem_ValidateNullProduct_ShouldThrowException()
        {
            InvoiceItem invoiceItem = ObjectMother.GetInvalidInvoiceItemWithProductNull(_fakeInvoice.Object);
            Action action = () => invoiceItem.Validate();
            action.Should().Throw<InvoiceItemNullProductException>();
        }

        [Test]
        public void Test_InvoiceItem_ValidateInvalidSender_ShouldThrowException()
        {
            _fakeProduct.Setup(sender => sender.Validate()).Throws<ProductDescriptionLengthOverflowException>();
            InvoiceItem invoiceItem = ObjectMother.GetInvalidInvoiceItemWithInvalidProduct(_fakeInvoice.Object, _fakeProduct.Object);
            Action action = () => invoiceItem.Validate();
            action.Should().Throw<ProductDescriptionLengthOverflowException>();
        }
    }
}
