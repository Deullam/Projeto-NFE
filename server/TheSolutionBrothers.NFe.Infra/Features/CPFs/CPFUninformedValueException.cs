using System;
using System.Runtime.Serialization;

namespace TheSolutionBrothers.Nfe.Infra.Features.CPFs
{

    public class CPFUninformedValueException : Exception
    {
        
        public CPFUninformedValueException() : base("CPF não informado.")
        {
        }

    }
}