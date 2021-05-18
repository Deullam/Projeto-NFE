using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.Nfe.Infra.Features.CNPJs
{
    public class CNPJInvalidValueException : Exception
    {

        public CNPJInvalidValueException() : base("CNPJ inválido.")
        {

        }

    }
}
