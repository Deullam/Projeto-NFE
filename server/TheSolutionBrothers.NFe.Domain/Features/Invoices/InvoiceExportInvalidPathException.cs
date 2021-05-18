using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Invoices
{
    public class InvoiceExportInvalidPathException : BusinessException
    {
        public InvoiceExportInvalidPathException() : base(ErrorCodes.Unauthorized, "Caminho não informado.")
        {
        }
    }
}
