using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Products.Commands;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Products;

namespace TheSolutionBrothers.NFe.Application.Features.Products
{
    public class ProductProfile : Profile
    {

        public ProductProfile() : base("ProductProfile")
        {
            CreateMap<ProductRegisterCommand, Product>();

            CreateMap<ProductUpdateCommand, Product>();

            CreateMap<Product, ProductViewModel>()
                .ForMember(x => x.IcmsAliquot, y => y.MapFrom(z => z.TaxProduct.IcmsAliquot))
                .ForMember(x => x.IpiAliquot, y => y.MapFrom(z => z.TaxProduct.IpiAliquot));
        }

    }
}
