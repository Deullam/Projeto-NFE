using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes
{
    public class InvoiceTaxNonPositiveIcmsValueException : BusinessException
    {

        public InvoiceTaxNonPositiveIcmsValueException() : base(ErrorCodes.Unauthorized, "Valor de ICMS não positivo.")
        {
        }

    }
}
