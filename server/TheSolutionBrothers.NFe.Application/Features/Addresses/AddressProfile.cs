using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;

namespace TheSolutionBrothers.NFe.Application.Features.Addresses
{
    public class AddressProfile : Profile
    {

        public AddressProfile() : base("AddressProfile")
        {
            CreateMap<AddressCommand, Address>();

            CreateMap<Address, AddessViewModel>();
        }

    }
}
