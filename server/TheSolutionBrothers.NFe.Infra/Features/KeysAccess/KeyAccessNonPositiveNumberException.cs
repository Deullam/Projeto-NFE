using System;
using System.Runtime.Serialization;

namespace TheSolutionBrothers.Nfe.Infra.Features.KeysAccess
{

    public class KeyAccessNonPositiveNumberException : Exception
    {
        
        public KeyAccessNonPositiveNumberException() : base("Número da chave de acesso não positivo.")
        {
        }

    }
}