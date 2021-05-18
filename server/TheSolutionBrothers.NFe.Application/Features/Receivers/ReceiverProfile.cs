using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;

namespace TheSolutionBrothers.NFe.Application.Features.Receivers
{
    public class ReceiverProfile : Profile
    {

        public ReceiverProfile() : base("ReceiverProfile")
        {
            CreateMap<ReceiverRegisterCommand, Receiver>()
                .ForMember(x => x.Type, y => y.MapFrom(z => z.PersonType))
                .ForPath(x => x.Cnpj.Value, y => y.MapFrom(z => z.Cnpj))
                .ForPath(x => x.Cpf.Value, y => y.MapFrom(z => z.Cpf));

            CreateMap<ReceiverUpdateCommand, Receiver>()
                .ForMember(x => x.Type, y => y.MapFrom(z => z.PersonType))
                .ForPath(x => x.Cnpj.Value, y => y.MapFrom(z => z.Cnpj))
                .ForPath(x => x.Cpf.Value, y => y.MapFrom(z => z.Cpf));

            CreateMap<Receiver, ReceiverViewModel>()
                .ForMember(x => x.PersonType, y => y.MapFrom(z => z.Type))
                .ForMember(x => x.Cnpj, y => y.MapFrom(z => z.Cnpj.FormattedValue))
                .ForMember(x => x.Cpf, y => y.MapFrom(z => z.Cpf.FormattedValue));
        }

    }
}
