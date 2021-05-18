using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using System;

namespace TheSolutionBrothers.NFe.Application.Mappers
{
    public class DomainToViewModelProfile : Profile
    {

        public DomainToViewModelProfile() : base("DomainToViewModelProfile")
        {
			CreateMap<Sender, SenderViewModel>()
			   .ForPath(x => x.Cnpj, y => y.MapFrom(z => z.Cnpj.FormattedValue));

            CreateMap<Receiver, ReceiverViewModel>()
                .ForMember(x => x.PersonType, y => y.MapFrom(z => z.Type))
                .ForPath(x => x.Cnpj, y => y.MapFrom(z => z.Cnpj.FormattedValue))
                .ForPath(x => x.Cpf, y => y.MapFrom(z => z.Cpf.FormattedValue));

            CreateMap<Carrier, CarrierViewModel>()
                .ForPath(x => x.CNPJ, y => y.MapFrom(z => z.CNPJ.FormattedValue))
                .ForPath(x => x.CPF, y => y.MapFrom(z => z.CPF.FormattedValue));

			CreateMap<Product, ProductViewModel>()
				.ForPath(x => x.IcmsAliquot, y => y.MapFrom(z => z.TaxProduct.IcmsAliquot))
				.ForPath(x => x.IpiAliquot, y => y.MapFrom(z => z.TaxProduct.IpiAliquot));

            CreateMap<Invoice, InvoiceViewModel>()
                .ForMember(x => x.SenderName, y => y.MapFrom(z => z.Sender.CompanyName))
                .ForMember(x => x.ReceiverName, y => y.MapFrom(z => z.Receiver.Name))
                .ForMember(x => x.CarrierName, y => y.MapFrom(z => z.Carrier.Name))
                .ForPath(x => x.KeyAccess, y => y.MapFrom(z => z.KeyAccess.Value))
                .ForPath(x => x.EntryDate, y => y.MapFrom(z => z.EntryDate.ToUniversalTime()))
				.ForPath(x => x.IssueDate, y => y.MapFrom(z => z.IssueDate == null ? "" : z.IssueDate.Value.ToString()));

            CreateMap<InvoiceTax, InvoiceTaxViewModel>();
            CreateMap<InvoiceItem, InvoiceItemViewModel>();
        }

    }
}
