using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
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
using TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.PDF.Tests.Features.Invoices
{
    [TestFixture]
    public class PDFExportTests
    {
        InvoicePdfGenerator _pdfExport;
        private Invoice _invoice;

        private IList<InvoiceItem> _invoiceItens;
        private InvoiceItem _invoiceItem;
        private Product _product;
        private TaxProduct _taxProduct;

        private Address _address;
        private CPF _cpf;
        private CNPJ _cnpjReceiver;
        private CNPJ _cnpjSender;
        private KeyAccess _keyAccess;

        private InvoiceCarrier _invoiceCarrier;
        private Carrier _carrier;

        private InvoiceSender _invoiceSender;
        private Sender _sender;

        private InvoiceReceiver _invoiceReceiver;
        private Receiver _receiver;

        private InvoiceTax _invoiceTax;

        [SetUp]
        public void Initalize()
        {
            _address = ObjectMother.GetExistentValidAddress();
            _cpf = ObjectMother.GetValidCPF();
            _cnpjReceiver = ObjectMother.GetValidCNPJ();
            _cnpjSender = new CNPJ() { Value = "00745557000151" };
            _keyAccess = ObjectMother.GetValidKeyAccess();
            _taxProduct = ObjectMother.GetValidTaxProduct();

            _product = ObjectMother.GetExistentValidProduct(_taxProduct);

            _carrier = ObjectMother.GetExistentValidCarrierPhysical(_address, _cpf);
            _sender = ObjectMother.GetExistentValidSender(_address, _cnpjSender);
            _receiver = ObjectMother.GetExistentValidLegalReceiver(_address, _cnpjReceiver);


            _invoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(_carrier, _invoice);
            _invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(_invoice, _sender);
            _invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(_invoice, _receiver);

            _invoiceItem = ObjectMother.GetExistentConsolidatedInvoiceItem(_invoice,_product);
            _invoiceItens = new List<InvoiceItem>() { _invoiceItem };
            _invoiceTax = ObjectMother.GetExistentValidInvoiceTax(_invoice);

            _invoice = ObjectMother.GetExistentValidIssuedInvoice(_keyAccess,_sender, _receiver, _carrier, _invoiceSender, _invoiceReceiver, _invoiceCarrier, _invoiceTax, _invoiceItens);

            _invoiceCarrier.Invoice = _invoice;
            _invoiceSender.Invoice = _invoice;
            _invoiceReceiver.Invoice = _invoice;
            _invoiceItem.Invoice = _invoice;
            _invoiceItens[0] =_invoiceItem;
            _invoiceTax.Invoice = _invoice;
            _pdfExport = new InvoicePdfGenerator();
            
        }

        [Test]
        public void Test_PDFExport_Export_WithFreightResponsabilitySender_ShouldBeOK()
        {
           string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(desktop, "Nota.PDF");
            Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(_keyAccess, _sender, _receiver, _carrier, _invoiceSender, _invoiceReceiver, _invoiceCarrier, _invoiceTax, _invoiceItens);

            _pdfExport.Export(invoice,path);

            File.Exists(path).Should().BeTrue();
        }

        [Test]
        public void Test_PDFExport_Export_WithFreightResponsabilityReceiver_ShouldBeOK()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(desktop, "Nota.PDF");

            Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(_keyAccess, _sender, _receiver, _carrier, _invoiceSender, _invoiceReceiver, _invoiceCarrier, _invoiceTax, _invoiceItens);

            invoice.Carrier.FreightResponsability = FreightResponsability.RECEIVER;
            _pdfExport.Export(invoice, path);

            File.Exists(path).Should().BeTrue();
        }


    }
}
