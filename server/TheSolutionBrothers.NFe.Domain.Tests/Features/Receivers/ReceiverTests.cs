using System;
using NUnit.Framework;
using FluentAssertions;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using Moq;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.Receivers
{
	[TestFixture]
	public class ReceiverTests
	{
		private Mock<Address> _fakeAddress;
		private Mock<CNPJ> _fakeCnpj;
		private Mock<CPF> _fakeCpf;

		[SetUp]
		public void Initialize()
		{
			_fakeAddress = new Mock<Address>();
			_fakeCnpj = new Mock<CNPJ>();
			_fakeCpf = new Mock<CPF>();
		}

		[Test]
		public void Test_Receiver_ValidateWithReceiverPhysical_ShouldBeOk()
		{
			Receiver receiver = ObjectMother.GetNewValidPhysicalReceiver(_fakeAddress.Object, _fakeCpf.Object);
			_fakeCpf.Setup(a => a.Validate());
			receiver.Validate();
		}

		[Test]
		public void Test_Receiver_ValidateWithReceiverLegal_ShouldBeOk()
		{
			Receiver receiver = ObjectMother.GetNewValidLegalReceiver(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate());
			receiver.Validate();
		}

		[Test]
		public void Test_Receiver_ValidateCpfWithUninformedValue_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverEmptyCpf(_fakeAddress.Object, _fakeCpf.Object);
			_fakeCpf.Setup(a => a.Validate()).Throws<CPFUninformedValueException>(); 
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<CPFUninformedValueException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithNullCpf_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverNullCpf(_fakeAddress.Object);
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverNullCpfException>();
		}

		[Test]
		public void Test_Receiver_ValidateCpfWithInvalidValue_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(_fakeAddress.Object, _fakeCpf.Object);
			_fakeAddress.Setup(a => a.Validate());
			_fakeCpf.Setup(a => a.Validate()).Throws<CPFInvalidValueException>();
			Action action = receiver.Validate;
			action.Should().Throw<CPFInvalidValueException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithUninformedName_ShouldThrowException()
		{
			_fakeCpf.Setup(a => a.Validate());
			Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverEmptyName(_fakeAddress.Object, _fakeCpf.Object);
			_fakeAddress.Setup(a => a.Validate());
			_fakeCpf.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverUninformedNameException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithNullName_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverNullName(_fakeAddress.Object, _fakeCpf.Object);
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverUninformedNameException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithNameLengthOverflow_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidPhysicalReceiverNameLength(_fakeAddress.Object, _fakeCpf.Object);
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverNameLengthOverflowException>();
		}

		[Test]
		public void Test_Receiver_ValidateCnpjWithUninformedValue_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyCnpj(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate()).Throws<CPFUninformedValueException>();
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<CPFUninformedValueException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithNullCnpj_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullCnpj(_fakeAddress.Object);
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverNullCnpjException>();
		}

		[Test]
		public void Test_Receiver_ValidateCnpjWithInvalidValue_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeAddress.Setup(a => a.Validate());
			_fakeCnpj.Setup(a => a.Validate()).Throws<CNPJInvalidValueException>();
			Action action = receiver.Validate;
			action.Should().Throw<CNPJInvalidValueException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithUninformedCompanyName_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyCompanyName(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate());
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverUninformedCompanyNameException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithNullCompanyName_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullCompanyName(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate());
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverUninformedCompanyNameException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithCompanyNameLengthOverflow_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidLegalReceiverCompanyNameLengthOverflow(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate());
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverCompanyNameLengthOverflowException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithUninformedStateRegistration_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidLegalReceiverEmptyStateRegistration(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate());
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverUninformedStateRegistrationException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithNullStateRegistration_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullStateRegistration(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate());
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverUninformedStateRegistrationException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithStateRegistrationLengthOverflow_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidLegalReceiverStateRegistrationLengthOverflow(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate());
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverStateRegistrationLengthOverflowException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithNullAddress_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetInvalidLegalReceiverNullAddress(_fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate());
			_fakeAddress.Setup(a => a.Validate());
			Action action = receiver.Validate;
			action.Should().Throw<ReceiverUninformedAddressException>();
		}

		[Test]
		public void Test_Receiver_ValidateWithInvalidAddress_ShouldThrowException()
		{
			Receiver receiver = ObjectMother.GetNewValidLegalReceiver(_fakeAddress.Object, _fakeCnpj.Object);
			_fakeCnpj.Setup(a => a.Validate());
			_fakeAddress.Setup(a => a.Validate()).Throws<AddressUninformedCityException>();
			Action action = receiver.Validate;
			action.Should().Throw<AddressUninformedCityException>();
		}

	}
}
