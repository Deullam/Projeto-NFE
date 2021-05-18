using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Moq;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Application.Features.Receivers;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Queries;
using System.Linq;
using TheSolutionBrothers.NFe.Application.Mappers;

namespace TheSolutionBrothers.NFe.Application.Tests.Features.Receivers
{
    [TestFixture]
    public class ReceiverServiceTests
    {
        private Mock<IReceiverRepository> _mockRepositoryReceiver;
        private Mock<IAddressRepository> _mockRepositoryAddress;
        private Mock<IInvoiceRepository> _mockRepositoryInvoice;
        private Mock<IMapper> _mockIMapper;

        private ReceiverService _receiverService;
        private CPF _validCpf;
        private CNPJ _validCnpj;

        private Address mappedAddress;
        private Receiver mappedLegalReceiver;
        private Receiver mappedPhysicalReceiver;
        private Address savedAddress;
        private Receiver legalReceiverSaved;
        private Receiver physicalReceiverSaved;
        private AddressCommand addressCommand;
        private ReceiverRegisterCommand receiverLegalRegisterCommand;
        private ReceiverRegisterCommand receiverPhysicalRegisterCommand;

        private ReceiverUpdateCommand receiverPhysicalUpdateCommand;
        private ReceiverUpdateCommand receiverLegalUpdateCommand;

        [SetUp]
        public void Initialize()
        {
            _mockRepositoryReceiver = new Mock<IReceiverRepository>();
            _mockRepositoryAddress = new Mock<IAddressRepository>();
            _mockRepositoryInvoice = new Mock<IInvoiceRepository>();
            _mockIMapper = new Mock<IMapper>();

            _receiverService = new ReceiverService(_mockRepositoryReceiver.Object, _mockRepositoryAddress.Object, _mockRepositoryInvoice.Object, _mockIMapper.Object);

            _validCpf = ObjectMother.GetValidCPF();
            _validCnpj = ObjectMother.GetValidCNPJ();

            mappedAddress = ObjectMother.GetNewValidAddress();
            mappedLegalReceiver = ObjectMother.GetNewValidLegalReceiver(mappedAddress, _validCnpj);
            mappedPhysicalReceiver = ObjectMother.GetNewValidPhysicalReceiver(mappedAddress, _validCpf);

            savedAddress = ObjectMother.GetExistentValidAddress();
            legalReceiverSaved = ObjectMother.GetExistentValidLegalReceiver(mappedAddress, _validCnpj);
            physicalReceiverSaved = ObjectMother.GetExistentValidLegalReceiver(mappedAddress, _validCnpj);

            addressCommand = ObjectMother.GetValidAddresCommand();
            receiverLegalRegisterCommand = ObjectMother.GetValidLegalReceiverRegisterCommand(addressCommand, _validCnpj);
            receiverPhysicalRegisterCommand = ObjectMother.GetValidPhysicalReceiverRegisterCommand(addressCommand, _validCpf);

            receiverPhysicalUpdateCommand = ObjectMother.GetExistentValidPhysicalReceiverUpdateCommand(addressCommand, _validCpf);
            receiverLegalUpdateCommand = ObjectMother.GetExistentValidLegalReceiverUpdateCommand(addressCommand, _validCnpj);
        }


        #region ADD
        [Test]
        public void Test_ReceiverService_AddPhysical_ShouldBeOk()
        {
            long expectedId = 1;
            
            _mockIMapper.Setup(m => m.Map<Receiver>(receiverPhysicalRegisterCommand)).Returns(mappedPhysicalReceiver);
            _mockRepositoryAddress.Setup(a => a.Save(mappedAddress)).Returns(savedAddress);
            _mockRepositoryReceiver.Setup(r => r.Add(mappedPhysicalReceiver)).Returns(physicalReceiverSaved);

            long result = _receiverService.Add(receiverPhysicalRegisterCommand);

            _mockIMapper.Verify(m => m.Map<Receiver>(receiverPhysicalRegisterCommand));
            _mockRepositoryAddress.Verify(a => a.Save(mappedAddress));
            _mockRepositoryReceiver.Verify(r => r.Add(mappedPhysicalReceiver));
            result.Should().Be(expectedId);
        }

