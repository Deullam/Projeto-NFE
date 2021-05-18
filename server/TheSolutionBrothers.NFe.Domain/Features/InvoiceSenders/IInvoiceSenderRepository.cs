using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders
{
    public interface IInvoiceSenderRepository
    {

        InvoiceSender Add(InvoiceSender entity);
        InvoiceSender GetByInvoice(long id);

    }
}
