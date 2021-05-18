using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Queries;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;

namespace TheSolutionBrothers.NFe.Application.Features.Carriers
{
    public class CarrierService : ICarrierService
    {

        private readonly ICarrierRepository _carrierRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public CarrierService(
            ICarrierRepository CarrierRepository, 
            IAddressRepository addressRepository,
            IInvoiceRepository invoiceRepository,
            IMapper mapper)
        {
            _carrierRepository = CarrierRepository;
            _addressRepository = addressRepository;
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }
        

        public long Add(CarrierRegisterCommand command)
        {
            var entity = _mapper.Map<Carrier>(command);

            entity.Validate();

            entity.Address = _addressRepository.Save(entity.Address);
            return _carrierRepository.Add(entity).Id;
        }

        public bool Remove(CarrierDeleteCommand command)
        {
            var isRemovedAll = true;
            foreach (var carrierId in command.CarrierIds)
            {
                var carrierToDelete = _carrierRepository.GetById(carrierId);
                
                var isRemoved = false;
                if (_invoiceRepository.GetByCarrier(carrierId).Count == 0)
                    isRemoved = _carrierRepository.Remove(carrierId) && _addressRepository.Remove(carrierToDelete.AddressId);

                isRemovedAll = isRemoved ? isRemovedAll : false;
            }

            return isRemovedAll;
        }

        public CarrierViewModel GetById(long id)
        {
            return _mapper.Map<CarrierViewModel>(_carrierRepository.GetById(id));
        }

        public IQueryable<Carrier> GetAll()
        {
            return _carrierRepository.GetAll();
        }

        public IQueryable<Carrier> GetAll(CarrierGetAllQuery query)
        {
            return _carrierRepository.GetAll(query.Size);

        }

        public bool Update(CarrierUpdateCommand command)
        {
            var entity = _mapper.Map<Carrier>(command);

            var carrierDb = _carrierRepository.GetById(entity.Id) ?? throw new NotFoundException();

            if (entity.Address != null)
            {
                carrierDb.Address.City = entity.Address.City;
                carrierDb.Address.Country = entity.Address.Country;
                carrierDb.Address.Neighborhood = entity.Address.Neighborhood;
                carrierDb.Address.Number = entity.Address.Number;
                carrierDb.Address.State = entity.Address.State;
                carrierDb.Address.StreetName = entity.Address.StreetName;
            }
            
            carrierDb.CompanyName = entity.CompanyName;
            carrierDb.Name = entity.Name;
            carrierDb.StateRegistration = entity.StateRegistration;
            carrierDb.PersonType = entity.PersonType;
            carrierDb.CPF = entity.CPF;
            carrierDb.CNPJ = entity.CNPJ;
            carrierDb.FreightResponsability = entity.FreightResponsability;

            carrierDb.Validate();

            return _carrierRepository.Update(carrierDb);
        }
        
    }
}
