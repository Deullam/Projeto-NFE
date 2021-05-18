using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices
{
    public class InvoiceRepositoryPDFNullInvoiceException : BusinessException
    {

        public InvoiceRepositoryPDFNullInvoiceException() : base(ErrorCodes.Unauthorized, "Nota não informada.")
        {
        }

    }
}
