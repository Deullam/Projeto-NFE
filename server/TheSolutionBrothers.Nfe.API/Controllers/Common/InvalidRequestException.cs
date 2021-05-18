using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.Nfe.API.Controllers.Common
{
    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(ErrorCodes errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        public ErrorCodes ErrorCode { get; }
    }
}
