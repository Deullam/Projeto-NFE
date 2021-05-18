using NDD.Nfe.Infra.ExtensionMethods;
using NDD.Nfe.Infra.Features.CNPJs;
using NDD.Nfe.Infra.Features.CPFs;
using NDD.NFe.Common.Tests.Features.ObjectMothers;
using NDD.NFe.Domain.Features.Carriers;
using NDD.NFe.Domain.Features.InvoiceItems;
using NDD.NFe.Domain.Features.Invoices;
using NDD.NFe.Domain.Features.Products;
using NDD.NFe.Domain.Features.Receivers;
using NDD.NFe.Domain.Features.Senders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDD.NFe.Infra.Tests.ExtensionMethods
{

    [TestFixture]
    public class XMLGeneratorTests
    {

        [Test]
        public void Test_XMLGenerator_GenerateXML_Invoice_ShouldBeOk()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML - teste integração infra.xml";

            CPF cpfCarrier = ObjectMother.GetValidCPF();
            CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
            CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

            Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
            Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
            Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

            IList<InvoiceItem> items = new List<InvoiceItem>();
            
            Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(sender, receiver, carrier, items);

            Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
            InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
            items.Add(item);

            double freightValue = 20;
            invoice.Issue(freightValue);

            invoice.GenerateXML(path);

        }

    }
}
