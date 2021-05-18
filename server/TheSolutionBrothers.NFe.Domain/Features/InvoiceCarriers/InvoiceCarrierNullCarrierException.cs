using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers
{
    public class InvoiceCarrierNullCarrierException : BusinessException
    {
        public InvoiceCarrierNullCarrierException() : base(ErrorCodes.Unauthorized, "Transportador não informado")
        {
        }
    }
}
