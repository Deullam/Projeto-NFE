using NUnit.Framework;
using FluentAssertions;
using System;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using Moq;
using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.InvoiceReceivers
{
    [TestFixture]
    public class InvoiceReceiverRepositoryTests
    {
		private InvoiceReceiverRepository _repositoryInvoiceReceiver;

		private Mock<Invoice> _fakeInvoice;
		private Address _address;
		private Receiver _receiverLegal;
		private Receiver _receiverPhysical;

		private CPF _cpf;
		private CNPJ _cnpj;

		[SetUp]
		public void Initialize()
		{
			BaseSqlTest.SeedDatabase();

			_repositoryInvoiceReceiver = new InvoiceReceiverRepository();

			_fakeInvoice = new Mock<Invoice>();
			_fakeInvoice.Setup(m => m.Id).Returns(1);

			_cpf = ObjectMother.GetValidCPF();
			_cnpj = ObjectMother.GetValidCNPJ();

			_address = ObjectMother.GetExistentValidAddress();
			_receiverPhysical = ObjectMother.GetExistentValidPhysicalReceiver(_address, _cpf);
			_receiverLegal = ObjectMother.GetExistentValidLegalReceiver(_address, _cnpj);

		}

		[Test]
		public void Test_InvoiceReceiverRepository_SaveReceiverLegal_ShouldBeOk()
		{
			InvoiceReceiver receiver = ObjectMother.GetNewValidInvoiceReceiver(_fakeInvoice.Object, _receiverLegal);
			receiver = _repositoryInvoiceReceiver.Add(receiver);
			receiver.Id.Should().BeGreaterThan(0);
		}

		[Test]
		public void Test_InvoiceReceiverRepository_SaveReceiverPhysical_ShouldBeOk()
		{
			InvoiceReceiver invoiceReceiver = ObjectMother.GetNewValidInvoiceReceiver(_fakeInvoice.Object, _receiverPhysical);
			invoiceReceiver = _repositoryInvoiceReceiver.Add(invoiceReceiver);
			invoiceReceiver.Id.Should().BeGreaterThan(0);
		}

		[Test]
		public void Test_InvoiceReceiverRepository_SaveWithNullInvoice_ShouldThrowException()
		{
			InvoiceReceiver invoiceReceiver = ObjectMother.GetInvalidInvoiceReceiverNullInvoice(_receiverPhysical);
			Action action = () => _repositoryInvoiceReceiver.Add(invoiceReceiver);
			action.Should().Throw<InvoiceReceiverNullInvoiceException>();
		}

		[Test]
		public void Test_InvoiceReceiverRepository_SaveWithNullReceiver_ShouldThrowException()
		{
			InvoiceReceiver invoiceReceiver = ObjectMother.GetInvalidInvoiceReceiverNullReceiver(_fakeInvoice.Object);
			Action action = () => _repositoryInvoiceReceiver.Add(invoiceReceiver);
			action.Should().Throw<InvoiceReceiverNullReceiverException>();
		}

		[Test]
		public void Test_InvoiceReceiverRepository_SaveWithReceiverUninformedName_ShouldThrowException()
		{
			_receiverPhysical = ObjectMother.GetInvalidPhysicalReceiverEmptyName(_address, _cpf);
			InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(_fakeInvoice.Object, _receiverPhysical);
			Action action = () => _repositoryInvoiceReceiver.Add(invoiceReceiver);
			action.Should().Throw<ReceiverUninformedNameException>();
		}

		[Test]
		public void Test_InvoiceReceiverRepository_GetByInvoice_ShouldBeOk()
		{
			long existentId = 4;

			InvoiceReceiver invoiceReceiver = _repositoryInvoiceReceiver.GetByInvoice(existentId);

			invoiceReceiver.Should().NotBeNull();
			invoiceReceiver.Receiver.Should().NotBeNull();
		}

		[Test]
		public void Test_InvoiceReceiverRepository_GetByInvoiceNonexistentId_ShouldBeOk()
		{
			long unexistentId = 100;
			InvoiceReceiver receiver = _repositoryInvoiceReceiver.GetByInvoice(unexistentId);
			receiver.Should().BeNull();
		}

		[Test]
		public void Test_InvoiceReceiverRepository_GetByInvoiceIdInvalid_ShouldThrowException()
		{
			long invalidId = -1;
			Action action = () => _repositoryInvoiceReceiver.GetByInvoice(invalidId);
			action.Should().Throw<IdentifierUndefinedException>();
		}

	}
}
