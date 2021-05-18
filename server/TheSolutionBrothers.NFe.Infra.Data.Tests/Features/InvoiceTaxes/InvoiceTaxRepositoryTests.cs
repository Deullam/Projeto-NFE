using System;
using NUnit.Framework;
using FluentAssertions;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using Moq;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.InvoiceTaxes
{
	[TestFixture]
	public class InvoiceTaxRepositoryTests
	{
		private InvoiceTaxRepository _repositoryInvoiceTax;

		private Mock<Invoice> _fakeInvoice;

		[SetUp]
		public void Initialize()
		{
			BaseSqlTest.SeedDatabase();

			_repositoryInvoiceTax = new InvoiceTaxRepository();

			_fakeInvoice = new Mock<Invoice>();
			_fakeInvoice.Setup(m => m.Id).Returns(1);

		}

		[Test]
		public void Test_InvoiceTaxRepository_Save_ShouldBeOk()
		{
			InvoiceTax invoiceTax = ObjectMother.GetNewValidInvoiceTax(_fakeInvoice.Object);
			invoiceTax = _repositoryInvoiceTax.Add(invoiceTax);
			invoiceTax.Id.Should().BeGreaterThan(0);
		}

		[Test]
		public void Test_InvoiceTaxRepository_SaveWithIpiValueEqualZero_ShouldThrowException()
		{
			InvoiceTax invoiceTax = ObjectMother.GetInvalidInvoiceTaxWithIpiValueEqualZero(_fakeInvoice.Object);
			Action action = () => _repositoryInvoiceTax.Add(invoiceTax);
			action.Should().Throw<InvoiceTaxNonPositiveIpiValueException>();
		}

		[Test]
		public void Test_InvoiceTaxRepository_GetByInvoice_ShouldBeOk()
		{
			long existentId = 1;

			InvoiceTax invoiceTax = _repositoryInvoiceTax.GetByInvoice(existentId);

			invoiceTax.Should().NotBeNull();
		}

		[Test]
		public void Test_InvoiceTaxRepository_GetByInvoiceNonexistentId_ShouldBeOk()
		{
			long unexistentId = 100;
			InvoiceTax receiver = _repositoryInvoiceTax.GetByInvoice(unexistentId);
			receiver.Should().BeNull();
		}

		[Test]
		public void Test_InvoiceTaxRepository_GetByInvoiceIdInvalid_ShouldThrowException()
		{
			long invalidId = -1;
			Action action = () => _repositoryInvoiceTax.GetByInvoice(invalidId);
			action.Should().Throw<IdentifierUndefinedException>();
		}

	}
}

