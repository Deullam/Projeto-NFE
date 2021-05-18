using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceItems
{

    public interface IInvoiceItemRepository
    {
        InvoiceItem Add(InvoiceItem entity);
        bool Update(InvoiceItem entity);
        InvoiceItem GetById(long id);
        IQueryable<InvoiceItem> GetAll();
        IQueryable<InvoiceItem> GetAll(int size);
		bool Remove(long id);


        IList<InvoiceItem> GetByInvoice(long idInvoice);
        IList<InvoiceItem> GetByProduct(long idProduct);
    }

}
