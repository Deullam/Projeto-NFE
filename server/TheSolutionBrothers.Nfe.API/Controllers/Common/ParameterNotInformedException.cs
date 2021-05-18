using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.Nfe.API.Controllers.Common
{
    public class ParameterNotInformedException : InvalidRequestException
    {
        public ParameterNotInformedException() : base(ErrorCodes.Unauthorized, "Parâmetro não informado.")
        {
        }
    }
}
