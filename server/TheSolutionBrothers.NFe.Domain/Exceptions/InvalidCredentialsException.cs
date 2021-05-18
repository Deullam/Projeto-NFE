using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Exceptions
{
    public class InvalidCredentialsException : BusinessException
    {

        public InvalidCredentialsException() : base(ErrorCodes.Unauthorized, "Usuário ou senha inválida.")
        {

        }

    }

}
