using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Senders.Commands;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Senders;

namespace TheSolutionBrothers.NFe.Application.Features.Senders
{
    public class SenderProfile : Profile
    {

        public SenderProfile() : base("SenderProfile")
        {
            CreateMap<SenderRegisterCommand, Sender>()
               .ForPath(x => x.Cnpj.Value, y => y.MapFrom(z => z.Cnpj));

            CreateMap<SenderUpdateCommand, Sender>()
                .ForPath(x => x.Cnpj.Value, y => y.MapFrom(z => z.Cnpj));
            
            CreateMap<Sender, SenderViewModel>()
               .ForMember(x => x.Cnpj, y => y.MapFrom(z => z.Cnpj.FormattedValue));
        }

    }
}
