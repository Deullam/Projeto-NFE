using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.Nfe.Infra.XML.ExtensionMethods;
using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices
{
    public class InvoiceRepositoryXML : IInvoiceRepositoryXML
    {

        public IMapper<Invoice, NFeModel> _invoiceMapper;

        public InvoiceRepositoryXML(IMapper<Invoice, NFeModel> invoiceMapper)
        {
            _invoiceMapper = invoiceMapper;
        }

        public void Export(Invoice invoice, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new InvalidPathException();
            }

			if (invoice == null)
			{
				throw new InvoiceRepositoryXMLNullInvoiceException();
			}

            if (invoice.Status != InvoiceStatus.ISSUED)
			{
				throw new InvoiceRepositoryXMLStatusEqualsOpenException();
			}

			invoice.Validate();

            NFeModel nfeModel = _invoiceMapper.Map(invoice);
            nfeModel.GenerateXML(path);
        }

    }
}
