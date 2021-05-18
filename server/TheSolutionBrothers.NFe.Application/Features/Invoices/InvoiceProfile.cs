using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Commands;
using TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices
{
    public class InvoiceProfile : Profile
    {

        public InvoiceProfile() : base("InvoiceProfile")
        {
            CreateMap<InvoiceRegisterCommand, Invoice>();

            CreateMap<InvoiceUpdateCommand, Invoice>();

            CreateMap<InvoiceItemRegisterCommand, InvoiceItem>();

            CreateMap<InvoiceItemUpdateCommand, InvoiceItem>();

            CreateMap<Invoice, InvoiceViewModel>()
                .ForMember(x => x.SenderName, y => y.MapFrom(z => z.Sender.CompanyName))
                .ForMember(x => x.EntryDate, y => y.MapFrom(z => z.EntryDate.ToString("yyyy-MM-ddTHH:mm:ssZ")))
                .ForMember(x => x.IssueDate, y => y.MapFrom(z => z.IssueDate == null ? "" : z.IssueDate.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")))
                .ForMember(x => x.KeyAccess, y => y.MapFrom(z => z.KeyAccess == null ? "" : z.KeyAccess.Value));

            CreateMap<InvoiceTax, InvoiceTaxViewModel>();

            CreateMap<InvoiceItem, InvoiceItemViewModel>();
        }

    }
}
