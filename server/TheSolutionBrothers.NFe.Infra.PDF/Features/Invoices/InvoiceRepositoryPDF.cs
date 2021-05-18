using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices
{
    public class InvoiceRepositoryPDF : IInvoiceRepositoryPDF
    {
        private InvoicePdfGenerator _PdfGenerator;

        public InvoiceRepositoryPDF(InvoicePdfGenerator pdfGenerator)
        {
            _PdfGenerator = pdfGenerator;

        }

        public void Export(Invoice invoice, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new InvalidPathException();
            }

            if (invoice == null)
            {
                throw new InvoiceRepositoryPDFNullInvoiceException();
            }

            if (invoice.Status != InvoiceStatus.ISSUED)
            {
                throw new InvoiceRepositoryPDFStatusEqualsOpenException();
            }

            invoice.Validate();

            _PdfGenerator.Export(invoice,path);
        }
        
    }
}
