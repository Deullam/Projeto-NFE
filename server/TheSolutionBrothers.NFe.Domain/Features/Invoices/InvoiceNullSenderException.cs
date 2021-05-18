using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class InvoiceNullSenderException : BusinessException
    {

        public InvoiceNullSenderException() : base(ErrorCodes.Unauthorized, "Emitente não informado.")
        {
        }

    }
}
