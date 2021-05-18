﻿using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Senders
{
    public class SenderNullAddressException : BusinessException
    {
        public SenderNullAddressException() : base(ErrorCodes.Unauthorized, "Endereço não informado.")
        {
        }
    }
}
