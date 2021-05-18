using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices
{
    public class InvoiceRepositoryPDFStatusEqualsOpenException : BusinessException
    {
        public InvoiceRepositoryPDFStatusEqualsOpenException() : base(ErrorCodes.Unauthorized, "Impossível gerar PDF de uma nota fiscal não emitida.")
        {
        }
    }
}
