using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Addresses
{
    public class AddressCityLengthOverflowException : BusinessException
    {

        public AddressCityLengthOverflowException() : base(ErrorCodes.Unauthorized, "Comprimento do nome da cidade maior que 50 caracteres.")
        {
        }

    }
}
