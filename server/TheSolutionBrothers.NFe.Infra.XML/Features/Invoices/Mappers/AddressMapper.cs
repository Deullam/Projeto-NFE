using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers
{
    public class AddressMapper : IMapper<Address, EnderModel>
    {

        public EnderModel Map(Address entity)
        {
            return new EnderModel()
            {
                XLgr = entity.StreetName,
                Nro = entity.Number,
                XBairro = entity.Neighborhood,
                XMun = entity.City,
                UF = entity.State,
                XPais = entity.Country
            };
        }

    }

}
