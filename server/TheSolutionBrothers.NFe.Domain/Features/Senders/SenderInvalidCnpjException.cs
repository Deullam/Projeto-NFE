using NDD.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDD.NFe.Domain.Features.Senders
{
    public class SenderInvalidCnpjException : BusinessException
    {
        public SenderInvalidCnpjException() : base("cnpj invalido.")
        {
        }
    }
}
