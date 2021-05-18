using System;
using System.Linq;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Commands;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Queries;
using TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels;
using AutoMapper;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using System.Collections.Generic;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices
{
    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository _invoiceRepository;
        private ICarrierRepository _carrierRepository;
        private IReceiverRepository _receiverRepository;
        private ISenderRepository _senderRepository;
        private IProductRepository _productRepository;
        private IInvoiceItemRepository _invoiceItemRepository;
        private readonly IMapper _mapper;

        public InvoiceService(
            IInvoiceRepository invoiceRepository, 
            ICarrierRepository carrierRepository, 
            IReceiverRepository receiverRepository, 
            ISenderRepository senderRepository, 
            IProductRepository productRepository, 
            IInvoiceItemRepository invoiceItemRepository, 
            IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _carrierRepository = carrierRepository;
            _receiverRepository = receiverRepository;
            _senderRepository = senderRepository;
            _productRepository = productRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _mapper = mapper;

        }

        public long Add(InvoiceRegisterCommand command)
        {
            if (command.Status.Equals(InvoiceStatus.ISSUED))
                throw new InvoiceSaveIssuedInvoiceException();
            var entity = _mapper.Map<Invoice>(command);
            entity.EntryDate = DateTime.Now;

            Invoice invoice = _invoiceRepository.GetByNumber(entity.Number);
            if (invoice != null)
                throw new InvoiceExistentNumberException();

            entity.Carrier = _carrierRepository.GetById(command.CarrierId) ?? throw new NotFoundException();
            entity.Receiver = _receiverRepository.GetById(command.ReceiverId) ?? throw new NotFoundException();
            entity.Sender = _senderRepository.GetById(command.SenderId) ?? throw new NotFoundException();

            for (int i = 0; i < entity.InvoiceItems.Count; i++)
            {
                entity.InvoiceItems[i].Product = _productRepository.GetById(entity.InvoiceItems[i].ProductId) ?? throw new NotFoundException();
                entity.InvoiceItems[i].Invoice = entity;
            }

            entity.Validate();

            entity = _invoiceRepository.Add(entity);
            return entity.Id;
        }

        public InvoiceViewModel GetById(long id)
        {
			Invoice invoice = _invoiceRepository.GetById(id);

			InvoiceViewModel viewModel = _mapper.Map<InvoiceViewModel>(invoice);

			return viewModel;
        }

        public IQueryable<Invoice> GetAll()
        {
            return _invoiceRepository.GetAll();
        }

        public IQueryable<Invoice> GetAll(InvoiceGetAllQuery query)
        {
            return _invoiceRepository.GetAll(query.Size);
        }

        public bool Remove(InvoiceDeleteCommand entity)
        {
            var isRemovedAll = true;
            foreach (var invoiceId in entity.InvoiceIds)
            {
                var invoiceToDelete = _invoiceRepository.GetById(invoiceId) ?? throw new NotFoundException();
                if (invoiceToDelete.Status.Equals(InvoiceStatus.ISSUED))
                    throw new InvoiceDeleteIssuedInvoiceException();
                var isRemoved = _invoiceRepository.Remove(invoiceId);
                isRemovedAll = isRemoved ? isRemovedAll : false;
            }
            return isRemovedAll;
        }

        public bool Update(InvoiceUpdateCommand command)
        {
            if (command.Status.Equals(InvoiceStatus.ISSUED))
                throw new InvoiceUpdateIssuedInvoiceException();

            var entity = _mapper.Map<Invoice>(command);
            
            Invoice invoice = _invoiceRepository.GetByNumber(entity.Number);
            if (invoice != null && !invoice.Id.Equals(entity.Id))
                throw new InvoiceExistentNumberException();

            var invoiceDb = _invoiceRepository.GetById(entity.Id) ?? throw new NotFoundException();

            invoiceDb.NatureOperation = entity.NatureOperation;
            invoiceDb.Number = entity.Number;
            invoiceDb.Status = entity.Status;
            invoiceDb.Carrier = _carrierRepository.GetById(command.CarrierId) ?? throw new NotFoundException();
            invoiceDb.Receiver = _receiverRepository.GetById(command.ReceiverId) ?? throw new NotFoundException();
            invoiceDb.Sender = _senderRepository.GetById(command.SenderId) ?? throw new NotFoundException();

            invoiceDb.Validate();

            return _invoiceRepository.Update(invoiceDb);
        }

        public void ExportToPDF(Invoice entity, string path)
        {
            throw new NotImplementedException();
        }

        public void ExportToXML(Invoice entity, string path)
        {
            throw new NotImplementedException();
        }

        public Invoice Issue(Invoice invoice, double freightValue)
        {
            throw new NotImplementedException();
        }

        public List<long> AddItems(long id, List<InvoiceItemRegisterCommand> commands)
        {
            List<long> ids = new List<long>();
            Invoice invoice = _invoiceRepository.GetById(id) ?? throw new NotFoundException();

            if (invoice.Status.Equals(InvoiceStatus.ISSUED))
                throw new InvoiceSaveIssuedInvoiceException();

            foreach (InvoiceItemRegisterCommand command in commands)
            {
                var entity = _mapper.Map<InvoiceItem>(command);
                entity.Invoice = invoice;
                entity.Product = _productRepository.GetById(entity.ProductId) ?? throw new NotFoundException();

                entity.Validate();

                entity = _invoiceItemRepository.Add(entity);
                ids.Add(entity.Id);
            }

            return ids;
        }

        public bool UpdateItems(long id, List<InvoiceItemUpdateCommand> commands)
        {
            bool success = true;
            Invoice invoice = _invoiceRepository.GetById(id) ?? throw new NotFoundException();

            if (invoice.Status.Equals(InvoiceStatus.ISSUED))
                throw new InvoiceSaveIssuedInvoiceException();

            foreach (InvoiceItemUpdateCommand command in commands)
            {
                var entity = _mapper.Map<InvoiceItem>(command);
                entity.Product = _productRepository.GetById(entity.ProductId) ?? throw new NotFoundException();

                var entityDb = _invoiceItemRepository.GetById(entity.Id) ?? throw new NotFoundException();
                
                entityDb.Invoice = invoice;
                entityDb.Product = entity.Product;
                entityDb.Amount = entity.Amount;

                entityDb.Validate();

                success = success && _invoiceItemRepository.Update(entityDb);
            }

            return success;
        }

        public bool DeleteItems(long id, InvoiceItemDeleteCommand command)
        {
            Invoice invoice = _invoiceRepository.GetById(id) ?? throw new NotFoundException();

            if (invoice.Status.Equals(InvoiceStatus.ISSUED))
                throw new InvoiceSaveIssuedInvoiceException();

            bool success = true;
            foreach (long idItem in command.InvoiceItemIds)
            {
                success = success && _invoiceItemRepository.Remove(idItem);
            }

            return success;
        }
    }
}
