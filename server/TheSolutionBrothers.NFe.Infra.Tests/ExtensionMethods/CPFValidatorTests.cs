using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CPF;
using NUnit.Framework;

namespace TheSolutionBrothers.NFe.Infra.Tests.ExtensionMethods
{

    [TestFixture]
    public class CPFValidatorTests
    {
        
        [Test]
        public void Test_CPFValidator_FormatValidCpf_ShouldBeOk()
        {
            string validCpf = "70188867082";

            string formattedCpf = "701.888.670-82";

            validCpf.Format().Should().Be(formattedCpf);
        }
        
        [Test]
        public void Test_CPFValidator_FormatInvalidCpf_ShouldBeOk()
        {
            string invalidCpf = "32217362372";

            invalidCpf.Format().Should().BeNull();
        }

        [Test]
        public void Test_CPFValidator_IsValid_ValidCpf_ShouldBeOk()
        {
            string validCpf = "70188867082";

            validCpf.IsValid().Should().BeTrue();
        }

        [Test]
        public void Test_CPFValidator_IsValid_ValidCpfWithFirstVerifyingDigitEqualThanZero_ShouldBeOk()
        {
            string validCpf = "22723714004";

            validCpf.IsValid().Should().BeTrue();
        }

        [Test]
        public void Test_CPFValidator_IsValid_ValidCpfWithSecondVerifyingDigitEqualThanZero_ShouldBeOk()
        {
            string validCpf = "26603679060";

            validCpf.IsValid().Should().BeTrue();
        }
        
        [Test]
        public void Test_CPFValidator_CalculateFirstVerifyDigit_ShouldBeOk()
        {
            int[] numbers = { 2,2,7,2,3,7,1,4,0,0,4 };
            int firstVerifyingDigit = 1;

            numbers.CalculateFirstVerifyDigit().Should().Be(firstVerifyingDigit);
        }

        [Test]
        public void Test_CPFValidator_CalculateSecondVerifyDigit_ShouldBeOk()
        {
            int[] numbers = { 2,6,6,0,3,6,7,9,0,6,0 };
            int secondVerifyingDigit = 1;

            numbers.CalculateSecondVerifyDigit().Should().Be(secondVerifyingDigit);
        }

        [Test]
        public void Test_CPFValidator_IsValid_InvalidFirstVerifyingDigit_ShouldBeOk()
        {
            string invalidCpf = "70188867081";

            invalidCpf.IsValid().Should().BeFalse();
        }
        
        [Test]
        public void Test_CPFValidator_IsValid_InvalidSecondVerifyingDigit_ShouldBeOk()
        {
            string invalidCpf = "70188867042";

            invalidCpf.IsValid().Should().BeFalse();
        }

        [Test]
        public void Test_CPFValidator_IsValid_InvalidFirstVerifyingDigitWhenEqualThanZero_ShouldBeOk()
        {
            string invalidCpf = "22723714014";

            invalidCpf.IsValid().Should().BeFalse();
        }

        [Test]
        public void Test_CPFValidator_IsValid_InvalidSecondVerifyingDigitWhenEqualThanZero_ShouldBeOk()
        {
            string invalidCpf = "26603679061";

            invalidCpf.IsValid().Should().BeFalse();
        }

        [Test]
        public void Test_CPFValidator_IsValid_InvalidCpfWithLengthDifferentThan11_ShouldBeOk()
        {
            string invalidCpf = "2272371400";

            invalidCpf.IsValid().Should().BeFalse();
        }

        [Test]
        public void Test_CPFValidator_IsValid_InvalidCpfWithEqualNumber_ShouldBeOk()
        {
            string invalidCpf = "11111111111";

            invalidCpf.IsValid().Should().BeFalse();
        }

        [Test]
        public void Test_CPFValidator_IsValid_InvalidCpfWithSequence_ShouldBeOk()
        {
            string invalidCpf = "12345678909";

            invalidCpf.IsValid().Should().BeFalse();
        }

        [Test]
        public void Test_CPFValidator_HasEqualNumbers_EqualNumbers_ShouldBeOk()
        {
            string cpf = "11111111111";

            cpf.HasEqualNumbers().Should().BeTrue();
        }

        [Test]
        public void Test_CPFValidator_HasEqualNumbers_NotEqualNumbers_ShouldBeOk()
        {
            string cpf = "43723848758";

            cpf.HasEqualNumbers().Should().BeFalse();
        }

        [Test]
        public void Test_CPFValidator_IsSequence_Sequence_ShouldBeOk()
        {
            string cpf = "12345678909";

            cpf.IsSequence().Should().BeTrue();
        }

        [Test]
        public void Test_CPFValidator_IsSequence_NotSequence_ShouldBeOk()
        {
            string cpf = "43723848758";

            cpf.IsSequence().Should().BeFalse();
        }
        
        [Test]
        public void Test_CPFValidator_ReplaceNonNumericCharacters_ShouldBeOk()
        {
            string cpf = "437.238.487-58 ";

            string result = cpf.ReplaceNonNumericCharacters();

            result.Contains(".").Should().BeFalse();
            result.Contains("-").Should().BeFalse();
        }
        
        [Test]
        public void Test_CPFValidator_ParseToArray_ShouldBeOk()
        {
            string cpf = "43723848758";

            int[] result = cpf.ParseToArray();

            result.Length.Should().Be(11);
            for (int index = 0; index < result.Length; index++)
            {
                result[index].Should().Be(int.Parse(cpf[index].ToString()));
            }
        }

    }
}
