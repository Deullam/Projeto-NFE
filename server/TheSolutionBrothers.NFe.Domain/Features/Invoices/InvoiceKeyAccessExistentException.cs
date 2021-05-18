using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Invoices
{
    public class InvoiceKeyAccessExistentException : BusinessException
    {
        public InvoiceKeyAccessExistentException() : base(ErrorCodes.Unauthorized, "Chave de acesso ja existe.")
        {
        }
    }
}
