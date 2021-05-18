using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Addresses
{
    public class AddressNegativeNumberException : BusinessException
    {

        public AddressNegativeNumberException() : base(ErrorCodes.Unauthorized, "Número negativo.")
        {
        }

    }
}
