using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Invoices
{
    public class InvoiceIssueIssuedInvoiceException : BusinessException
    {
        public InvoiceIssueIssuedInvoiceException() : base(ErrorCodes.Unauthorized, "Nota Fiscal emitida, nao pode ser emitida novamente.")
        {
        }
    }
}
