using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders
{
    public class InvoiceSenderNullInvoiceException : BusinessException
    {
        public InvoiceSenderNullInvoiceException() : base(ErrorCodes.Unauthorized, "Nota fiscal não informada.")
        {
        }
    }
}
