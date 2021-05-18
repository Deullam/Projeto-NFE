using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Addresses
{
    public class AddressStateLengthOverflowException : BusinessException
    {

        public AddressStateLengthOverflowException() : base(ErrorCodes.Unauthorized, "Comprimento do nome do estado maior que 2 caracteres.")
        {
        }

    }
}
