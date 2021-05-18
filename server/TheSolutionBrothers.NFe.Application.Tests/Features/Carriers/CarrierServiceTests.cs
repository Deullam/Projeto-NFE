using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Application.Features.Carriers;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Queries;

namespace TheSolutionBrothers.NFe.Application.Tests.Features.Carriers
{
    [TestFixture]
   public  class CarrierServiceTests
   {

        private Mock<ICarrierRepository> _mockCarrierRepository;
        private Mock<IAddressRepository> _mockAddressRepository;
        private Mock<IInvoiceRepository> _mockInvoiceRepository;
        private Mock<IMapper> _mockIMapper;

        private CarrierService _service;
        private Address _address;
        private CNPJ _cnpj;
        private CPF _cpf;
        
        [SetUp]
        public void Initialize()
        {
            _address = ObjectMother.GetNewValidAddress();
            _cnpj = ObjectMother.GetValidCNPJ();
            _cpf = ObjectMother.GetValidCPF();

            _mockCarrierRepository = new Mock<ICarrierRepository>();
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockInvoiceRepository = new Mock<IInvoiceRepository>();
            _mockIMapper = new Mock<IMapper>();

            _service = new CarrierService(_mockCarrierRepository.Object, _mockAddressRepository.Object, _mockInvoiceRepository.Object, _mockIMapper.Object);
        }

        [Test]
        public void Test_CarrierService_Add_Physical_ShouldBeOk()
        {
            long expectedId = 1;

            Address expectedAddress = ObjectMother.GetExistentValidAddress();
            Carrier expectedCarrier = ObjectMother.GetExistentValidCarrierPhysical(expectedAddress, _cpf);
            
            AddressCommand newAddressCommand = ObjectMother.GetValidAddresCommand();
            CarrierRegisterCommand carrierCommand = ObjectMother.GetValidPhysicalCarrierRegisterCommand(newAddressCommand, _cpf);

            _mockIMapper.Setup(m => m.Map<Carrier>(carrierCommand)).Returns(expectedCarrier);
            _mockAddressRepository.Setup(a => a.Save(expectedAddress)).Returns(expectedAddress);
            _mockCarrierRepository.Setup(r => r.Add(expectedCarrier)).Returns(expectedCarrier);

            long result = _service.Add(carrierCommand);

            _mockIMapper.Verify(m => m.Map<Carrier>(carrierCommand));
            _mockAddressRepository.Verify(a => a.Save(expectedAddress));
            _mockCarrierRepository.Verify(r => r.Add(expectedCarrier));
            result.Should().Be(expectedId);
        }

        [Test]
        public void Test_CarrierService_Add_Legal_ShouldBeOk()
        {
            long expectedId = 1;

            Address expectedAddress = ObjectMother.GetExistentValidAddress();
            Carrier expectedCarrier = ObjectMother.GetExistentValidCarrierLegal(expectedAddress, _cnpj);


            AddressCommand newAddressCommand = ObjectMother.GetValidAddresCommand();
            CarrierRegisterCommand carrierCommand = ObjectMother.GetValidLegalCarrierRegisterCommand(newAddressCommand, _cnpj);

            _mockIMapper.Setup(m => m.Map<Carrier>(carrierCommand)).Returns(expectedCarrier);
            _mockAddressRepository.Setup(a => a.Save(expectedAddress)).Returns(expectedAddress);
            _mockCarrierRepository.Setup(r => r.Add(expectedCarrier)).Returns(expectedCarrier);

            long result = _service.Add(carrierCommand);

            _mockIMapper.Verify(m => m.Map<Carrier>(carrierCommand));
            _mockAddressRepository.Verify(a => a.Save(expectedAddress));
            _mockCarrierRepository.Verify(r => r.Add(expectedCarrier));
            result.Should().Be(expectedId);
        }

        [Test]
        public void Test_CarrierService_Add_Invalid_ShouldThrowException()
        {
            Carrier expectedCarrier = ObjectMother.GetInvalidCarrierLegalNullAddress(_cnpj);

            AddressCommand newAddressCommand = ObjectMother.GetValidAddresCommand();
            CarrierRegisterCommand carrierCommand = ObjectMother.GetValidLegalCarrierRegisterCommand(newAddressCommand, _cnpj);

            Address newAddress = ObjectMother.GetInvalidAddressWithUninformedCity();
            _mockIMapper.Setup(m => m.Map<Carrier>(carrierCommand)).Returns(expectedCarrier);
            
            Action action = () => { _service.Add(carrierCommand); };
            action.Should().Throw<CarrierNullAddressException>();

            _mockCarrierRepository.VerifyNoOtherCalls();
            _mockAddressRepository.VerifyNoOtherCalls();
        }
        
