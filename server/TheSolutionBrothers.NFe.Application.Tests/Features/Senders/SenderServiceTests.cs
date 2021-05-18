using FluentAssertions;
using Moq;
using TheSolutionBrothers.NFe.Application.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TheSolutionBrothers.NFe.Application.Features.Senders.Commands;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using AutoMapper;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Senders.Queries;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Application.Tests.Features.Senders
{

	[TestFixture]
	public class SenderServiceTests
	{
		private SenderService _senderService;

		private Mock<ISenderRepository> _mockRepositorySender;
		private Mock<IAddressRepository> _mockRepositoryAddress;
		private Mock<IInvoiceRepository> _mockRepositoryInvoice;
		private Mock<IMapper> _mockIMapper;

		private CNPJ _validCpnj;
		private Address _address;
		private Sender _newSenderMapped;
		private Sender _existentSenderMapped;

		[SetUp]
		public void Initialize()
		{
			_mockRepositorySender = new Mock<ISenderRepository>();
			_mockRepositoryAddress = new Mock<IAddressRepository>();
			_mockRepositoryInvoice = new Mock<IInvoiceRepository>();
			_mockIMapper = new Mock<IMapper>();

			_senderService = new SenderService(_mockRepositorySender.Object, _mockRepositoryAddress.Object, _mockRepositoryInvoice.Object, _mockIMapper.Object);

			_validCpnj = ObjectMother.GetValidCNPJ();
			_address = ObjectMother.GetExistentValidAddress();
			_newSenderMapped = ObjectMother.GetNewValidSender(_address, _validCpnj);
			_existentSenderMapped = ObjectMother.GetExistentValidSender(_address, _validCpnj);
		}

		[Test]
		public void Test_SenderService_Add_ShouldBeOk()
		{
			int expectedId = 1;
			AddressCommand addressCommand = ObjectMother.GetValidAddresCommand();
			SenderRegisterCommand senderToRegister = ObjectMother.GetNewValidSenderRegisterCommand(addressCommand, _validCpnj);

			_mockIMapper.Setup(m => m.Map<Sender>(senderToRegister)).Returns(_newSenderMapped);
			_mockRepositorySender.Setup(m => m.Add(_newSenderMapped)).Returns(new Sender() { Id = expectedId });

			long result = _senderService.Add(senderToRegister);

			result.Should().Be(expectedId);
			_mockRepositorySender.Verify(rp => rp.Add(_newSenderMapped));
		}

		[Test]
		public void Test_SenderService_Add_ShouldBeHandleException()
		{
			AddressCommand addressCommand = ObjectMother.GetValidAddresCommand();
			SenderRegisterCommand senderToRegister = ObjectMother.GetNewValidSenderRegisterCommand(addressCommand, _validCpnj);

			Address addressMapped = ObjectMother.GetExistentValidAddress();

			_mockIMapper.Setup(m => m.Map<Sender>(senderToRegister)).Returns(_newSenderMapped);
			_mockRepositorySender.Setup(pr => pr.Add(It.IsAny<Sender>())).Throws<Exception>();

			Action newSenderAction = () => _senderService.Add(senderToRegister);

			newSenderAction.Should().Throw<Exception>();
			_mockRepositorySender.Verify(pr => pr.Add(It.IsAny<Sender>()), Times.Once);
		}

		[Test]
		public void Test_SenderService_Add_SenderWithAddressInvalid_ShouldThrowException()
		{
			AddressCommand addressCommand = ObjectMother.GetValidAddresCommand();
			SenderRegisterCommand senderToRegister = ObjectMother.GetNewValidSenderRegisterCommand(addressCommand, _validCpnj);

			Sender invalidSenderMapped = ObjectMother.GetInvalidSenderWithNullAddress(_validCpnj);

			_mockIMapper.Setup(m => m.Map<Sender>(senderToRegister)).Returns(invalidSenderMapped);

			Action action = () => { _senderService.Add(senderToRegister); };
			action.Should().Throw<SenderNullAddressException>();

			_mockIMapper.Verify(m => m.Map<Sender>(senderToRegister));
			_mockRepositorySender.VerifyNoOtherCalls();
			_mockRepositorySender.VerifyNoOtherCalls();
		}

		[Test]
		public void Test_SenderService_Remove_True_ShouldBeOk()
		{
            bool isAllUpdated = true;
			long[] senderIds = { 1 };
			SenderDeleteCommand deleteCommand = ObjectMother.GetSenderCommandToDelete(senderIds);

			_mockRepositorySender.Setup(r => r.GetById(It.IsAny<long>())).Returns(_existentSenderMapped);
            _mockRepositorySender.Setup(m => m.Remove(It.IsAny<long>()));
            _mockRepositoryInvoice.Setup(i => i.GetBySender(It.IsAny<long>())).Returns(new List<Invoice>());
            _mockRepositorySender.Setup(i => i.Remove(It.IsAny<long>())).Returns(isAllUpdated);
            _mockRepositoryAddress.Setup(a => a.Remove(It.IsAny<long>())).Returns(isAllUpdated);

            var result = _senderService.Remove(deleteCommand);

            result.Should().BeTrue();
			_mockRepositorySender.Verify(rp => rp.Remove(deleteCommand.SenderIds[0]));
			_mockRepositoryInvoice.Verify(rp => rp.GetBySender(It.IsAny<long>()), Times.Exactly(deleteCommand.SenderIds.Count()));
			_mockRepositoryAddress.Verify(rp => rp.Remove(It.IsAny<long>()), Times.Exactly(deleteCommand.SenderIds.Count()));
        }

		[Test]
		public void Test_SenderService_Delete_False_ShouldBeOk()
		{
			var isAllUpdated = false;

			long[] senderIds = { 1 };
			var deleteCommand = ObjectMother.GetSenderCommandToDelete(senderIds);

			_existentSenderMapped.AddressId = _existentSenderMapped.Address.Id;

			_mockRepositorySender.Setup(r => r.GetById(It.IsAny<long>())).Returns(_existentSenderMapped);
			_mockRepositorySender.Setup(m => m.Remove(It.IsAny<long>())).Returns(isAllUpdated);
            _mockRepositoryInvoice.Setup(i => i.GetBySender(It.IsAny<long>())).Returns(new List<Invoice>() { new Invoice()});
			_mockRepositoryAddress.Setup(m => m.Remove(It.IsAny<long>())).Returns(isAllUpdated);

            var result = _senderService.Remove(deleteCommand);

			result.Should().BeFalse();
            _mockRepositorySender.Verify(rp => rp.GetById(It.IsAny<long>()), Times.Exactly(deleteCommand.SenderIds.Count()));
            _mockRepositoryInvoice.Verify(rp => rp.GetBySender(It.IsAny<long>()), Times.Exactly(deleteCommand.SenderIds.Count()));
            _mockRepositoryAddress.VerifyNoOtherCalls();
        }

		[Test]
		public void Products_Service_Remove_ShouldBeHandleNotFoundException()
		{
			long[] senderIds = { 1 };
			var senderToRemove = ObjectMother.GetSenderCommandToDelete(senderIds);

            _mockRepositorySender.Setup(i => i.GetById(It.IsAny<long>())).Returns(_existentSenderMapped);
            _mockRepositorySender.Setup(pr => pr.Remove(senderToRemove.SenderIds[0])).Throws<NotFoundException>();
            _mockRepositoryInvoice.Setup(i => i.GetBySender(It.IsAny<long>())).Returns(new List<Invoice>());

			Action senderAction = () => _senderService.Remove(senderToRemove);

			senderAction.Should().Throw<NotFoundException>();
			_mockRepositorySender.Verify(pr => pr.Remove(senderToRemove.SenderIds[0]), Times.Once);
		}

		[Test]
		public void Test_SenderService_GetById_ShouldBeOk()
		{
			long id = 1;

			Mock<AddessViewModel> adressViewModel = new Mock<AddessViewModel>();
			SenderViewModel senderViewModel = ObjectMother.GetValidSenderViewModel(adressViewModel.Object, _validCpnj);

			_mockRepositorySender.Setup(m => m.GetById(id)).Returns(ObjectMother.GetExistentValidSender(_address, _validCpnj));
			_mockIMapper.Setup(m => m.Map<SenderViewModel>(_existentSenderMapped)).Returns(senderViewModel);

			SenderViewModel result = _senderService.GetById(id);

			_newSenderMapped.Should().NotBeNull();
			_mockRepositorySender.Verify(r => r.GetById(id));
		}

		[Test]
		public void Test_SenderService_GetById_ShouldBeHandleNotFoundException()
		{
			var exception = new NotFoundException();
			_mockRepositorySender.Setup(pr => pr.GetById(_existentSenderMapped.Id)).Throws(exception);

			Action productAction = () => _senderService.GetById(_existentSenderMapped.Id);

			productAction.Should().Throw<NotFoundException>();
			_mockRepositorySender.Verify(pr => pr.GetById(_existentSenderMapped.Id), Times.Once);
		}

		[Test]
		public void Test_SenderService_GetAll_Query_ShouldBeOk()
		{
			int amount = 1;

			SenderGetAllQuery query = new SenderGetAllQuery(amount);

			Mock<AddessViewModel> adressViewModel = new Mock<AddessViewModel>();
			var senderMapped = ObjectMother.GetValidSenderViewModel(adressViewModel.Object, _validCpnj);
			var repositoryMockValue = new List<Sender>() { _existentSenderMapped }.AsQueryable();

			_mockRepositorySender.Setup(m => m.GetAll(query.Size)).Returns(repositoryMockValue);
			_mockIMapper.Setup(m => m.Map<SenderViewModel>(It.IsAny<Sender>())).Returns(senderMapped);

			var senders = _senderService.GetAll(query);

			_mockRepositorySender.Verify(x => x.GetAll(amount));
			senders.Should().NotBeNull();
			senders.Should().HaveCount(repositoryMockValue.Count());
			senders.ToList().First().Id.Should().Be(senderMapped.Id);
		}

		[Test]
		public void Test_SenderService_GetAll_ShouldBeOk()
		{
			int amount = 1;

			var repositoryMockValue = new List<Sender>() { _existentSenderMapped }.AsQueryable();
			_mockRepositorySender.Setup(m => m.GetAll()).Returns(repositoryMockValue);

			var customers = _senderService.GetAll();

			_mockRepositorySender.Verify(x => x.GetAll());
			customers.Should().NotBeNull();
			customers.Should().HaveCount(amount);
			_mockRepositoryAddress.VerifyNoOtherCalls();
		}

		[Test]
		public void Test_SenderService_Update_ShouldBeOk()
		{
			AddressCommand addressCommand = ObjectMother.GetValidAddresCommand();
			var senderToUpdate = ObjectMother.GetExistentValidSenderCommandToUpdate(addressCommand, _validCpnj);

			var isUpdated = true;

			_mockIMapper.Setup(m => m.Map<Sender>(senderToUpdate)).Returns(_existentSenderMapped);
			_mockRepositorySender.Setup(pr => pr.GetById(_existentSenderMapped.Id)).Returns(_existentSenderMapped);
			_mockRepositorySender.Setup(pr => pr.Update(_existentSenderMapped)).Returns(isUpdated);

			var isSenderRemoved = _senderService.Update(senderToUpdate);

			_mockRepositorySender.Verify(pr => pr.GetById(_existentSenderMapped.Id), Times.Once);
			_mockRepositorySender.Verify(pr => pr.Update(_existentSenderMapped), Times.Once);
			isSenderRemoved.Should().BeTrue();
		}

		[Test]
		public void Products_Service_Update_ShouldBeHandleNotFoundException()
		{
			AddressCommand addressCommand = ObjectMother.GetValidAddresCommand();
			var senderToUpdate = ObjectMother.GetExistentValidSenderCommandToUpdate(addressCommand, _validCpnj);

			_mockIMapper.Setup(m => m.Map<Sender>(senderToUpdate)).Returns(_existentSenderMapped);
			_mockRepositorySender.Setup(pr => pr.GetById(senderToUpdate.Id)).Returns((Sender)null);

			Action senderAction = () => _senderService.Update(senderToUpdate);

			senderAction.Should().Throw<NotFoundException>();

			_mockRepositorySender.Verify(pr => pr.GetById(_existentSenderMapped.Id), Times.Once);
			_mockRepositorySender.Verify(pr => pr.Update(It.IsAny<Sender>()), Times.Never);
		}
	}
}