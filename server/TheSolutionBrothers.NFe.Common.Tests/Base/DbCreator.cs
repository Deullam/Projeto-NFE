using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;

namespace TheSolutionBrothers.NFe.Common.Tests.Base
{
    public class DbCreator<T> : DropCreateDatabaseAlways<T> where T : ContextNfe
    {
        protected override void Seed(T context)
        {
            Address _address = ObjectMother.GetNewValidAddress();

            CPF _cpf = ObjectMother.GetValidCPF();
            CNPJ _cnpj = ObjectMother.GetValidCNPJ();

            Receiver legalReceiver = ObjectMother.GetNewValidLegalReceiver(ObjectMother.GetNewValidAddress(), _cnpj);
            Receiver physicalReceiver = ObjectMother.GetNewValidPhysicalReceiver(ObjectMother.GetNewValidAddress(), _cpf);

            legalReceiver.Address = context.Addresses.Add(legalReceiver.Address);
            physicalReceiver.Address = context.Addresses.Add(physicalReceiver.Address);

            context.Receivers.Add(physicalReceiver);
            context.Receivers.Add(legalReceiver);



            Sender legalSender = ObjectMother.GetNewValidSender(ObjectMother.GetNewValidAddress(), _cnpj);
            Sender Sender = ObjectMother.GetNewValidSender(ObjectMother.GetNewValidAddress(), _cnpj);

            legalSender.Address = context.Addresses.Add(legalSender.Address);
            Sender.Address = context.Addresses.Add(Sender.Address);

            context.Senders.Add(legalSender);
            context.Senders.Add(Sender);


            Carrier legalCarrier = ObjectMother.GetNewValidCarrierLegal(ObjectMother.GetNewValidAddress(), _cnpj);
            Carrier physicalCarrier = ObjectMother.GetNewValidCarrierPhysical(ObjectMother.GetNewValidAddress(), _cpf);

            legalCarrier.Address = context.Addresses.Add(legalCarrier.Address);
            context.SaveChanges();

            physicalCarrier.Address = context.Addresses.Add(physicalCarrier.Address);
            context.SaveChanges();


            context.Carriers.Add(legalCarrier);
            context.SaveChanges();

            context.Carriers.Add(physicalCarrier);

            Product product = ObjectMother.GetNewValidProduct(new TaxProduct());

            context.Products.Add(product);

            InvoiceItem invoiceItem = ObjectMother.GetNewInvoiceItemOk(null,product);
            InvoiceItem thirdItem = ObjectMother.GetNewInvoiceItemOk(null,product);
            Invoice invoice = ObjectMother.GetExistentValidOpenedInvoice(legalSender,physicalReceiver,legalCarrier,new List<InvoiceItem>() { invoiceItem, thirdItem});

            context.Invoices.Add(invoice);

            InvoiceItem secondinvoiceItem2 = ObjectMother.GetNewInvoiceItemOk(null, product);
            Invoice secondinvoice = ObjectMother.GetExistentValidOpenedInvoice(legalSender, physicalReceiver, legalCarrier, new List<InvoiceItem>() { secondinvoiceItem2 });

            context.Invoices.Add(secondinvoice);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