        [Test]
        public void Test_ReceiverService_AddLegal_ShouldBeOk()
        {
            long expectedId = 1;
            _mockIMapper.Setup(m => m.Map<Receiver>(receiverLegalRegisterCommand)).Returns(mappedLegalReceiver);
            _mockRepositoryAddress.Setup(a => a.Save(mappedAddress)).Returns(savedAddress);
            _mockRepositoryReceiver.Setup(r => r.Add(mappedLegalReceiver)).Returns(legalReceiverSaved);

            long result = _receiverService.Add(receiverLegalRegisterCommand);

            _mockIMapper.Verify(m => m.Map<Receiver>(receiverLegalRegisterCommand));
            _mockRepositoryAddress.Verify(a => a.Save(mappedAddress));
            _mockRepositoryReceiver.Verify(r => r.Add(mappedLegalReceiver));
            result.Should().Be(expectedId);
        }

        [Test]
        public void Test_ReceiverService_Add_ReceiverLegalWithAddressInvalid_ShouldThrowException()
        {
            Receiver invalidReceiverMapped = ObjectMother.GetInvalidLegalReceiverNullAddress(_validCnpj);

            _mockIMapper.Setup(m => m.Map<Receiver>(receiverLegalRegisterCommand)).Returns(invalidReceiverMapped);

            Action action = () => { _receiverService.Add(receiverLegalRegisterCommand); };
            action.Should().Throw<ReceiverUninformedAddressException>();

            _mockIMapper.Verify(m => m.Map<Receiver>(receiverLegalRegisterCommand));
            _mockRepositoryReceiver.VerifyNoOtherCalls();
            _mockRepositoryAddress.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ReceiverService_Add_ReceiverPhysicalWithAddressInvalid_ShouldThrowException()
        {
            Receiver invalidReceiverMapped = ObjectMother.GetInvalidPhysicalReceiverNullAddress(_validCpf);

            _mockIMapper.Setup(m => m.Map<Receiver>(receiverPhysicalRegisterCommand)).Returns(invalidReceiverMapped);

            Action action = () => { _receiverService.Add(receiverPhysicalRegisterCommand); };
            action.Should().Throw<ReceiverUninformedAddressException>();

            _mockIMapper.Verify(m => m.Map<Receiver>(receiverPhysicalRegisterCommand));
            _mockRepositoryReceiver.VerifyNoOtherCalls();
            _mockRepositoryAddress.VerifyNoOtherCalls();
        }
        #endregion

        #region UPDATE
        [Test]
        public void Test_ReceiverService_Update_ReceiverPhysical_ShouldBeOk()
        {
            var isUpdated = true;

            _mockIMapper.Setup(m => m.Map<Receiver>(receiverPhysicalUpdateCommand)).Returns(mappedPhysicalReceiver);
            _mockRepositoryReceiver.Setup(r => r.GetById(It.IsAny<long>())).Returns(mappedPhysicalReceiver);
            _mockRepositoryReceiver.Setup(m => m.Update(mappedPhysicalReceiver)).Returns(isUpdated);

            bool result = _receiverService.Update(receiverPhysicalUpdateCommand);

            result.Should().BeTrue();
            _mockRepositoryReceiver.Verify(rp => rp.Update(mappedPhysicalReceiver));
        }

        [Test]
        public void Test_ReceiverService_Update_ReceiverLegal_ShouldBeOk()
        {
            var isUpdated = true;

            _mockIMapper.Setup(m => m.Map<Receiver>(receiverLegalUpdateCommand)).Returns(mappedLegalReceiver);
            _mockRepositoryReceiver.Setup(r => r.GetById(It.IsAny<long>())).Returns(mappedLegalReceiver);
            _mockRepositoryReceiver.Setup(m => m.Update(mappedLegalReceiver)).Returns(isUpdated);

            bool result = _receiverService.Update(receiverLegalUpdateCommand);

            result.Should().BeTrue();
            _mockRepositoryReceiver.Verify(rp => rp.Update(mappedLegalReceiver));
            _mockRepositoryAddress.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ReceiverService_Update_InvalidReceiverLegal_ShouldThrowException()
        {
            bool isUpdated = false;

            Receiver invalidReceiverMapped = ObjectMother.GetInvalidLegalReceiverEmptyCompanyName(mappedAddress, _validCnpj);

            _mockIMapper.Setup(m => m.Map<Receiver>(receiverLegalUpdateCommand)).Returns(invalidReceiverMapped);
            _mockRepositoryReceiver.Setup(r => r.GetById(It.IsAny<long>())).Returns(invalidReceiverMapped);
            _mockRepositoryReceiver.Setup(m => m.Update(invalidReceiverMapped)).Returns(isUpdated);

            Action action = () => { _receiverService.Update(receiverLegalUpdateCommand); };

            action.Should().Throw<ReceiverUninformedCompanyNameException>();
            _mockRepositoryAddress.VerifyNoOtherCalls();
        }


        [Test]
        public void Test_ReceiverService_Update_InvalidReceiverPhysical_ShouldThrowException()
        {
            bool isUpdated = false;

            Receiver invalidReceiverMapped = ObjectMother.GetInvalidPhysicalReceiverEmptyName(mappedAddress, _validCpf);

            _mockIMapper.Setup(m => m.Map<Receiver>(receiverPhysicalUpdateCommand)).Returns(invalidReceiverMapped);
            _mockRepositoryReceiver.Setup(r => r.GetById(It.IsAny<long>())).Returns(invalidReceiverMapped);
            _mockRepositoryReceiver.Setup(m => m.Update(invalidReceiverMapped)).Returns(isUpdated);

            Action action = () => { _receiverService.Update(receiverPhysicalUpdateCommand); };

            action.Should().Throw<ReceiverUninformedNameException>();
            _mockRepositoryAddress.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ReceiverService_Update_NonExistentReceiver_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Receiver>(receiverLegalUpdateCommand)).Returns(mappedLegalReceiver);
            _mockRepositoryReceiver.Setup(r => r.GetById(It.IsAny<long>()));

            Action action = () => { _receiverService.Update(receiverLegalUpdateCommand); };
            action.Should().Throw<NotFoundException>();

            _mockRepositoryAddress.VerifyNoOtherCalls();
        }
        #endregion

        [Test]
        public void Test_ReceiverService_Get_ShouldBeOk()
        {
            long existentId = 1;

            Mock<AddessViewModel> adressViewModel = new Mock<AddessViewModel>();
            ReceiverViewModel receiverViewModel = ObjectMother.GetPhysicalReceiverViewModel(adressViewModel.Object, _validCpf);

            _mockRepositoryReceiver.Setup(m => m.GetById(It.IsAny<long>())).Returns(legalReceiverSaved);
            _mockIMapper.Setup(m => m.Map<ReceiverViewModel>(It.IsAny<Receiver>())).Returns(receiverViewModel);

            ReceiverViewModel result = _receiverService.GetById(existentId);

            result.Id.Should().Be(existentId);
            _mockRepositoryReceiver.Verify(rp => rp.GetById(existentId));
            _mockRepositoryAddress.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ReceiverService_Get_NonexistentId_ShouldBeOk()
        {
            long unexistentId = 100;
            _mockRepositoryReceiver.Setup(m => m.GetById(unexistentId));

            ReceiverViewModel result = _receiverService.GetById(unexistentId);

            result.Should().BeNull();
            _mockRepositoryReceiver.Verify(rp => rp.GetById(unexistentId));
        }

        [Test]
        public void Test_ReceiverService_GetAll_Query_ShouldBeOk()
        {
            int amount = 1;

            ReceiverGetAllQuery query = ObjectMother.GetValidReceiverGetAllQuery();
            var repositoryMockValue = new List<Receiver>() { legalReceiverSaved }.AsQueryable();
            _mockRepositoryReceiver.Setup(m => m.GetAll(query.Size)).Returns(repositoryMockValue);

            var receivers = _receiverService.GetAll(query);

            _mockRepositoryReceiver.Verify(x => x.GetAll(query.Size));
            receivers.Should().NotBeNull();
            receivers.Should().HaveCount(amount);
            _mockRepositoryAddress.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ReceiverService_GetAll_ShouldBeOk()
        {
            int amount = 1;

            var repositoryMockValue = new List<Receiver>() { legalReceiverSaved }.AsQueryable();
            _mockRepositoryReceiver.Setup(m => m.GetAll()).Returns(repositoryMockValue);

            var customers = _receiverService.GetAll();

            _mockRepositoryReceiver.Verify(x => x.GetAll());
            customers.Should().NotBeNull();
            customers.Should().HaveCount(amount);
            _mockRepositoryAddress.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ReceiverService_Delete_False_ShouldBeOk()
        {
            var deleteCommand = ObjectMother.GetReceiverDeleteCommand();

            legalReceiverSaved.AddressId = legalReceiverSaved.Address.Id;
            _mockRepositoryReceiver.Setup(r => r.GetById(It.IsAny<long>())).Returns(legalReceiverSaved);
            _mockRepositoryAddress.Setup(m => m.Remove(legalReceiverSaved.AddressId));
            _mockRepositoryInvoice.Setup(m => m.GetByReceiver(It.IsAny<long>())).Returns(new List<Invoice>() { new Invoice() });

            var result = _receiverService.Remove(deleteCommand);

            result.Should().BeFalse();
            _mockRepositoryReceiver.Verify(rp => rp.GetById(It.IsAny<long>()));
            _mockRepositoryInvoice.Verify(rp => rp.GetByReceiver(It.IsAny<long>()));
        }

        [Test]
        public void Test_ReceiverService_Delete_True_ShouldBeOk()
        {
            var isAllUpdated = true;

            var deleteCommand = ObjectMother.GetReceiverDeleteCommand();

            _mockRepositoryReceiver.Setup(r => r.GetById(It.IsAny<long>())).Returns(legalReceiverSaved);
            _mockRepositoryReceiver.Setup(m => m.Remove(It.IsAny<long>())).Returns(isAllUpdated);
            _mockRepositoryAddress.Setup(m => m.Remove(It.IsAny<long>())).Returns(isAllUpdated);
            _mockRepositoryInvoice.Setup(i => i.GetByReceiver(It.IsAny<long>())).Returns(new List<Invoice>());

            var result = _receiverService.Remove(deleteCommand);

            result.Should().BeTrue();
            _mockRepositoryReceiver.Verify(rp => rp.Remove(It.IsAny<long>()));
            _mockRepositoryReceiver.Verify(rp => rp.GetById(It.IsAny<long>()));
            _mockRepositoryAddress.Verify(rp => rp.Remove(It.IsAny<long>()));
        }

        //[Test]
        //public void Test_ReceiverService_Delete_ReceiverRelatedToInvoice_ShouldThrowException()
        //{
        //    Address newAddress = ObjectMother.GetExistentValidAddress();
        //    Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(newAddress, _validCpnj);
        //    _mockRepositoryInvoice.Setup(ir => ir.GetByReceiver(receiver.Id)).Returns(new List<Invoice> { new Invoice() });

        //    Action action = () => { _receiverService.Remove(receiver); };
        //    action.Should().Throw<ReceiverDeleteWithDependenceException>();

        //    _mockRepositoryReceiver.VerifyNoOtherCalls();
        //    _mockRepositoryAddress.VerifyNoOtherCalls();
        //}
    }
}
