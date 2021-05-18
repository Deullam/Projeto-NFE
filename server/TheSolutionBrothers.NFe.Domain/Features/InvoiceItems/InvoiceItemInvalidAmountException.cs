using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceItems
{
    public class InvoiceItemInvalidAmountException : BusinessException
    {
        public InvoiceItemInvalidAmountException() : base(ErrorCodes.Unauthorized, "Quantidade invalida.")
        {
        }
    }
}
