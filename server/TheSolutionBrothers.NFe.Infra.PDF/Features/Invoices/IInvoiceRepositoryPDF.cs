using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices
{
    public interface IInvoiceRepositoryPDF
    {
        void Export(Invoice invoice, string path);
    }
}
