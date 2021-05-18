using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers
{
    public class ReceiverMapper : IMapper<Receiver, DestModel>
    {

        private IMapper<Address, EnderModel> _addressMapper;

        public ReceiverMapper(IMapper<Address, EnderModel> addressMapper)
        {
            _addressMapper = addressMapper;
        }

        public DestModel Map(Receiver entity)
        {
            return new DestModel()
            {
                CNPJ = (entity.Type == PersonType.PHYSICAL ? null: entity.Cnpj.Value),
                CPF = (entity.Type == PersonType.PHYSICAL ? entity.Cpf.Value : null),
                IE = entity.StateRegistration,
                XName = (entity.Type == PersonType.PHYSICAL ? entity.Name : entity.CompanyName),
                Ender = _addressMapper.Map(entity.Address)
            };
        }
        
    }
}
