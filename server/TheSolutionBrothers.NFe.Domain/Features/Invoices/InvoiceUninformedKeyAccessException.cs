using NDD.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDD.NFe.Domain.Features.Carriers
{
    public class InvoiceUninformedKeyAccessException : BusinessException
    {

        public InvoiceUninformedKeyAccessException() : base("Chave de acesso não informada.")
        {
        }

    }
}
