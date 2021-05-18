using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CNPJ;
using NUnit.Framework;

namespace TheSolutionBrothers.NFe.Infra.Tests.ExtensionMethods
{

    [TestFixture]
    public class CNPJValidatorTests
    {
        
        [Test]
        public void Test_CNPJValidator_FormatValidCpf_ShouldBeOk()
        {
            string validCnpj = "19834150000129";

            string formattedCnpj = "19.834.150/0001-29";

            validCnpj.Format().Should().Be(formattedCnpj);
        }
        
        [Test]
        public void Test_CNPJValidator_FormatInvalidCpf_ShouldBeOk()
        {
            string invalidCnpj = "19834150000124";

            invalidCnpj.Format().Should().BeNull();
        }

        [Test]
        public void Test_CNPJValidator_IsValid_ValidCpf_ShouldBeOk()
        {
            string validCnpj = "01854255000184";

            validCnpj.IsValid().Should().BeTrue();
        }

        [Test]
        public void Test_CNPJValidator_IsValid_ValidCpfWithFirstVerifyingDigitEqualThanZero_ShouldBeOk()
        {
            string validCnpj = "23812802000100";

            validCnpj.IsValid().Should().BeTrue();
        }

        [Test]
        public void Test_CNPJValidator_IsValid_ValidCpfWithSecondVerifyingDigitEqualThanZero_ShouldBeOk()
        {
            string validCnpj = "23812802000100";

            validCnpj.IsValid().Should().BeTrue();
        }
        
        [Test]
        public void Test_CNPJValidator_CalculateFirstVerifyDigit_ShouldBeOk()
        {
            int[] numbers = { 2,3,8,1,2,8,0,2,0,0,0,1,0,0 };
            int firstVerifyingDigit = 1;

            numbers.CalculateFirstVerifyingDigit().Should().Be(firstVerifyingDigit);
        }

        [Test]
        public void Test_CNPJValidator_CalculateSecondVerifyDigit_ShouldBeOk()
        {
            int[] numbers = { 2, 3, 8, 1, 2, 8, 0, 2, 0, 0, 0, 1, 0, 0 };
            int secondVerifyingDigit = 1;

            numbers.CalculateSecondVerifyingDigit().Should().Be(secondVerifyingDigit);
        }

        [Test]
        public void Test_CNPJValidator_IsValid_InvalidFirstVerifyingDigit_ShouldBeOk()
        {
            string invalidCnpj = "37827667000152";

            invalidCnpj.IsValid().Should().BeFalse();
        }
        
        [Test]
        public void Test_CNPJValidator_IsValid_InvalidSecondVerifyingDigit_ShouldBeOk()
        {
            string invalidCnpj = "37827667000178";

            invalidCnpj.IsValid().Should().BeFalse();
        }

        [Test]
        public void Test_CNPJValidator_IsValid_InvalidFirstVerifyingDigitWhenEqualThanZero_ShouldBeOk()
        {
            string invalidCnpj = "23812802000110";

            invalidCnpj.IsValid().Should().BeFalse();
        }

        [Test]
        public void Test_CNPJValidator_IsValid_InvalidSecondVerifyingDigitWhenEqualThanZero_ShouldBeOk()
        {
            string invalidCnpj = "23812802000101";

            invalidCnpj.IsValid().Should().BeFalse();
        }

        [Test]
        public void Test_CNPJValidator_IsValid_InvalidCnpjWithLengthDifferentThan14_ShouldBeOk()
        {
            string invalidCnpj = "6831395400019";

            invalidCnpj.IsValid().Should().BeFalse();
        }
        
        [Test]
        public void Test_CNPJValidator_ReplaceNonNumericCharacters_ShouldBeOk()
        {
            string cnpj = "19.834.150/0001-29 ";

            string result = cnpj.ReplaceNonNumericCharacters();

            result.Contains(".").Should().BeFalse();
            result.Contains("-").Should().BeFalse();
            result.Contains("/").Should().BeFalse();
        }
        
        [Test]
        public void Test_CNPJValidator_ParseToArray_ShouldBeOk()
        {
            string cnpj = "40755737000139";

            int[] result = cnpj.ParseToArray();

            result.Length.Should().Be(14);
            for (int index = 0; index < result.Length; index++)
            {
                result[index].Should().Be(int.Parse(cnpj[index].ToString()));
            }
        }

    }
}
