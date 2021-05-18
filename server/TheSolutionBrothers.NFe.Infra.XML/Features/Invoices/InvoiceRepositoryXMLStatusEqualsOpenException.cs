using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices
{
    public class InvoiceRepositoryXMLStatusEqualsOpenException : BusinessException
    {
        public InvoiceRepositoryXMLStatusEqualsOpenException() : base(ErrorCodes.Unauthorized, "Impossível gerar xml de uma nota fiscal não emitida.")
        {
        }
    }
}
