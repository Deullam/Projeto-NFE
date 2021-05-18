using FluentAssertions;
using Moq;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.Products
{

    [TestFixture]
    public class ProductTests
    {

        private Mock<TaxProduct> _fakeTaxProduct;

        [SetUp]
        public void Initialize()
        {
            _fakeTaxProduct = new Mock<TaxProduct>();
        }
        
        [Test]
        public void Test_Product_Validate_ShouldBeOk()
        {
            //_fakeTaxProduct.Setup(tp => tp.Validate());

            Product product = ObjectMother.GetNewValidProduct(_fakeTaxProduct.Object);
            product.Validate();
        }
        
        [Test]
        public void Test_Product_ValidateWithUniformedCode_ShouldBeOk()
        {
            Product product = ObjectMother.GetInvalidProductWithUniformedCode(_fakeTaxProduct.Object);

            Action action = product.Validate;
            action.Should().Throw<ProductUninformedCodeException>();
        }

        [Test]
        public void Test_Product_ValidateWithCodeLengthOverflow_ShouldBeOk()
        {
            Product product = ObjectMother.GetInvalidProductWithCodeLengthOverflow(_fakeTaxProduct.Object);

            Action action = product.Validate;
            action.Should().Throw<ProductCodeLengthOverflowException>();
        }

        [Test]
        public void Test_Product_ValidateWithUniformedDescription_ShouldBeOk()
        {
            Product product = ObjectMother.GetInvalidProductWithUninformedDescription(_fakeTaxProduct.Object);

            Action action = product.Validate;
            action.Should().Throw<ProductUninformedDescriptionException>();
        }

        [Test]
        public void Test_Product_ValidateWithDescriptionLengthOverflow_ShouldBeOk()
        {
            Product product = ObjectMother.GetInvalidProductWithDescriptionLengthOverflow(_fakeTaxProduct.Object);

            Action action = product.Validate;
            action.Should().Throw<ProductDescriptionLengthOverflowException>();
        }
        
        [Test]
        public void Test_Product_ValidateWithCurrentValueEqualThanZero_ShouldBeOk()
        {
            Product product = ObjectMother.GetInvalidProductWithCurrentValueEqualThanZero(_fakeTaxProduct.Object);

            Action action = product.Validate;
            action.Should().Throw<ProductCurrentValueLowerOrEqualThanZeroException>();
        }

        [Test]
        public void Test_Product_ValidateWithCurrentValueLowerThanZero_ShouldBeOk()
        {
            Product product = ObjectMother.GetInvalidProductWithCurrentValueLowerThanZero(_fakeTaxProduct.Object);

            Action action = product.Validate;
            action.Should().Throw<ProductCurrentValueLowerOrEqualThanZeroException>();
        }

        [Test]
        public void Test_Product_ValidateWithNullTaxProduct_ShouldBeOk()
        {
            Product product = ObjectMother.GetInvalidProductWithNullTaxProduct();

            Action action = product.Validate;
            action.Should().Throw<ProductNullTaxProductException>();
        }

    }
}
