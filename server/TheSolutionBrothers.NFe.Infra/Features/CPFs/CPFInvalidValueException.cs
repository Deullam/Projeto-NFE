using System;
using System.Runtime.Serialization;

namespace TheSolutionBrothers.Nfe.Infra.Features.CPFs
{
    
    public class CPFInvalidValueException : Exception
    {

        public CPFInvalidValueException() : base("CPF inválido.")
        {
        }

    }
}