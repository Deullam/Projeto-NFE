using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Carriers;
using NUnit.Framework;
using System;
using TheSolutionBrothers.NFe.Infra.Data.Tests.Initializer;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.Carriers
{

    [TestFixture]
    public class CarrierRepositoryTests : EffortTestBase
    {
        private ICarrierRepository _repository;

        private Carrier _carrierLegal;
        private Carrier _carrierPhysical;

        private Address _addressLegal;
        private Address _addressPhysical;
        private CNPJ _cnpj;
        private CPF _cpf;
        
        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
            _repository = new CarrierRepository(_contexto);

            _addressLegal = ObjectMother.GetNewValidAddress();
            _addressPhysical = ObjectMother.GetNewValidAddress();
            _cnpj = ObjectMother.GetValidCNPJ();
            _cpf = ObjectMother.GetValidCPF();
            _carrierLegal = ObjectMother.GetNewValidCarrierLegal(_addressLegal, _cnpj);
            _carrierPhysical = ObjectMother.GetNewValidCarrierPhysical(_addressPhysical, _cpf);
        }

        [Test]
        public void Test_CarrierRepository_Add_Physical_ShouldBeOk()
        {
            var carrierRegistered = _repository.Add(_carrierPhysical);

            carrierRegistered.Should().NotBeNull();
            carrierRegistered.Should().Be(_carrierPhysical);
        }

        [Test]
        public void Test_CarrierRepository_Add_Legal_ShouldBeOk()
        {
            var carrierRegistered = _repository.Add(_carrierLegal);

            carrierRegistered.Should().NotBeNull();
            carrierRegistered.Should().Be(_carrierLegal);
        }

        [Test]
        public void Test_CarrierRepository_GetAll_ShouldBeOk()
        {
            int expectedAmount = 3;
            var carriers = _repository.GetAll();

            carriers.Should().NotBeNull();
            carriers.Should().HaveCount(expectedAmount);
            carriers.Should().Contain(_carrierPhysicalSeed);
            carriers.Should().Contain(_carrierLegalSeed);
        }

        [Test]
        public void Test_CarrierRepository_GetAllWithAmount_ShouldBeOk()
        {
            int amount = 2;
            var carriers = _repository.GetAll(amount);

            carriers.Should().NotBeNull();
            carriers.Should().HaveCount(amount);
            carriers.Should().Contain(_carrierPhysicalSeed);
            carriers.Should().Contain(_carrierLegalSeed);
        }

        [Test]
        public void Test_CarrierRepository_GetById_ShouldBeOk()
        {
            var carrierResult = _repository.GetById(_carrierPhysicalSeed.Id);

            carrierResult.Should().NotBeNull();
            carrierResult.Should().Be(_carrierPhysicalSeed);
        }

        [Test]
        public void Test_CarrierRepository_GetById_ShouldThrowNotFoundException()
        {
            long notFoundId = 10;
            var carrierResult = _repository.GetById(notFoundId);
            carrierResult.Should().BeNull();
        }

        [Test]
        public void Test_CarrierRepository_Delete_ShouldBeOk()
        {
            var result = _repository.Remove(_carrierToDelete.Id);
            result.Should().BeTrue();
            _contexto.Carriers.Where(x => x.Id == _carrierToDelete.Id).ToList().Should().BeEmpty();
        }

        [Test]
        public void Test_CarrierRepository_Delete_ShouldHandleNotFoundException()
        {
            long notFoundId = 10;

            Action action = () => _repository.Remove(notFoundId);

            action.Should().Throw<NotFoundException>();
        }

        [Test]
        public void Test_CarrierRepository_Update_Physical_ShouldBeOk()
        {
            var wasRemoved = false;
            var newName = "Novo nome";
            _carrierPhysicalSeed.Name = newName;
            var action = new Action(() => { wasRemoved = _repository.Update(_carrierPhysicalSeed); });
            action.Should().NotThrow<Exception>();
            wasRemoved.Should().BeTrue();
        }

        [Test]
        public void Test_CarrierRepository_Update_Legal_ShouldBeOk()
        {
            var wasRemoved = false;
            var newName = "Novo nome";
            _carrierLegalSeed.Name = newName;
            var action = new Action(() => { wasRemoved = _repository.Update(_carrierLegalSeed); });
            action.Should().NotThrow<Exception>();
            wasRemoved.Should().BeTrue();
        }

        [Test]
        public void Test_CarrierRepository_Update_ShouldHandleUnknownId()
        {
            _carrierLegal.Id = 20;

            var action = new Action(() => _repository.Update(_carrierLegal));

            action.Should().Throw<DbUpdateConcurrencyException>();
        }
    }
}

