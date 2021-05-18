using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Addresses
{
    public class AddressStreetNameLengthOverflowException : BusinessException
    {

        public AddressStreetNameLengthOverflowException() : base(ErrorCodes.Unauthorized, "Comprimento do logradouro maior que 60 caracteres.")
        {
        }

    }
}
