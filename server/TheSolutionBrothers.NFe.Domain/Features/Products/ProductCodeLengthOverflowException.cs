using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Products
{

    public class ProductCodeLengthOverflowException : BusinessException
    {

        public ProductCodeLengthOverflowException() : base(ErrorCodes.Unauthorized, "Comprimento do código maior que 15 caracteres.")
        {
        }

    }
}
