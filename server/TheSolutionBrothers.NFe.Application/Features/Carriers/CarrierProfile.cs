using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;

namespace TheSolutionBrothers.NFe.Application.Features.Carriers
{
    public class CarrierProfile : Profile
    {

        public CarrierProfile() : base("CarrierProfile")
        {
            CreateMap<CarrierRegisterCommand, Carrier>()
                .ForPath(x => x.CNPJ.Value, y => y.MapFrom(z => z.CNPJ))
                .ForPath(x => x.CPF.Value, y => y.MapFrom(z => z.CPF));

            CreateMap<CarrierUpdateCommand, Carrier>()
                .ForPath(x => x.CNPJ.Value, y => y.MapFrom(z => z.CNPJ))
                .ForPath(x => x.CPF.Value, y => y.MapFrom(z => z.CPF));

            CreateMap<Carrier, CarrierViewModel>()
                .ForMember(x => x.CNPJ, y => y.MapFrom(z => z.CNPJ.FormattedValue))
                .ForMember(x => x.CPF, y => y.MapFrom(z => z.CPF.FormattedValue));
        }

    }
}
