using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceItems
{
    public class InvoiceItemNullProductException : BusinessException
    {
        public InvoiceItemNullProductException() : base(ErrorCodes.Unauthorized, "Produto não informado.")
        {
        }
    }
}
