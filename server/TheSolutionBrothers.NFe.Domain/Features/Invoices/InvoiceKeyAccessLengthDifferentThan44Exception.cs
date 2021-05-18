using NDD.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDD.NFe.Domain.Features.Carriers
{
    public class InvoiceKeyAccessLengthDifferentThan44Exception : BusinessException
    {

        public InvoiceKeyAccessLengthDifferentThan44Exception() : base("Chave de acesso não possui 44 caracteres.")
        {
        }

    }
}
