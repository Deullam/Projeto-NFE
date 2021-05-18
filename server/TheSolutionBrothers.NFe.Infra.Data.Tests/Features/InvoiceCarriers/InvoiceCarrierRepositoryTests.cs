using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceCarriers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.InvoiceCarriers
{

    [TestFixture]
    public class InvoiceCarrierRepositoryTests
    {
        private InvoiceCarrier _invoiceCarrier;
        private IInvoiceCarrierRepository _repository;
        private Address _address;
        private CNPJ _cnpj;
        private CPF _cpf;
        private Carrier _carrierPhysical;
        private Carrier _carrierLegal;
        private Mock<Invoice> _fakeInvoice;
        
        [SetUp]
        public void Initalize()
        {
            BaseSqlTest.SeedDatabase();
            _invoiceCarrier = new InvoiceCarrier();
            _repository = new InvoiceCarrierRepository();
            _address = ObjectMother.GetExistentValidAddress();
            _cnpj = ObjectMother.GetValidCNPJ();
            _cpf = ObjectMother.GetValidCPF();
            _carrierPhysical = ObjectMother.GetExistentValidCarrierPhysical(_address, _cpf);
            _carrierLegal = ObjectMother.GetExistentValidCarrierLegal(_address, _cnpj);
            _fakeInvoice = new Mock<Invoice>();
        }

        [Test]
        public void Test_InvoiceCarrier_Save_CarrierPhysical_ShouldBeOK()
        {
            _fakeInvoice.Setup(invoice => invoice.Id).Returns(1);
            InvoiceCarrier invoiceCarrier = ObjectMother.GetNewValidInvoiceCarrier(_carrierPhysical,_fakeInvoice.Object);
            invoiceCarrier = _repository.Add(invoiceCarrier);
            invoiceCarrier.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Test_Carrier_SaveInvalidCarrier_ShouldThrowException()
        {
            _invoiceCarrier = ObjectMother.GetInvalidInvoiceCarrierWithNullCarrier(_carrierLegal, _fakeInvoice.Object);

            Action comparation = () => _invoiceCarrier.Validate();
            comparation.Should().Throw<InvoiceCarrierNullCarrierException>();
        }

        [Test]
        public void Test_Carrier_SaveInvalidInvoice_ShouldThrowException()
        {
            _invoiceCarrier = ObjectMother.GetInvalidInvoiceCarrierWithNullInvoice(_carrierPhysical, _fakeInvoice.Object);

            Action comparation = () => _invoiceCarrier.Validate();
            comparation.Should().Throw<InvoiceCarrierNullInvoiceException>();
        }

        [Test]
        public void Test_InvoiceCarrier_SaveCarrierLegal_ShouldBeOK()
        {
            _fakeInvoice.Setup(invoice => invoice.Id).Returns(2);
            InvoiceCarrier invoiceCarrier = ObjectMother.GetNewValidInvoiceCarrier(_carrierLegal, _fakeInvoice.Object);
            invoiceCarrier = _repository.Add(invoiceCarrier);
            invoiceCarrier.Id.Should().BeGreaterThan(0);
        }
        [Test]
        public void Test_InvoiceCarrierRepository_GetByIdInvoice_ShouldBeOk()
        {
            long existentId = 4;
            InvoiceCarrier carrierFound = _repository.GetByInvoice(existentId);
            carrierFound.Should().NotBeNull();
            carrierFound.Carrier.Should().NotBeNull();
        }
        [Test]
        public void Test_InvoiceCarrierRepository_GetByIdInvalid_ShouldThrowException()
        {
            long invalidId = -1;
            Action action = () => _repository.GetByInvoice(invalidId);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_InvoiceCarrierRepository_GetNonexistentId_ShouldBeOk()
        {
            long invalidId = 100;
            InvoiceCarrier carrierFound = _repository.GetByInvoice(invalidId);
            carrierFound.Should().BeNull();
        }

       

    }
}
