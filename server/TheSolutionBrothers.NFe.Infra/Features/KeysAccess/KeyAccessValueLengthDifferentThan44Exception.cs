using System;
using System.Runtime.Serialization;

namespace TheSolutionBrothers.Nfe.Infra.Features.KeysAccess
{

    public class KeyAccessValueLengthDifferentThan44Exception : Exception
    {
        
        public KeyAccessValueLengthDifferentThan44Exception() : base("Chave de acesso não possui 44 caracteres.")
        {
        }

    }
}