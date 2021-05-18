using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceSenders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.InvoiceSenders
{

    [TestFixture]
    public class InvoiceSenderRepositoryTests
    {
        private IInvoiceSenderRepository _repository;
        private Address _address;
        private CNPJ _cnpj;
        private Sender _sender;
        private Mock<Invoice> _fakeInvoice;

        [SetUp]
        public void Initialize()
        {
            BaseSqlTest.SeedDatabase();
            _repository = new InvoiceSenderRepository();
            _address = ObjectMother.GetExistentValidAddress();
            _cnpj = ObjectMother.GetValidCNPJ();
            _sender = ObjectMother.GetExistentValidSender(_address,_cnpj);
            _fakeInvoice = new Mock<Invoice>();
        }

        [Test]
        public void Test_InvoiceSenderRepository_Save_ShouldBeOk()
        {
            long existentId = 2;
            _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
            InvoiceSender invoiceSender = ObjectMother.GetNewInvoiceSenderOk(_fakeInvoice.Object,_sender);
            invoiceSender = _repository.Add(invoiceSender);
            invoiceSender.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Test_InvoiceSenderRepository_SaveInvalid_ShouldThrowException()
        {
            long existentId = 2;
            _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
            InvoiceSender invoiceSender = ObjectMother.GetInvalidInvoiceSenderWithSenderNull(_fakeInvoice.Object);
            Action action = () => _repository.Add(invoiceSender);
            action.Should().Throw<InvoiceSenderNullSenderException>();
        }

        [Test]
        public void Test_InvoiceSenderRepository_GetByInvoiceInvalidId_ShouldThrowException()
        {
            long invalidId = 0;
            _fakeInvoice.Setup(invoice => invoice.Id).Returns(invalidId);
            InvoiceSender invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(_fakeInvoice.Object, _sender);
            Action action  = () => _repository.GetByInvoice(invoiceSender.Invoice.Id);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_InvoiceSenderRepository_GetByInvoice_ShouldBeOk()
        {
            long existentId = 4;
            _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
            InvoiceSender invoiceSender = ObjectMother.GetNewInvoiceSenderOk(_fakeInvoice.Object, _sender);
            invoiceSender = _repository.GetByInvoice(invoiceSender.Invoice.Id);
            invoiceSender.Should().NotBeNull();
        }

        [Test]
        public void Test_InvoiceSenderRepository_GetByInvoiceNonExistentId_ShouldBeOk()
        {
            long existentId = 2;
            _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
            InvoiceSender invoiceSender = ObjectMother.GetNewInvoiceSenderOk(_fakeInvoice.Object, _sender);
            invoiceSender = _repository.GetByInvoice(invoiceSender.Invoice.Id);
            invoiceSender.Should().BeNull();
        }
    }

}