        [Test]
        public void Test_CarrierService_Update_Physical_ShouldBeOk()
        {
            bool success = true;

            Address expectedAddress = ObjectMother.GetExistentValidAddress();
            Carrier expectedCarrier = ObjectMother.GetExistentValidCarrierPhysical(expectedAddress, _cpf);

            AddressCommand newAddressCommand = ObjectMother.GetValidAddresCommand();
            CarrierUpdateCommand carrierCommand = ObjectMother.GetExistentValidPhysicalCarrierUpdateCommand(newAddressCommand, _cpf);

            _mockIMapper.Setup(m => m.Map<Carrier>(carrierCommand)).Returns(expectedCarrier);
            _mockCarrierRepository.Setup(r => r.GetById(carrierCommand.Id)).Returns(expectedCarrier);
            _mockCarrierRepository.Setup(r => r.Update(expectedCarrier)).Returns(success);

            bool result = _service.Update(carrierCommand);

            _mockIMapper.Verify(m => m.Map<Carrier>(carrierCommand));
            _mockCarrierRepository.Verify(r => r.Update(expectedCarrier));
            result.Should().BeTrue();
        }

        [Test]
        public void Test_CarrierService_Update_Legal_ShouldBeOk()
        {
            bool success = true;

            Address expectedAddress = ObjectMother.GetExistentValidAddress();
            Carrier expectedCarrier = ObjectMother.GetExistentValidCarrierLegal(expectedAddress, _cnpj);

            AddressCommand newAddressCommand = ObjectMother.GetValidAddresCommand();
            CarrierUpdateCommand carrierCommand = ObjectMother.GetExistentValidLegalCarrierUpdateCommand(newAddressCommand, _cnpj);

            _mockIMapper.Setup(m => m.Map<Carrier>(carrierCommand)).Returns(expectedCarrier);
            _mockCarrierRepository.Setup(r => r.GetById(carrierCommand.Id)).Returns(expectedCarrier);
            _mockCarrierRepository.Setup(r => r.Update(expectedCarrier)).Returns(success);

            bool result = _service.Update(carrierCommand);

            _mockIMapper.Verify(m => m.Map<Carrier>(carrierCommand));
            _mockCarrierRepository.Verify(r => r.Update(expectedCarrier));
            result.Should().BeTrue();
        }

        [Test]
        public void Test_CarrierService_Update_Invalid_ShouldThrowException()
        {
            Carrier expectedCarrier = ObjectMother.GetInvalidCarrierLegalNullAddress(_cnpj);

            AddressCommand newAddressCommand = ObjectMother.GetValidAddresCommand();
            CarrierUpdateCommand carrierCommand = ObjectMother.GetExistentValidLegalCarrierUpdateCommand(newAddressCommand, _cnpj);

            Address newAddress = ObjectMother.GetInvalidAddressWithUninformedCity();
            _mockCarrierRepository.Setup(r => r.GetById(expectedCarrier.Id)).Returns(expectedCarrier);
            _mockIMapper.Setup(m => m.Map<Carrier>(carrierCommand)).Returns(expectedCarrier);

            Action action = () => { _service.Update(carrierCommand); };
            action.Should().Throw<CarrierNullAddressException>();

            _mockCarrierRepository.Verify(r => r.GetById(expectedCarrier.Id));
            _mockCarrierRepository.VerifyNoOtherCalls();
            _mockAddressRepository.VerifyNoOtherCalls();
        }
        
        [Test]
        public void Test_CarrierService_Update_ShouldBeHandleNotFoundException()
        {
            Address expectedAddress = ObjectMother.GetExistentValidAddress();
            AddressCommand newAddressCommand = ObjectMother.GetValidAddresCommand();
            CarrierUpdateCommand carrierCommand = ObjectMother.GetExistentValidLegalCarrierUpdateCommand(newAddressCommand, _cnpj);
            Carrier expectedCarrier = ObjectMother.GetExistentValidCarrierLegal(expectedAddress, _cnpj);

            _mockIMapper.Setup(m => m.Map<Carrier>(carrierCommand)).Returns(expectedCarrier);
            _mockCarrierRepository.Setup(m => m.GetById(expectedCarrier.Id));

            Action accountAction = () => _service.Update(carrierCommand);

            accountAction.Should().Throw<NotFoundException>();
            _mockCarrierRepository.Verify(rp => rp.GetById(expectedCarrier.Id));
            _mockCarrierRepository.VerifyNoOtherCalls();
            _mockAddressRepository.VerifyNoOtherCalls();
        }
        
        [Test]
        public void Test_CarrierService_Get_ShouldBeOk()
        {
            long existentId = 1;
            Address address = ObjectMother.GetExistentValidAddress();
            Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(address, _cpf);

            Mock<AddessViewModel> adressViewModel = new Mock<AddessViewModel>();
            CarrierViewModel expectedCarrier = ObjectMother.GetPhysicalCarrierViewModel(adressViewModel.Object, _cpf);

            _mockCarrierRepository.Setup(m => m.GetById(It.IsAny<long>())).Returns(carrier);
            _mockIMapper.Setup(m => m.Map<CarrierViewModel>(It.IsAny<Carrier>())).Returns(expectedCarrier);

            CarrierViewModel result = _service.GetById(existentId);

            result.Id.Should().Be(existentId);
            _mockCarrierRepository.Verify(rp => rp.GetById(existentId));
        }

