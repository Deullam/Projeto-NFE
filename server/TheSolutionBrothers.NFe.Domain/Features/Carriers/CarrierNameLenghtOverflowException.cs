using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class CarrierNameLenghtOverflowException : BusinessException
    {
        public CarrierNameLenghtOverflowException( ) : base(ErrorCodes.Unauthorized, "O nome é maior que 60 carácteres")
        {
        }
    }
}
