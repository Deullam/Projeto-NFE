using System;

namespace TheSolutionBrothers.NFe.Domain.Exceptions
{
    public class InvalidPathException : Exception
    {

        public InvalidPathException() : base("Caminho não informado.")
        {

        }

    }
}
