using System;
using NUnit.Framework;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using FluentAssertions;
using Moq;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.Senders
{
    [TestFixture]
    public class SenderTests
    {
        private Mock<CNPJ> _fakeCnpj;
        private Mock<Address> _fakeAddress;

        [SetUp]
        public void Initialize()
        {
            _fakeCnpj = new Mock<CNPJ>();
            _fakeAddress = new Mock<Address>();
        }
        [Test]
        public void Test_Sender_Validate_ShouldBeOk()
        {
            _fakeCnpj.Setup(c => c.Validate());
            Sender sender = ObjectMother.GetNewValidSender(_fakeAddress.Object ,_fakeCnpj.Object);
            sender.Validate();
        }

        [Test]
        public void Test_Sender_ValidateWIthUninformedFancyName_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithUninformedFancyName(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderUninformedFancyNameException>();
        }

        [Test]
        public void Test_Sender_ValidateWithUninformedCompanyName_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithUninformedCompanyName(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderUninformedCompanyNameException>();
        }

        [Test]
        public void Test_Sender_ValidateWithUninformedStateRegistration_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithUninformedStateRegistration(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderUninformedStateRegistrationException>();
        }

        [Test]
        public void Test_Sender_ValidateWithUninformedMunicipalRegistration_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithUninformedMunicipalRegistration(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderUninformedMunicipalRegistrationException>();
        }

        [Test]
        public void Test_Sender_ValidateWithNullCnpj_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithNullCnpj(_fakeAddress.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderNullCnpjException>();
        }

        [Test]
        public void Test_Sender_ValidateCnpjWithUninformedValue_ShouldThrowException()
        {
            _fakeCnpj.Setup(c => c.Validate()).Throws<CNPJUninformedValueException>();
            Sender sender = ObjectMother.GetInvalidSenderWithCnpjValueNull(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<CNPJUninformedValueException>();
        }

        [Test]
        public void Test_Sender_ValidateCnpjWithInvalidValue_ShouldThrowException()
        {
            _fakeCnpj.Setup(c => c.Validate()).Throws<CNPJInvalidValueException>();
            Sender sender = ObjectMother.GetInvalidSenderCnpjWithInvalidValue(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<CNPJInvalidValueException>();
        }

        [Test]
        public void Test_Sender_ValidateWithFancyNameLengthOverflow_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithFancyNameLenghtOverflow(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderFancyNameLenghtOverflowException>();
        }

        [Test]
        public void Test_Sender_ValidateWithCompanyNameLengthOverflow_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithCompanyNameLenghtOverflow(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderCompanyNameLenghtOverflowException>();
        }

        [Test]
        public void Test_Sender_ValidateWithStateRegistrationLengthOverflow_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithStateRegistrationLenghtOverflow(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderStateRegistrationLenghtOverflowException>();
        }

        [Test]
        public void Test_Sender_ValidateWithMunicipalRegistrationLengthOverflow_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithMunicipalRegistrationLenghtOverflow(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderMunicipalRegistrationLenghtOverflowException>();
        }

        [Test]
        public void Test_Sender_ValidateWithNullAddress_ShouldThrowException()
        {
            Sender sender = ObjectMother.GetInvalidSenderWithNullAddress(_fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<SenderNullAddressException>();
        }

        [Test]
        public void Test_Sender_ValidateWithInvalidAddress_ShouldThrowException()
        {
            _fakeAddress.Setup(a => a.Validate()).Throws<AddressUninformedCityException>();
            Sender sender = ObjectMother.GetInvalidSenderWithInvalidAddress(_fakeAddress.Object, _fakeCnpj.Object);
            Action action = () => sender.Validate();
            action.Should().Throw<AddressUninformedCityException>();
        }
    }
}
