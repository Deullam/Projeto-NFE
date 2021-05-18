using Effort;
using Effort.Provider;
using NUnit.Framework;
using System;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Infra.Data.Tests.Context;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using System.Collections.Generic;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Initializer
{
    [TestFixture]
    public class EffortTestBase
    {
        protected FakeDbContext _contexto;

        protected Carrier _carrierLegalSeed;
        protected Carrier _carrierPhysicalSeed;
        protected Carrier _carrierToDelete;
		protected Sender _senderSeed;
		protected Sender _senderToDelete;
        protected Product _productSeed;
        protected Product _productToDelete;
        protected Receiver _receiverLegalSeed;
        protected Receiver _receiverPhysicalSeed;
        protected Receiver _receiverToDelete;
		protected Invoice _invoiceSeed;
		protected Invoice _invoiceToDelete;
		protected InvoiceItem _invoiceItemSeed;
		protected InvoiceItem _invoiceItemToDelete;

		[OneTimeSetUp]
        public void InitializeOneTime()
        {
            EffortProviderConfiguration.RegisterProvider();
        }

        [SetUp]
        public virtual void Initialize()
        {
            var connection = DbConnectionFactory.CreatePersistent(Guid.NewGuid().ToString());
            _contexto = new FakeDbContext(connection);

            EffortProviderFactory.ResetDb();
            
            Address addressLegal = ObjectMother.GetNewValidAddress();
            Address addressPhysical = ObjectMother.GetNewValidAddress();
			Address addressSender = ObjectMother.GetNewValidAddress();
			CNPJ cnpj = ObjectMother.GetValidCNPJ();
            CPF cpf = ObjectMother.GetValidCPF();

            _carrierLegalSeed = ObjectMother.GetNewValidCarrierLegal(addressLegal, cnpj);
            _carrierPhysicalSeed = ObjectMother.GetNewValidCarrierPhysical(addressPhysical, cpf);

            _receiverLegalSeed = ObjectMother.GetNewValidLegalReceiver(addressLegal, cnpj);
            _receiverPhysicalSeed = ObjectMother.GetNewValidPhysicalReceiver(addressLegal, cpf);

            _carrierToDelete = ObjectMother.GetNewValidCarrierPhysical(addressPhysical, cpf);
            _receiverToDelete = ObjectMother.GetNewValidPhysicalReceiver(addressPhysical, cpf);

			_senderSeed = ObjectMother.GetNewValidSender(addressSender, cnpj);
			_senderToDelete = ObjectMother.GetNewValidSender(addressSender, cnpj);

            _productSeed = ObjectMother.GetNewValidProduct(new TaxProduct());
            _productToDelete = ObjectMother.GetNewValidProduct(new TaxProduct());

            _contexto.Carriers.Add(_carrierPhysicalSeed);
            _contexto.Carriers.Add(_carrierLegalSeed);
            _contexto.Carriers.Add(_carrierToDelete);
			_contexto.Senders.Add(_senderSeed);
			_contexto.Senders.Add(_senderToDelete);

            _contexto.Receivers.Add(_receiverPhysicalSeed);
            _contexto.Receivers.Add(_receiverLegalSeed);
            _contexto.Receivers.Add(_receiverToDelete);

            _contexto.Products.Add(_productSeed);
            _contexto.Products.Add(_productToDelete);

			_invoiceSeed = ObjectMother.GetNewValidOpenedInvoice(_senderSeed,_receiverLegalSeed,_carrierLegalSeed, null);
			_invoiceItemSeed = ObjectMother.GetNewInvoiceItemOk(_invoiceSeed, _productSeed);
			_invoiceSeed.InvoiceItems = new List<InvoiceItem>() { _invoiceItemSeed };

			_contexto.Invoices.Add(_invoiceSeed);

			_invoiceToDelete = ObjectMother.GetNewValidOpenedInvoice(_senderSeed, _receiverLegalSeed, _carrierLegalSeed, null);
			_invoiceItemToDelete = ObjectMother.GetNewInvoiceItemOk(_invoiceToDelete, _productSeed);
			_invoiceToDelete.InvoiceItems = new List<InvoiceItem>() { _invoiceItemToDelete };
            _invoiceToDelete.Number = 2;

			_contexto.Invoices.Add(_invoiceToDelete);

			_contexto.SaveChanges();
		}
	}
}
