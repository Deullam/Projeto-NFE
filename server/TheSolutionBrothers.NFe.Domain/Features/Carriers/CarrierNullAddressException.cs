using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class CarrierNullAddressException : BusinessException
    {
        public CarrierNullAddressException() : base(ErrorCodes.Unauthorized, "Endereço deve ser informado")
        {
        }
    }
}
