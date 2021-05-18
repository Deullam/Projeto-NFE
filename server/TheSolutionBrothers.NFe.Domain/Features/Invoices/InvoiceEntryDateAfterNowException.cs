using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class InvoiceEntryDateAfterNowException : BusinessException
    {

        public InvoiceEntryDateAfterNowException() : base(ErrorCodes.Unauthorized, "Data de entrada superior a data atual.")
        {
        }

    }
}
