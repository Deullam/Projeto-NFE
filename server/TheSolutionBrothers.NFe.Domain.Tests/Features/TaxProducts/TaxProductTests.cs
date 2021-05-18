using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using NUnit.Framework;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.TaxProducts
{

    [TestFixture]
    public class TaxProductTests
    {

        [Test]
        public void Test_TaxProduct_Validate_ShouldBeOk()
        {
            TaxProduct taxProduct = ObjectMother.GetValidTaxProduct();
            //taxProduct.Validate();
        }

        [Test]
        public void Test_TaxProduct_IcmsAliquotEqualThan4Percent_ShouldBeOk()
        {
            double expectedIcmsAliquot = 0.04;

            TaxProduct taxProduct = ObjectMother.GetValidTaxProduct();
            taxProduct.IcmsAliquot.Should().Be(expectedIcmsAliquot);
        }

        [Test]
        public void Test_TaxProduct_IpiAliquotEqualThan10Percent_ShouldBeOk()
        {
            double expectedIpiAliquot = 0.1;

            TaxProduct taxProduct = ObjectMother.GetValidTaxProduct();
            taxProduct.IpiAliquot.Should().Be(expectedIpiAliquot);
        }

    }

}
