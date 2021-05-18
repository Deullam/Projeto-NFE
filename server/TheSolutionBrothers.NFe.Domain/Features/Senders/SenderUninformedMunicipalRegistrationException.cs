using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Senders
{
    public class SenderUninformedMunicipalRegistrationException : BusinessException
    {
        public SenderUninformedMunicipalRegistrationException() : base(ErrorCodes.Unauthorized, "Inscrição municipal não informada.")
        {
        }
    }
}
