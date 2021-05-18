using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers
{
    public class InvoiceCarrierNullReceiverException : BusinessException
    {
        public InvoiceCarrierNullReceiverException() : base(ErrorCodes.Unauthorized, "Recebedor não informado")
        {
        }
    }
}
