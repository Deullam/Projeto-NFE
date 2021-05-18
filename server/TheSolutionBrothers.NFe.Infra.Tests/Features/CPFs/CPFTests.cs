using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.Tests.Features.CPFs
{

    [TestFixture]
    public class CPFTests
    {

        [Test]
        public void Test_CPF_Equal_ShouldBeOk()
        {
            CPF cpf1 = ObjectMother.GetValidCPF();
            CPF cpf2 = ObjectMother.GetValidCPF();

            bool result = cpf1.Equals(cpf2);

            result.Should().BeTrue();
        }

        [Test]
        public void Test_CPF_EqualWithDifferentValues_ShouldBeOk()
        {
            CPF cpf1 = ObjectMother.GetValidCPF();
            CPF cpf2 = new CPF()
            {
                Value = "29089167030"
            };

            bool result = cpf1.Equals(cpf2);

            result.Should().BeFalse();
        }


        [Test]
        public void Test_CPF_EqualWithNullCPF_ShouldBeOk()
        {
            CPF cpf1 = ObjectMother.GetValidCPF();
            CPF cpf2 = null;

            bool result = cpf1.Equals(cpf2);

            result.Should().BeFalse();
        }

        [Test]
        public void Test_CPF_Validate_ShouldBeOk()
        {
            CPF cpf = ObjectMother.GetValidCPF();
            cpf.Validate();
        }

        [Test]
        public void Test_CPF_ValidateUninformedValue_ShouldBeOk()
        {
            CPF cpf = ObjectMother.GetInvalidCPFWithUninformedValue();

            Action action = cpf.Validate;
            action.Should().Throw<CPFUninformedValueException>();
        }

        [Test]
        public void Test_CPF_ValidateInvalidValue_ShouldBeOk()
        {
            CPF cpf = ObjectMother.GetInvalidCPFWithInvalidValue();

            Action action = cpf.Validate;
            action.Should().Throw<CPFInvalidValueException>();
        }

        [Test]
        public void Test_CPF_Formatted_ShouldBeOk()
        {
            string expectedCpf = "008.052.890-20";
            CPF cpf = ObjectMother.GetValidCPF();

            cpf.FormattedValue.Should().Be(expectedCpf);
        }

    }
}
