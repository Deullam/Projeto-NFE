using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Addresses
{
    public class AddressCountryLengthOverflowException : BusinessException
    {

        public AddressCountryLengthOverflowException() : base(ErrorCodes.Unauthorized, "Comprimento do nome do país maior que 50 caracteres.")
        {
        }

    }
}
