using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Senders
{
    public class SenderCompanyNameLenghtOverflowException : BusinessException
    {
        public SenderCompanyNameLenghtOverflowException() : base(ErrorCodes.Unauthorized, "Nome da companhia maior que 60 caracteres.")
        {
        }
    }
}
