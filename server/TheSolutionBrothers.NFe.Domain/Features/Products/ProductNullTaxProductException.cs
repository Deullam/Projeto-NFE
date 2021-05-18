using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Products
{

    public class ProductNullTaxProductException : BusinessException
    {

        public ProductNullTaxProductException() : base(ErrorCodes.Unauthorized, "Imposto do produto nulo.")
        {
        }

    }
}
