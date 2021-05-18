using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Senders
{
    public class SenderDeleteWithDependenceException : BusinessException
    {
        public SenderDeleteWithDependenceException() : base(ErrorCodes.Unauthorized, "Emitente está relacionado com uma ou mais notas fiscais.")
        {
        }
    }
}
