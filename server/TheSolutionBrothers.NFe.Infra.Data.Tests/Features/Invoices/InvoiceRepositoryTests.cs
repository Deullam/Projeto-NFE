using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Infra.Data.Tests.Initializer;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.Invoices
{

	[TestFixture]
	public class InvoiceRepositoryTests : EffortTestBase
	{
		private Invoice _invoice;
		private InvoiceItem _invoiceItem;
		private IInvoiceRepository _repositoryInvoice;

		[SetUp]
		public void Initalize()
		{
			base.Initialize();
			_repositoryInvoice = new InvoiceRepository(_contexto);

			_invoiceItem = ObjectMother.GetNewInvoiceItemOk(_invoice, _productSeed);
			_invoice = ObjectMother.GetExistentValidOpenedInvoice(_senderSeed, _receiverLegalSeed, _carrierLegalSeed,
				new List<InvoiceItem>() { _invoiceItem });

		}


		[Test]
		public void Test_InvoiceRepository_Add_ShouldBeOK()
		{
			var invoiceRegistered = _repositoryInvoice.Add(_invoice);

			invoiceRegistered.Should().NotBeNull();
			invoiceRegistered.Should().Be(_invoice);
			invoiceRegistered.InvoiceItems.Should().NotBeNull();
			invoiceRegistered.InvoiceItems.First().Should().Be(_invoiceItem);
		}

		//[Test]
		//public void Test_InvoiceRepository_Save_NullCarrier_ShouldBeOK()
		//{
		//    _invoice.Carrier = null;
		//    Invoice invoiceToSave = _repositoryInvoice.Save(_invoice);
		//    invoiceToSave.Id.Should().BeGreaterThan(3);
		//}

		//[Test]
		//public void Test_InvoiceRepository_SaveInvalid_ShouldBeThrowException()
		//{
		//    Invoice invoiceToSave = _invoice;
		//    invoiceToSave.NatureOperation = null;
		//    Action resultadoEsperado = () => _repositoryInvoice.Save(invoiceToSave);
		//    resultadoEsperado.Should().Throw<InvoiceUninformedNatureOperationException>();

		//}

		[Test]
		public void Test_InvoiceRepository_Update_ShouldBeOk()
		{
			var wasRemoved = false;
			var newNumber = 99999;

			_invoiceSeed.Number = newNumber;
			var action = new Action(() => { wasRemoved = _repositoryInvoice.Update(_invoiceSeed); });
			action.Should().NotThrow<Exception>();
			wasRemoved.Should().BeTrue();
		}

		//[Test]
		//public void Test_InvoiceRepository_UpdateInvoiceIsseud_ShouldBeOk()
		//{
		//    KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
		//    Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(_address, _cpfReceiver);
		//    Carrier carrier = ObjectMother.GetExistentValidCarrierLegal(_address, _cnpj);
		//    Sender sender = ObjectMother.GetExistentValidSender(_address, _cnpj);
		//    InvoiceReceiver invoiceReceiver = ObjectMother.GetNewValidInvoiceReceiver(_invoice, receiver);
		//    InvoiceCarrier invoiceCarrier = ObjectMother.GetNewValidInvoiceCarrier(carrier, _invoice);
		//    InvoiceSender invoiceSender = ObjectMother.GetNewInvoiceSenderOk(_invoice, sender);
		//    InvoiceTax invoiceTax = ObjectMother.GetNewValidInvoiceTax(_invoice);
		//    TaxProduct taxProduct = ObjectMother.GetValidTaxProduct();
		//    Product product = ObjectMother.GetExistentValidProduct(taxProduct);
		//    InvoiceItem invoiceItem = ObjectMother.GetNewInvoiceItemOk(_invoice, product);
		//    IList<InvoiceItem> itens = new List<InvoiceItem> { invoiceItem };
		//    Invoice invoiceToUpdate = ObjectMother.GetExistentValidIssuedInvoice(keyAccess, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, itens);
		//    invoiceToUpdate.NatureOperation = "operação de saida de produtos";

		//    invoiceToUpdate = _repositoryInvoice.Update(invoiceToUpdate);

		//    Invoice result = _repositoryInvoice.Get(invoiceToUpdate.Id);
		//    result.Should().NotBeNull();
		//    result.NatureOperation.Should().Be(invoiceToUpdate.NatureOperation);
		//}

		[Test]
		public void Test_InvoiceRepository_Update_ShouldHandleUnknownId()
		{
			_invoice.Id = 20;

			var action = new Action(() => _repositoryInvoice.Update(_invoice));

			action.Should().Throw<InvalidOperationException>();
		}

		[Test]
		public void Test_InvoiceRepository_Delete_ShouldBeOk()
		{
            var result = _repositoryInvoice.Remove(_invoiceToDelete.Id);
            result.Should().BeTrue();
            _contexto.Invoices.Where(x => x.Id == _invoiceToDelete.Id).ToList().Should().BeEmpty();
            _contexto.Invoices.Where(x => x.Id == _invoiceToDelete.InvoiceItems.FirstOrDefault().Id).ToList().Should().BeEmpty();
        }

        [Test]
		public void Test_InvoiceRepository_Delete_ShouldHandleNotFoundException()
		{
			long notFoundId = 10;

			Action action = () => _repositoryInvoice.Remove(notFoundId);

			action.Should().Throw<NotFoundException>();
		}

		[Test]
		public void Test_InvoiceRepository_GetById_ShouldBeOk()
		{
			long existentId = 1;
			Invoice invoiceFound = _repositoryInvoice.GetById(existentId);

			invoiceFound.Should().NotBeNull();
			invoiceFound.Should().Be(_invoiceSeed);
			invoiceFound.Carrier.Should().NotBeNull();
			invoiceFound.Receiver.Should().NotBeNull();
			invoiceFound.Sender.Should().NotBeNull();
			invoiceFound.Carrier.Should().Be(_carrierLegalSeed);
			invoiceFound.Receiver.Should().Be(_receiverLegalSeed);
			invoiceFound.Sender.Should().Be(_senderSeed);
		}

		[Test]
		public void Test_InvoiceRepository_GetById_ShouldThrowNotFoundException()
		{
			long notFoundId = 10;
			var invoiceResult = _repositoryInvoice.GetById(notFoundId);
			invoiceResult.Should().BeNull();
		}

		[Test]
		public void Test_InvoiceRepository_GetByCarrierInvalid_ShouldThrowException()
		{
			long invalidId = -1;
			Action action = () => _repositoryInvoice.GetByCarrier(invalidId);
			action.Should().Throw<IdentifierUndefinedException>();
		}

        [Test]
        public void Test_InvoiceRepository_GetAllWithAmount_ShouldBeOk()
        {
            int amount = 1;
            var invoices = _repositoryInvoice.GetAll(amount);

            invoices.Should().NotBeNull();
            invoices.Should().HaveCount(amount);
            invoices.Should().Contain(_invoiceSeed);
        }

        [Test]
        public void Test_InvoiceRepository_GetAll_ShouldBeOk()
        {
            int expectedAmount = 2;
            var invoices = _repositoryInvoice.GetAll();

            invoices.Should().NotBeNull();
            invoices.Should().HaveCount(expectedAmount);
            invoices.Should().Contain(_invoiceSeed);
        }

        //[Test]
        //public void Test_InvoiceRepository_GetByIdInvalid_ShouldThrowException()
        //{
        //    long invalidId = -1;
        //    Action action = () => _repositoryInvoice.Get(invalidId);
        //    action.Should().Throw<IdentifierUndefinedException>();
        //}

        //[Test]
        //public void Test_InvoiceRepository_GetNonexistentId_ShouldBeOk()
        //{
        //    long invalidId = 100;
        //    Invoice invoiceFound = _repositoryInvoice.Get(invalidId);
        //    invoiceFound.Should().BeNull();
        //}

        [Test]
        public void Test_InvoiceRepository_GetByCarrier_ShouldBeOk()
        {
            long existentId = 1;
            int expectedAmount = 2;
            IList<Invoice> invoices = _repositoryInvoice.GetByCarrier(existentId);
            invoices.Count.Should().Be(expectedAmount);
            invoices[0].Carrier.Should().NotBeNull();

        }


        [Test]
        public void Test_InvoiceRepository_GetByCarrierNonexistentId_ShouldBeOk()
        {
            long invalidId = 100;
            IList<Invoice> invoices = _repositoryInvoice.GetByCarrier(invalidId);
            invoices.Should().BeNullOrEmpty();
        }

        [Test]
        public void Test_InvoiceRepository_GetByReceiver_ShouldBeOk()
        {
            long existentId = 1;
            int expectedAmount = 2;
            IList<Invoice> invoices = _repositoryInvoice.GetByReceiver(existentId);
            invoices.Count.Should().Be(expectedAmount);
            invoices[0].Receiver.Should().NotBeNull();

        }

        [Test]
		public void Test_InvoiceRepository_GetByReceiverInvalid_ShouldThrowException()
		{
			long invalidId = -1;
			Action action = () => _repositoryInvoice.GetByReceiver(invalidId);
			action.Should().Throw<IdentifierUndefinedException>();
		}

        [Test]
        public void Test_InvoiceRepository_GetByReceiverNonexistentId_ShouldBeOk()
        {
            long invalidId = 100;
            IList<Invoice> invoices = _repositoryInvoice.GetByReceiver(invalidId);
            invoices.Should().BeNullOrEmpty();
        }

        [Test]
        public void Test_InvoiceRepository_GetBySender_ShouldBeOk()
        {
            long existentId = 1;
            int expectedAmount = 2;
            IList<Invoice> invoices = _repositoryInvoice.GetBySender(existentId);
            invoices.Count.Should().Be(expectedAmount);
            invoices[0].Sender.Should().NotBeNull();
        }

        [Test]
		public void Test_InvoiceRepository_GetBySenderInvalid_ShouldThrowException()
		{
			long invalidId = -1;
			Action action = () => _repositoryInvoice.GetBySender(invalidId);
			action.Should().Throw<IdentifierUndefinedException>();
		}

        [Test]
        public void Test_InvoiceRepository_GetBySenderNonexistentId_ShouldBeOk()
        {
            long invalidId = 100;
            IList<Invoice> invoices = _repositoryInvoice.GetBySender(invalidId);
            invoices.Should().BeNullOrEmpty();
        }

        //[Test]
        //public void Test_InvoiceRepository_GetByKeyAccess_ShouldBeOk()
        //{
        //	string existentKey = "a";
        //	Invoice invoiceFound = _repositoryInvoice.GetByKeyAccess(existentKey);
        //	invoiceFound.Should().NotBeNull();
        //	invoiceFound.Carrier.Should().NotBeNull();
        //	invoiceFound.Receiver.Should().NotBeNull();
        //	invoiceFound.Sender.Should().NotBeNull();

        //}

        [Test]
		public void Test_InvoiceRepository_GetByKeyAccessNull_ShouldThrowException()
		{
			string existentKey = null;
			Action action = () => _repositoryInvoice.GetByKeyAccess(existentKey);
			action.Should().Throw<IdentifierUndefinedException>();
		}

		[Test]
		public void Test_InvoiceRepository_GetByKeyAccessEmpty_ShouldThrowException()
		{
			string existentKey = "";
			Action action = () => _repositoryInvoice.GetByKeyAccess(existentKey);
			action.Should().Throw<IdentifierUndefinedException>();
		}

        //[Test]
        //public void Test_InvoiceRepository_GetKeyAccess_NonExistentKeyAccess_ShoulBeOk()
        //{
        //	string nonExistentKey = "12312443665842356467";
        //	Invoice invoiceFound = _repositoryInvoice.GetByKeyAccess(nonExistentKey);
        //	invoiceFound.Should().BeNull();
        //}

        [Test]
        public void Test_InvoiceRepository_GetByNumber_ShouldBeOk()
        {
            int existentNumber = 1;
            Invoice invoiceFound = _repositoryInvoice.GetByNumber(existentNumber);
            invoiceFound.Should().NotBeNull();
            invoiceFound.Carrier.Should().NotBeNull();
            invoiceFound.Receiver.Should().NotBeNull();
            invoiceFound.Sender.Should().NotBeNull();

        }

        [Test]
        public void Test_InvoiceRepository_GetByNumberInvalid_ShouldThrowException()
        {
            int invalidNumber = -1;
            Action action = () => _repositoryInvoice.GetByNumber(invalidNumber);
            action.Should().Throw<IdentifierUndefinedException>();
        }

        [Test]
        public void Test_InvoiceRepository_GetByNumber_NonexistentNumber_ShouldBeOK()
        {
            int existentNumber = 100;
            Invoice invoiceFound = _repositoryInvoice.GetByNumber(existentNumber);
            invoiceFound.Should().BeNull();
        }


    }

}


