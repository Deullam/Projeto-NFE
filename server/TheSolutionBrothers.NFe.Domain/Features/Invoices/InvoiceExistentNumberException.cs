using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Invoices
{
    public class InvoiceExistentNumberException : BusinessException
    {
        public InvoiceExistentNumberException() : base(ErrorCodes.Unauthorized, "Nota fiscal com numero existente.")
        {
        }
    }
}
