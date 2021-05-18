using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers
{
    public interface IInvoiceReceiverRepository
    {

        InvoiceReceiver Add(InvoiceReceiver entity);
        InvoiceReceiver GetByInvoice(long id);

    }
}
