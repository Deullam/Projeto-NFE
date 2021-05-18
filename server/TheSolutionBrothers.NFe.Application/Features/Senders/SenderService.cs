using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Application.Features.Senders.Commands;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Senders.Queries;
using AutoMapper;
using System.Linq;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Application.Features.Senders
{
	public class SenderService : ISenderService
	{
		private ISenderRepository _senderRepository;
		private IAddressRepository _addressRepository;
        private IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

		public SenderService(ISenderRepository senderRepository, IAddressRepository addressRepository, IInvoiceRepository invoiceRepository, IMapper mapper)
		{
			_senderRepository = senderRepository;
			_addressRepository = addressRepository;
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
		}

		public long Add(SenderRegisterCommand command)
		{
			var entity = _mapper.Map<Sender>(command);

			entity.Validate();

			entity.Address = _addressRepository.Save(entity.Address);
			entity = _senderRepository.Add(entity);
			return entity.Id;
		}


		public bool Remove(SenderDeleteCommand sender)
		{
			var isRemovedAll = true;
			foreach (var senderId in sender.SenderIds)
			{
				var senderToDelete = _senderRepository.GetById(senderId);
                var isRemoved = false;
                if (_invoiceRepository.GetBySender(senderId).Count == 0)
                    isRemoved = _senderRepository.Remove(senderId) && _addressRepository.Remove(senderToDelete.AddressId);
				isRemovedAll = isRemoved ? isRemovedAll : false;
			}
			return isRemovedAll;
		}

		public bool Update(SenderUpdateCommand command)
		{
			var entity = _mapper.Map<Sender>(command);

			var senderDb = _senderRepository.GetById(entity.Id) ?? throw new NotFoundException();

			senderDb.Address.City = entity.Address.City;
			senderDb.Address.Country = entity.Address.Country;
			senderDb.Address.Neighborhood = entity.Address.Neighborhood;
			senderDb.Address.Number = entity.Address.Number;
			senderDb.Address.State = entity.Address.State;
			senderDb.Address.StreetName = entity.Address.StreetName;

			senderDb.FancyName = entity.FancyName;
			senderDb.CompanyName = entity.CompanyName;
			senderDb.Cnpj = entity.Cnpj;
			senderDb.StateRegistration = entity.StateRegistration;
			senderDb.MunicipalRegistration = entity.MunicipalRegistration;


			senderDb.Validate();

			return _senderRepository.Update(senderDb);
		}

		public SenderViewModel GetById(long id)
		{
			return _mapper.Map<SenderViewModel>(_senderRepository.GetById(id));
		}

		public IQueryable<Sender> GetAll()
		{
			return _senderRepository.GetAll();
		}

		public IQueryable<Sender> GetAll(SenderGetAllQuery query)
		{
			return _senderRepository.GetAll(query.Size);

		}


	}
}

