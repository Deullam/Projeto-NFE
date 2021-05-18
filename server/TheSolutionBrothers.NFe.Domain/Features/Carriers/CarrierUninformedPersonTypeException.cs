using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class CarrierUninformedPersonTypeException : BusinessException
    {
        public CarrierUninformedPersonTypeException( ) : base(ErrorCodes.Unauthorized, "Tipo de pessoa (Física/Jurídica) deve ser informado")
        {
        }
    }
}
