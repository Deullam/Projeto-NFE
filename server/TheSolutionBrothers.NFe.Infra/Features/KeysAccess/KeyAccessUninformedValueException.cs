using System;
using System.Runtime.Serialization;

namespace TheSolutionBrothers.Nfe.Infra.Features.KeysAccess
{

    public class KeyAccessUninformedValueException : Exception
    {
        
        public KeyAccessUninformedValueException() : base("Chave de acesso não informado.")
        {
        }

    }
}