        [Test]
        public void Test_CarrierService_Get_NonexistentId_ShouldBeOk()
        {
            long unexistentId = 100;
            _mockCarrierRepository.Setup(m => m.GetById(unexistentId));

            CarrierViewModel result = _service.GetById(unexistentId);

            result.Should().BeNull();
            _mockCarrierRepository.Verify(rp => rp.GetById(unexistentId));
        }

        [Test]
        public void Test_CarrierService_GetAll_Query_ShouldBeOk()
        {
            int amount = 1;
            Address address = ObjectMother.GetExistentValidAddress();

            Carrier expectedCarrier = ObjectMother.GetExistentValidCarrierPhysical(address, _cpf);
            CarrierGetAllQuery query = ObjectMother.GetValidCarrierGetAllQuery();

            var repositoryMockValue = new List<Carrier>() { expectedCarrier }.AsQueryable();
            _mockCarrierRepository.Setup(m => m.GetAll(query.Size)).Returns(repositoryMockValue);

            var customers = _service.GetAll(query);

            _mockCarrierRepository.Verify(x => x.GetAll(query.Size));
            customers.Should().NotBeNull();
            customers.Should().HaveCount(amount);
        }

        [Test]
        public void Test_CarrierService_GetAll_ShouldBeOk()
        {
            int amount = 1;
            Address address = ObjectMother.GetExistentValidAddress();

            Carrier expectedCarrier = ObjectMother.GetExistentValidCarrierPhysical(address, _cpf);

            var repositoryMockValue = new List<Carrier>() { expectedCarrier }.AsQueryable();
            _mockCarrierRepository.Setup(m => m.GetAll()).Returns(repositoryMockValue);

            var customers = _service.GetAll();

            _mockCarrierRepository.Verify(x => x.GetAll());
            customers.Should().NotBeNull();
            customers.Should().HaveCount(amount);
        }

        [Test]
        public void Test_CarrierService_Delete_ShouldBeOk()
        {
            var deleteCommand = new CarrierDeleteCommand()
            {
                CarrierIds = new long[] { 1, 2 }
            };

            _mockCarrierRepository.Setup(r => r.GetById(It.IsAny<long>())).Returns(ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetExistentValidAddress(), _cnpj));
            _mockCarrierRepository.Setup(m => m.Remove(It.IsAny<long>())).Returns(true);
            _mockInvoiceRepository.Setup(i => i.GetByCarrier(It.IsAny<long>())).Returns(new List<Invoice>());
            _mockAddressRepository.Setup(m => m.Remove(It.IsAny<long>())).Returns(true);

            bool result = _service.Remove(deleteCommand);

            result.Should().BeTrue();
            _mockCarrierRepository.Verify(i => i.GetById(It.IsAny<long>()), Times.Exactly(deleteCommand.CarrierIds.Count()));
            _mockCarrierRepository.Verify(rp => rp.Remove(It.IsAny<long>()), Times.Exactly(deleteCommand.CarrierIds.Count()));
            _mockInvoiceRepository.Verify(i => i.GetByCarrier(It.IsAny<long>()), Times.Exactly(deleteCommand.CarrierIds.Count()));
        }

        [Test]
        public void Test_CarrierService_Delete_CarrierWithDependency_ShouldBeOk()
        {

            var deleteCommand = new CarrierDeleteCommand()
            {
                CarrierIds = new long[] { 1, 2 }
            };

            _mockInvoiceRepository.Setup(r => r.GetByCarrier(It.IsAny<long>())).Returns(new List<Invoice>() { It.IsAny<Invoice>() });
            _mockCarrierRepository.Setup(r => r.GetById(It.IsAny<long>())).Returns(ObjectMother.GetExistentValidCarrierLegal(ObjectMother.GetExistentValidAddress(), _cnpj));
            _mockInvoiceRepository.Setup(i => i.GetByCarrier(It.IsAny<long>())).Returns(new List<Invoice>() { new Invoice() });
            _mockCarrierRepository.Setup(m => m.Remove(It.IsAny<long>())).Returns(true);
            _mockAddressRepository.Setup(m => m.Remove(It.IsAny<long>())).Returns(true);

            bool result = _service.Remove(deleteCommand);

            result.Should().BeFalse();
            _mockInvoiceRepository.Verify(r => r.GetByCarrier(It.IsAny<long>()));
            _mockCarrierRepository.Verify(rp => rp.Remove(It.IsAny<long>()), Times.Never);
            _mockAddressRepository.Verify(rp => rp.Remove(It.IsAny<long>()), Times.Never);
        }

    }
}
