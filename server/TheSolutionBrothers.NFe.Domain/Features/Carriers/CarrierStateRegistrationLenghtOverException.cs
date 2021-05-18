﻿using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class CarrierStateRegistrationLenghtOverflowException : BusinessException
    {
        public CarrierStateRegistrationLenghtOverflowException( ) : base(ErrorCodes.Unauthorized, "Inscrição Estadual deve ter menos que 15 caracteres ")
        {
        }
    }
}
