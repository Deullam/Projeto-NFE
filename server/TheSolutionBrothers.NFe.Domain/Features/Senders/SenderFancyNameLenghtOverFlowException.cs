using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Senders
{
    public class SenderFancyNameLenghtOverflowException : BusinessException
    {
        public SenderFancyNameLenghtOverflowException() : base(ErrorCodes.Unauthorized, "Comprimento do nome fantasia maior que 60 caracteres.")
        {
        }
    }
}
