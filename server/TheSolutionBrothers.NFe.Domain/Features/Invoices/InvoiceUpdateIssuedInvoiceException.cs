using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Invoices
{
    public class InvoiceUpdateIssuedInvoiceException : BusinessException
    {
        public InvoiceUpdateIssuedInvoiceException() : base(ErrorCodes.Unauthorized, "Nota Fiscal emitida, não pode ser atualizada.")
        {
        }
    }
}
