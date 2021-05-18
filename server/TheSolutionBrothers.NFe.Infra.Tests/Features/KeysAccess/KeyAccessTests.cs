using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.Tests.Features.KeysAccess
{

    [TestFixture]
    public class KeyAccessTests
    {

        [Test]
        public void Test_KeyAccess_Validate_ShouldBeOk()
        {
            KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
            keyAccess.Validate();
        }

        [Test]
        public void Test_KeyAccess_ValidateWithUninformedValue_ShouldThrowException()
        {
            KeyAccess keyAccess = ObjectMother.GetInvalidKeyAccessWithUninformedValue();

            Action action = keyAccess.Validate;
            action.Should().Throw<KeyAccessUninformedValueException>();
        }

        [Test]
        public void Test_KeyAccess_ValidateWithValueLengthDifferentThan44_ShouldThrowException()
        {
            KeyAccess keyAccess = ObjectMother.GetInvalidKeyAccessWithValueLengthDifferentThan44();

            Action action = keyAccess.Validate;
            action.Should().Throw<KeyAccessValueLengthDifferentThan44Exception>();
        }

        [Test]
        public void Test_KeyAccess_GenerateKeyAccess_ShouldBeOk()
        {
            int number = 1;
            string expectedValue = "00000000000000000000000000000000000000000001";

            KeyAccess keyAccess = new KeyAccess();
            keyAccess.GenerateKeyAccess(number);

            keyAccess.Validate();
            keyAccess.Value.Should().Be(expectedValue);
        }
        
        [Test]
        public void Test_KeyAccess_GenerateKeyAccessWithNegativeNumber_ShouldThrowException()
        {
            int number = -11;

            KeyAccess keyAccess = new KeyAccess();
            Action action = () => keyAccess.GenerateKeyAccess(number);
            action.Should().Throw<KeyAccessNonPositiveNumberException>();
        }


    }

}
