using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Products
{

    public class ProductCurrentValueLowerOrEqualThanZeroException : BusinessException
    {

        public ProductCurrentValueLowerOrEqualThanZeroException() : base(ErrorCodes.Unauthorized, "Valor igual ou menor que zerov.")
        {
        }

    }
}
