using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class InvoiceNaruteOperationLengthOverflowException : BusinessException
    {

        public InvoiceNaruteOperationLengthOverflowException() : base(ErrorCodes.Unauthorized, "Natureza da operação possui mais que 70 caracteres.")
        {
        }

    }
}
