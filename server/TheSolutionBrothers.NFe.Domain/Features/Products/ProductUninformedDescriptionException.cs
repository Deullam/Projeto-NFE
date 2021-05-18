using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Products
{

    public class ProductUninformedDescriptionException : BusinessException
    {

        public ProductUninformedDescriptionException() : base(ErrorCodes.Unauthorized, "Descrição não informado.")
        {
        }

    }
}
