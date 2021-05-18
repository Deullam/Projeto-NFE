using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Products.Commands;
using TheSolutionBrothers.NFe.Application.Features.Products.Queries;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceItems;

namespace TheSolutionBrothers.NFe.Application.Features.Products
{
    public class ProductService : IProductService
    {

        private IProductRepository _productRepository;
        //private IInvoiceItemRepository _invoiceItemRepository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _productRepository = repository;
            //_invoiceItemRepository = invoiceItemRepository;

            _mapper = mapper;
        }

        public long Add(ProductRegisterCommand command)
        {
            var entity = _mapper.Map<Product>(command);

            entity.Validate();

            return _productRepository.Add(entity).Id;
        }

        public bool Remove(ProductDeleteCommand product)
        {
            var isRemovedAll = true;

            foreach (var productId in product.ProductIds)
            {
                var receiverToDelete = _productRepository.GetById(productId);
                var isRemoved = _productRepository.Remove(productId);
                isRemovedAll = isRemoved ? isRemovedAll : false;
            }

            return isRemovedAll;
        }

        public ProductViewModel GetById(long id)
        {
            return _mapper.Map<ProductViewModel>(_productRepository.GetById(id));
        }

        public IQueryable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IQueryable<Product> GetAll(ProductGetAllQuery query)
        {
            return _productRepository.GetAll(query.Size);
        }

        public bool Update(ProductUpdateCommand command)
        {
            var entity = _mapper.Map<Product>(command);

            var productDb = _productRepository.GetById(entity.Id) ?? throw new NotFoundException();

            productDb.Code = entity.Code;
            productDb.CurrentValue = entity.CurrentValue;
            productDb.Description = entity.Description;

            productDb.Validate();

            return _productRepository.Update(productDb);
        }

    }
}
