using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.Data.Features.Senders;
using NUnit.Framework;
using System;
using System.Linq;
using TheSolutionBrothers.NFe.Infra.Data.Tests.Initializer;
using System.Data.Entity.Infrastructure;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.Senders
{
	[TestFixture]
	public class SenderRepositoryTests : EffortTestBase
	{
		private ISenderRepository _repository;

		private Sender _sender;
		private Address _address;

		private CNPJ _cnpj;

		[SetUp]
		public override void Initialize()
		{
			base.Initialize();
			_repository = new SenderRepository(_contexto);

			_address = ObjectMother.GetNewValidAddress();
			_cnpj = ObjectMother.GetValidCNPJ();
			_sender = ObjectMother.GetNewValidSender(_address, _cnpj);
		}

		[Test]
		public void Test_SenderRepository_Add_ShouldBeOk()
		{
			var senderRegistered = _repository.Add(_sender);

			senderRegistered.Should().NotBeNull();
			senderRegistered.Should().Be(_sender);
		}

		[Test]
		public void Test_SenderRepository_GetAll_ShouldBeOk()
		{
			int expectedAmount = 2;
			var senders = _repository.GetAll();

			senders.Should().NotBeNull();
			senders.Should().HaveCount(expectedAmount);
			senders.Should().Contain(_senderSeed);
		}

		[Test]
		public void Test_SenderRepository_GetAllWithAmount_ShouldBeOk()
		{
			int amount = 1;
			var senders = _repository.GetAll(amount);

			senders.Should().NotBeNull();
			senders.Should().HaveCount(amount);
			senders.Should().Contain(_senderSeed);
		}

		[Test]
		public void Test_SenderRepository_GetById_ShouldBeOk()
		{
			var senderResult = _repository.GetById(_senderSeed.Id);

			senderResult.Should().NotBeNull();
			senderResult.Should().Be(_senderSeed);
		}

		[Test]
		public void Test_SenderRepository_GetById_ShouldThrowNotFoundException()
		{
			long notFoundId = 10;
			var senderResult = _repository.GetById(notFoundId);
			senderResult.Should().BeNull();
		}

		[Test]
		public void Test_SenderRepository_Delete_ShouldBeOk()
		{
			var result = _repository.Remove(_senderToDelete.Id);
			result.Should().BeTrue();
			_contexto.Senders.Where(x => x.Id == _senderToDelete.Id).ToList().Should().BeEmpty();
		}

		[Test]
		public void Test_SenderRepository_Delete_ShouldHandleNotFoundException()
		{
			long notFoundId = 10;

			Action action = () => _repository.Remove(notFoundId);

			action.Should().Throw<NotFoundException>();
		}

		[Test]
		public void Test_SenderRepository_Update_ShouldBeOk()
		{
			var wasRemoved = false;
			var newCompanyName = "Novo nome";
			_senderSeed.CompanyName = newCompanyName;
			var action = new Action(() => { wasRemoved = _repository.Update(_senderSeed); });
			action.Should().NotThrow<Exception>();
			wasRemoved.Should().BeTrue();
		}

		[Test]
		public void Test_SenderRepository_Update_ShouldHandleUnknownId()
		{
			_sender.Id = 20;

			var action = new Action(() => _repository.Update(_sender));

			action.Should().Throw<DbUpdateConcurrencyException>();
		}
	}
}

