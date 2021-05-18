using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.Carriers
{
    [TestFixture]
    public class CarrierTests
    {
        private Carrier carrier;
        private Mock<Address> _fakeAddress;
        private Mock<CNPJ> _fakeCnpj;
        private Mock<CPF> _fakeCpf;

        [SetUp]
        public void Initalize()
        {
            carrier = new Carrier();
            _fakeAddress = new Mock<Address>();
            _fakeCnpj = new Mock<CNPJ>();
            _fakeCpf = new Mock<CPF>();

        }
        [Test]
        public void Test_Carrier_ValidateWithCarrierPhysical_ShouldBeOk()
        {
            _fakeCpf.Setup(c => c.Validate());
            carrier = ObjectMother.GetNewValidCarrierPhysical(_fakeAddress.Object, _fakeCpf.Object);
            carrier.Validate();

          
          
        }
        [Test]
        public void Test_Carrier_ValidateWithCarrierLegal_ShouldBeOk()
        {
            carrier = ObjectMother.GetNewValidCarrierLegal(_fakeAddress.Object, _fakeCnpj.Object);
            carrier.Validate();
        }

        [Test]
        public void Test_Carrier_ValidateWithPersonTypeInvalid_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidInvalidPersonType(_fakeAddress.Object, _fakeCpf.Object);
            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierUninformedPersonTypeException>();

        }

        [Test]
        public void Test_Carrier_ValidateCarrierPhysicalWithUninformedName_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierPhysicalNameEmpty(_fakeAddress.Object, _fakeCpf.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierUninformedNameException>();
        }

        [Test]
        public void Test_Carrier_ValidateCarrierLegalWithUninformedName_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierLegalNameEmpty(_fakeAddress.Object, _fakeCnpj.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierUninformedNameException>();
        }

        [Test]
        public void Test_Carrier_ValidateCarrierLegalWithCompanyNameUninformed_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierLegalCompanyNameEmpty(_fakeAddress.Object, _fakeCnpj.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierUninformedCompanyNameException>();
        }


        [Test]
        public void Test_Carrier_ValidateCarrierPhysicalWithNameLenghtOverflow_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierPhysicalNameLenghtOverflow(_fakeAddress.Object, _fakeCpf.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierNameLenghtOverflowException>();
        }

        [Test]
        public void Test_Carrier_ValidateCarrierLegalWithNameLenghtOverflow_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierLegalNameLenghtOverflow(_fakeAddress.Object, _fakeCnpj.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierNameLenghtOverflowException>();
        }

        [Test]
        public void Test_Carrier_ValidateCarrierLegalWithCompanyNameLenghtOverflowException_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierLegalCompanyNameLenghtOverflow(_fakeAddress.Object, _fakeCnpj.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierCompanyNameLenghtOverflowException>();
        }
        
        [Test]
        public void Test_Carrier_ValidateWithCpfNull_ShouldThrowException()
        {   

            carrier = ObjectMother.GetInvalidCarrierCPFNull( _fakeAddress.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierNullCPFException>();
        }

        [Test]
        public void Test_Carrier_ValidateWithCnpjNull_ShouldThrowException()
        { 
            carrier = ObjectMother.GetInvalidCarrierLegalNullCNPJ(_fakeAddress.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierNullCNPJException>();
        }

        [Test]
        public void Test_Carrier_ValidateCpfWithInvalidValue_ShouldThrowException()
        {
            _fakeCpf.Setup(cpf => cpf.Validate()).Throws<CPFInvalidValueException>();

            carrier = ObjectMother.GetInvalidCarrierCPF(_fakeAddress.Object,_fakeCpf.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CPFInvalidValueException>();
        }

        [Test]
        public void Test_Carrier_ValidateCcpjWithInvalidValue_ShouldThrowException()
        {
            _fakeCnpj.Setup(cnpj => cnpj.Validate()).Throws<CNPJInvalidValueException>();

            carrier = ObjectMother.GetInvalidCarrierCNPJ(_fakeAddress.Object, _fakeCnpj.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CNPJInvalidValueException>();
        }

        [Test]
        public void Test_Carrier_ValidateCpfWithUninformedValue_ShouldThrowException()
        {
            _fakeCpf.Setup(cpf => cpf.Validate()).Throws<CPFUninformedValueException>();

            carrier = ObjectMother.GetInvalidCarrierCPF(_fakeAddress.Object, _fakeCpf.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CPFUninformedValueException>();
        }

        [Test]
        public void Test_Carrier_ValidateCnpjWithUninformedValue_ShouldThrowException()
        {
            _fakeCnpj.Setup(cnpj => cnpj.Validate()).Throws<CNPJUninformedValueException>();

            carrier = ObjectMother.GetInvalidCarrierCNPJ(_fakeAddress.Object, _fakeCnpj.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CNPJUninformedValueException>();
        }

        [Test]
        public void Test_Carrier_ValidateWithUninformedStateRegistration_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierStateRegistrationEmpty(_fakeAddress.Object,_fakeCnpj.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierUninformedStateRegistrationException>();
        }
        [Test]
        public void Test_Carrier_ValidateWithStateRegistrationLenghtOverflow_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierStateRegistrationLenghtOverflow(_fakeAddress.Object,_fakeCnpj.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierStateRegistrationLenghtOverflowException>();
        }


        [Test]
        public void Test_Carrier_ValidateCarrierPhysicalWithUninformedFreightResponsability_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierPhysicalFreightResponsabilityEmpty(_fakeAddress.Object,_fakeCpf.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierUninformedFreightResponsabilityException>();
        }

        [Test]
        public void Test_Carrier_ValidateCarrierLegalWithUninformedFreightResponsability_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierLegalFreightResponsabilityEmpty(_fakeAddress.Object, _fakeCnpj.Object);

            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierUninformedFreightResponsabilityException>();
        }

        [Test]
        public void Test_Carrier_ValidateWithNullAddress_ShouldThrowException()
        {
            carrier = ObjectMother.GetInvalidCarrierLegalNullAddress(_fakeCnpj.Object);
            Action comparation = () => carrier.Validate();
            comparation.Should().Throw<CarrierNullAddressException>();
        }

    }
}
