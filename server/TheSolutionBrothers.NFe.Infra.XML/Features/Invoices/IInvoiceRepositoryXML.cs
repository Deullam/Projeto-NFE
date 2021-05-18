using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices
{
    public interface IInvoiceRepositoryXML
    {

        void Export(Invoice invoice, string path);

    }
}
