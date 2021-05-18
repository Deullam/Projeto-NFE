using NDD.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDD.NFe.Domain.Features.Invoices
{
    public class InvoiceDeleteIssuedInvoiceExcption : BusinessException
    {
        public InvoiceDeleteIssuedInvoiceExcption() : base("Nota Fiscal emitida, não pode ser deletada.")
        {
        }
    }
}
