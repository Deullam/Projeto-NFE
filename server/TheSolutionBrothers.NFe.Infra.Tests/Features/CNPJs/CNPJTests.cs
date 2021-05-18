using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.Tests.Features.CNPJs
{

    [TestFixture]
    public class CNPJTests
    {
        
        [Test]
        public void Test_CNPJ_Equal_ShouldBeOk()
        {
            CNPJ cnpj1 = ObjectMother.GetValidCNPJ();
            CNPJ cnpj2 = ObjectMother.GetValidCNPJ();

            bool result = cnpj1.Equals(cnpj2);

            result.Should().BeTrue();
        }

        [Test]
        public void Test_CNPJ_EqualWithDifferentValues_ShouldBeOk()
        {
            CNPJ cnpj1 = ObjectMother.GetValidCNPJ();
            CNPJ cnpj2 = new CNPJ()
            {
                Value = "09764460000160"
            };

            bool result = cnpj1.Equals(cnpj2);

            result.Should().BeFalse();
        }


        [Test]
        public void Test_CNPJ_EqualWithNullCNPJ_ShouldBeOk()
        {
            CNPJ cnpj1 = ObjectMother.GetValidCNPJ();
            CNPJ cnpj2 = null;

            bool result = cnpj1.Equals(cnpj2);

            result.Should().BeFalse();
        }

        [Test]
        public void Test_CNPJ_Validate_ShouldBeOk()
        {
            CNPJ cnpj = ObjectMother.GetValidCNPJ();
            cnpj.Validate();
        }

        [Test]
        public void Test_CNPJ_ValidateUninformedValue_ShouldBeOk()
        {
            CNPJ cnpj = ObjectMother.GetInvalidCNPJWithUninformedValue();

            Action action = cnpj.Validate;
            action.Should().Throw<CNPJUninformedValueException>();
        }

        [Test]
        public void Test_CNPJ_ValidateInvalidValue_ShouldBeOk()
        {
            CNPJ cnpj = ObjectMother.GetInvalidCNPJWithInvalidValue();

            Action action = cnpj.Validate;
            action.Should().Throw<CNPJInvalidValueException>();
        }

        [Test]
        public void Test_CNPJ_Formatted_ShouldBeOk()
        {
            string expectedCnpj = "25.103.755/0001-42";
            CNPJ cnpj = ObjectMother.GetValidCNPJ();

            cnpj.FormattedValue.Should().Be(expectedCnpj);
        }

    }
}
