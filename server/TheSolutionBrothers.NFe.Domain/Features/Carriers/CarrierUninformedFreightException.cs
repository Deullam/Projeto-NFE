using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class CarrierUninformedFreightResponsabilityException : BusinessException
    {
        public CarrierUninformedFreightResponsabilityException() : base(ErrorCodes.Unauthorized, "Responsabilidade do Frete deve ser informado")
        {
        }
    }
}
