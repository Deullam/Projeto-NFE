using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers
{
    public interface IInvoiceCarrierRepository
    {

        InvoiceCarrier Add(InvoiceCarrier entity);
        InvoiceCarrier GetByInvoice(long id);

    }
}
