using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers
{
    public class SenderMapper : IMapper<Sender, EmitModel>
    {

        private IMapper<Address, EnderModel> _addressMapper;

        public SenderMapper(IMapper<Address, EnderModel> addressMapper)
        {
            _addressMapper = addressMapper;
        }

        public EmitModel Map(Sender entity)
        {
            return new EmitModel()
            {
                CNPJ = entity.Cnpj.Value,
                IE = entity.StateRegistration,
                IM = entity.MunicipalRegistration,
                XFant = entity.FancyName,
                XName = entity.CompanyName,
                Ender = _addressMapper.Map(entity.Address)
            };
        }
        
    }
}
