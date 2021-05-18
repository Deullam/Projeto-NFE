using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
    public class ReceiverDeleteWithDependenceException : BusinessException
    {
        public ReceiverDeleteWithDependenceException() : base(ErrorCodes.Unauthorized, "Destinatario ralacionado com uma ou mais notas fiscais.")
        {
        }
    }
}
