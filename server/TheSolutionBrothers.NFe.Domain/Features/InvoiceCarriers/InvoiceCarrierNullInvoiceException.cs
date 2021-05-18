using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers
{
    public class InvoiceCarrierNullInvoiceException : BusinessException
    {
        public InvoiceCarrierNullInvoiceException() : base(ErrorCodes.Unauthorized, "Nota Fiscal não informada")
        {
        }
    }
}
