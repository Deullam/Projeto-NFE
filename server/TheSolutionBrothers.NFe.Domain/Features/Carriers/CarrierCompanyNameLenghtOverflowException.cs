using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class CarrierCompanyNameLenghtOverflowException : BusinessException
    {
        public CarrierCompanyNameLenghtOverflowException() : base(ErrorCodes.Unauthorized, "A Razão Social deve ser menor que 60 caracteres")
        {
        }
    }
}
