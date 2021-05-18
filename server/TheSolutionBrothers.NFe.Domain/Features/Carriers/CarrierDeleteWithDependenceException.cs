using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class CarrierDeleteWithDependenceException : BusinessException
    {
        public CarrierDeleteWithDependenceException() : base(ErrorCodes.Unauthorized, "Transportador relacionado com uma ou mais notas fiscais.")
        {
        }
    }
}
