using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using System.Linq;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Queries;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Application.Features.Receivers
{
	public class ReceiverService : IReceiverService
	{
		private IReceiverRepository _receiverRepository;
		private IAddressRepository _addressRepository;
        private IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public ReceiverService(IReceiverRepository receiverRepository, IAddressRepository addressRepository, IInvoiceRepository invoiceRepository, IMapper mapper)
		{
			_receiverRepository = receiverRepository;
			_addressRepository = addressRepository;
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;

        }

        public long Add(ReceiverRegisterCommand command)
		{
            var entity = _mapper.Map<Receiver>(command);

            entity.Validate();

            entity.Address = _addressRepository.Save(entity.Address);
            entity = _receiverRepository.Add(entity);
            return entity.Id;
		}


		public bool Remove(ReceiverDeleteCommand receiver)
		{
            var isRemovedAll = true;
            foreach (var receiverId in receiver.ReceiverIds)
            {
                var receiverToDelete = _receiverRepository.GetById(receiverId);
                var isRemoved = false;
                if (_invoiceRepository.GetByReceiver(receiverId).Count == 0)
                    isRemoved = _receiverRepository.Remove(receiverId) && _addressRepository.Remove(receiverToDelete.AddressId);
                isRemovedAll = isRemoved ? isRemovedAll : false;
            }
            return isRemovedAll;
		}

		public bool Update(ReceiverUpdateCommand command)
		{

            var entity = _mapper.Map<Receiver>(command);

            var receiverDb = _receiverRepository.GetById(entity.Id) ?? throw new NotFoundException();

            receiverDb.Address.City = entity.Address.City;
            receiverDb.Address.Country = entity.Address.Country;
            receiverDb.Address.Neighborhood = entity.Address.Neighborhood;
            receiverDb.Address.Number = entity.Address.Number;
            receiverDb.Address.State = entity.Address.State;
            receiverDb.Address.StreetName = entity.Address.StreetName;


            receiverDb.CompanyName = entity.CompanyName;
            receiverDb.Name = entity.Name;
            receiverDb.StateRegistration = entity.StateRegistration;
            receiverDb.Type = entity.Type;
            receiverDb.Cpf = entity.Cpf;
            receiverDb.Cnpj = entity.Cnpj;

            receiverDb.Validate();
            
			return _receiverRepository.Update(receiverDb);
		}

		public ReceiverViewModel GetById(long id)
		{
            return _mapper.Map<ReceiverViewModel>(_receiverRepository.GetById(id));
		}

        public IQueryable<Receiver> GetAll()
        {
            return _receiverRepository.GetAll();
        }

        public IQueryable<Receiver> GetAll(ReceiverGetAllQuery query)
        {
            return _receiverRepository.GetAll(query.Size);
        }
		
    }
}
