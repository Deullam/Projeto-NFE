using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices
{
    public class InvoiceRepositoryXMLNullInvoiceException : BusinessException
    {

        public InvoiceRepositoryXMLNullInvoiceException() : base(ErrorCodes.Unauthorized, "Nota não informada.")
        {
        }

    }
}
