using NDD.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDD.NFe.Domain.Features.InvoiceTaxes
{
    public class InvoiceTaxNonPositiveTotalValueInvoiceException : BusinessException
    {

        public InvoiceTaxNonPositiveTotalValueInvoiceException() : base("Valor total de impostos da nota não positivo.")
        {
        }

    }
}
