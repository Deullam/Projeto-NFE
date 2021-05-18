using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Addresses
{
    public class AddressNeighborhoodLengthOverflowException : BusinessException
    {

        public AddressNeighborhoodLengthOverflowException() : base(ErrorCodes.Unauthorized, "Comprimento do nome do bairro maior que 40 caracteres.")
        {
        }

    }
}
