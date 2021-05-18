using System;
using System.Collections.Generic;
using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Receivers;
using NUnit.Framework;
using TheSolutionBrothers.NFe.Infra.Data.Tests.Initializer;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.Receivers
{
	[TestFixture]
	public class ReceiverRepositoryTests : EffortTestBase
    {
        private IReceiverRepository _repository;

        private Receiver _receiverLegal;
        private Receiver _receiverPhysical;

        private Address _addressLegal;
        private Address _addressPhysical;
        private CNPJ _cnpj;
        private CPF _cpf;

        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
            _repository = new ReceiverRepository(_contexto);

            _addressLegal = ObjectMother.GetNewValidAddress();
            _addressPhysical = ObjectMother.GetNewValidAddress();
            _cnpj = ObjectMother.GetValidCNPJ();
            _cpf = ObjectMother.GetValidCPF();
            _receiverLegal = ObjectMother.GetNewValidLegalReceiver(_addressLegal, _cnpj);
            _receiverPhysical = ObjectMother.GetNewValidPhysicalReceiver(_addressPhysical, _cpf);
        }

        [Test]
        public void Test_ReceiverRepository_Add_Physical_ShouldBeOk()
        {
            var receiverRegistered = _repository.Add(_receiverPhysical);

            receiverRegistered.Should().NotBeNull();
            receiverRegistered.Should().Be(_receiverPhysical);
        }

        [Test]
        public void Test_ReceiverRepository_Add_Legal_ShouldBeOk()
        {
            var receiverRegistered = _repository.Add(_receiverLegal);

            receiverRegistered.Should().NotBeNull();
            receiverRegistered.Should().Be(_receiverLegal);
        }

        [Test]
        public void Test_ReceiverRepository_GetAll_ShouldBeOk()
        {
            int expectedAmount = 3;
            var receivers = _repository.GetAll();

            receivers.Should().NotBeNull();
            receivers.Should().HaveCount(expectedAmount);
            receivers.Should().Contain(_receiverPhysicalSeed);
            receivers.Should().Contain(_receiverLegalSeed);
        }

        [Test]
        public void Test_ReceiverRepository_GetAllWithAmount_ShouldBeOk()
        {
            int amount = 2;
            var receivers = _repository.GetAll(amount);

            receivers.Should().NotBeNull();
            receivers.Should().HaveCount(amount);
            receivers.Should().Contain(_receiverPhysicalSeed);
            receivers.Should().Contain(_receiverLegalSeed);
        }

        [Test]
        public void Test_ReceiverRepository_GetById_ShouldBeOk()
        {
            var receiverResult = _repository.GetById(_receiverPhysicalSeed.Id);

            receiverResult.Should().NotBeNull();
            receiverResult.Should().Be(_receiverPhysicalSeed);
        }

        [Test]
        public void Test_ReceiverRepository_GetById_ShouldThrowNotFoundException()
        {
            long notFoundId = 10;
            var receiverResult = _repository.GetById(notFoundId);
            receiverResult.Should().BeNull();
        }

        [Test]
        public void Test_ReceiverRepository_Delete_ShouldBeOk()
        {
            var result = _repository.Remove(_receiverToDelete.Id);
            result.Should().BeTrue();
            _contexto.Receivers.Where(x => x.Id == _receiverToDelete.Id).ToList().Should().BeEmpty();
        }

        [Test]
        public void Test_ReceiverRepository_Delete_ShouldHandleNotFoundException()
        {
            long notFoundId = 10;

            Action action = () => _repository.Remove(notFoundId);

            action.Should().Throw<NotFoundException>();
        }

        [Test]
        public void Test_ReceiverRepository_Update_Physical_ShouldBeOk()
        {
            var wasRemoved = false;
            var newName = "Novo nome";
            _receiverPhysicalSeed.Name = newName;
            var action = new Action(() => { wasRemoved = _repository.Update(_receiverPhysicalSeed); });
            action.Should().NotThrow<Exception>();
            wasRemoved.Should().BeTrue();
        }

        [Test]
        public void Test_ReceiverRepository_Update_Legal_ShouldBeOk()
        {
            var wasRemoved = false;
            var newName = "Novo nome";
            _receiverLegalSeed.Name = newName;
            var action = new Action(() => { wasRemoved = _repository.Update(_receiverLegalSeed); });
            action.Should().NotThrow<Exception>();
            wasRemoved.Should().BeTrue();
        }

        [Test]
        public void Test_ReceiverRepository_Update_ShouldHandleUnknownId()
        {
            _receiverLegal.Id = 20;

            var action = new Action(() => _repository.Update(_receiverLegal));

            action.Should().Throw<DbUpdateConcurrencyException>();
        }

    }
}
