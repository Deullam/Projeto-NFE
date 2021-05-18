using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Addresses
{
    public class AddressUninformedNeighborhoodException : BusinessException
    {

        public AddressUninformedNeighborhoodException() : base(ErrorCodes.Unauthorized, "Bairro não informado.")
        {
        }

    }
}
