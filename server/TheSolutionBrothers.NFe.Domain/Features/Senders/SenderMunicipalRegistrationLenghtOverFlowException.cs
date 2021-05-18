using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Senders
{
    public class SenderMunicipalRegistrationLenghtOverflowException : BusinessException
    {
        public SenderMunicipalRegistrationLenghtOverflowException() : base(ErrorCodes.Unauthorized, "Comprimento da inscrição municipal maior que 15 caracteres.")
        {
        }
    }
}
