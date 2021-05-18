using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes
{
    public class InvoiceTaxNonPositiveTotalValueProductsException : BusinessException
    {

        public InvoiceTaxNonPositiveTotalValueProductsException() : base(ErrorCodes.Unauthorized, "Valor total de impostos dos produtos não positivo.")
        {
        }

    }
}
