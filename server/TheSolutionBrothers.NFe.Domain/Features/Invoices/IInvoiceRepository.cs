using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Invoices
{
    public interface IInvoiceRepository
    {
        Invoice Add(Invoice entity);
        bool Update(Invoice entity);
        Invoice GetById(long id);
		IQueryable<Invoice> GetAll();
		IQueryable<Invoice> GetAll(int size);
		bool Remove(long id);

        IList<Invoice>GetByCarrier(long idCarrier);
        IList<Invoice> GetByReceiver(long idReceiver);
        IList<Invoice> GetBySender(long idSender);

        Invoice GetByKeyAccess(String keyAccess);
        Invoice GetByNumber(int number);

    }
}
