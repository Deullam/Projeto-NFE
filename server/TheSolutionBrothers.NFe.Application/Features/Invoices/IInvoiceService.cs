using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using System;
using System.Linq;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Commands;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Queries;
using TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels;
using System.Collections.Generic;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices
{
    public interface IInvoiceService
    {

		long Add(InvoiceRegisterCommand entity);
        bool Update(InvoiceUpdateCommand entity);
		InvoiceViewModel GetById(long id);
        IQueryable<Invoice> GetAll(InvoiceGetAllQuery query);
		IQueryable<Invoice> GetAll();
        bool Remove(InvoiceDeleteCommand entity);
        
        List<long> AddItems(long id, List<InvoiceItemRegisterCommand> commands);
        bool UpdateItems(long id, List<InvoiceItemUpdateCommand> commands);
        bool DeleteItems(long id, InvoiceItemDeleteCommand command);

        void ExportToXML(Invoice entity, String path);
		void ExportToPDF(Invoice entity, String path);
        Invoice Issue(Invoice invoice, double freightValue);

    }
}
