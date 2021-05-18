using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.Nfe.Infra.Features.CNPJs
{
    public class CNPJUninformedValueException : Exception
    {

        public CNPJUninformedValueException() : base("CNPJ não informado.")
        {

        }

    }
}
