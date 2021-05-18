using FluentAssertions;
using Moq;
using TheSolutionBrothers.NFe.Application.Features.Products;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Products.Commands;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Products.Queries;

namespace TheSolutionBrothers.NFe.Application.Tests.Features.Products
{
    [TestFixture]
    public class ProductServiceTests
    {

        private Mock<IProductRepository> _mockProductRepository;
        private Mock<IInvoiceItemRepository> _mockInvoiceItemRepository;
        private ProductService _productService;
        private Mock<IMapper> _mockIMapper;
        private ProductRegisterCommand productRegisterCommand;

        [SetUp]
        public void Initialize()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockInvoiceItemRepository = new Mock<IInvoiceItemRepository>();
            _mockIMapper = new Mock<IMapper>();
            _productService = new ProductService(_mockProductRepository.Object, _mockIMapper.Object);
            productRegisterCommand = ObjectMother.GetValidProductRegisterCommand();
        }

        [Test]
        public void Test_ProductService_Add_ShouldBeOk()
        {          

            Product product = ObjectMother.GetNewValidProduct(new TaxProduct());

            _mockIMapper.Setup(m => m.Map<Product>(productRegisterCommand)).Returns(product);

            _mockProductRepository.Setup(m => m.Add(product)).Returns(new Product() { Id = 1 });

            long result = _productService.Add(productRegisterCommand);

            result.Should().Be(1);

            _mockProductRepository.Verify(rp => rp.Add(product));
        }

        [Test]
        public void Test_ProductService_Add_InvalidProduct_ShouldThrowException()
        {
            ProductRegisterCommand invalidProductCommand = ObjectMother.GetInvalidProductWithCodeLengthOverflowRegisterCommand();
            Product invalidProduct = ObjectMother.GetInvalidProductWithCodeLengthOverflow(new TaxProduct());

            _mockIMapper.Setup(m => m.Map<Product>(invalidProductCommand)).Returns(invalidProduct);

            Action action = () => { _productService.Add(invalidProductCommand); };

            action.Should().Throw<BusinessException>();

            _mockProductRepository.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ProductService_Update_ShouldBeOk()
        {
            var isUpdated = true;

            ProductUpdateCommand productCommand = ObjectMother.GetExistentValidProductUpdateCommand();

            Product product = ObjectMother.GetExistentValidProduct(new TaxProduct());

            _mockIMapper.Setup(m => m.Map<Product>(productCommand)).Returns(product);

            _mockProductRepository.Setup(m => m.Update(product)).Returns(isUpdated);
            _mockProductRepository.Setup(m => m.GetById(product.Id)).Returns(product);

            bool result = _productService.Update(productCommand);


            result.Should().BeTrue();
            _mockProductRepository.Verify(rp => rp.Update(product));
            _mockProductRepository.Verify(rp => rp.GetById(product.Id));
        }

        [Test]
        public void Test_ProductService_Update_InvalidProduct_ShouldThrowException()
        {
            ProductUpdateCommand invalidProductCommand = ObjectMother.GetInvalidProductWithCurrentValueEqualThanZeroUpdateCommand();
            Product product = ObjectMother.GetInvalidProductWithCurrentValueEqualThanZero(new TaxProduct());

            _mockIMapper.Setup(m => m.Map<Product>(invalidProductCommand)).Returns(product);

            product.Id = 1;

            Action action = () => { _productService.Update(invalidProductCommand);};

            action.Should().Throw<BusinessException>();
        }
        [Test]

        public void Test_ProductService_Delete_False_ShouldBeOk()
        {
            var deleteCommand = ObjectMother.GetExistentValidProductWithoutDependencyDeleteCommand();

            var productSaved = ObjectMother.GetExistentValidProduct(new TaxProduct());

            _mockProductRepository.Setup(r => r.GetById(It.IsAny<long>())).Returns(productSaved);
            _mockProductRepository.Setup(m => m.Remove(productSaved.Id));

            var result = _productService.Remove(deleteCommand);

            result.Should().BeFalse();
            _mockProductRepository.Verify(rp => rp.Remove(productSaved.Id));
        }

        [Test]
        public void Test_ProductService_Delete_ShouldBeOk()
        {
            ProductDeleteCommand product = ObjectMother.GetExistentValidProductWithoutDependencyDeleteCommand();
            long productToDelete = 1;
            var isAllUpdated = true;
            _mockProductRepository.Setup(m => m.Remove(productToDelete)).Returns(isAllUpdated);
            _mockInvoiceItemRepository.Setup(invoiceItem => invoiceItem.GetByProduct(productToDelete)).Returns(new List<InvoiceItem>());


            var result = _productService.Remove(product);
            result.Should().BeTrue();
            _mockProductRepository.Verify(rp => rp.Remove(productToDelete));
        }

        //[Test]
        //public void Test_ProductService_Delete_ProductRelatedToInvoiceItem_ShouldThrowException()
        //{
        //    Product product = ObjectMother.GetExistentValidProduct(new TaxProduct());
        //    _mockInvoiceItemRepository.Setup(invoiceItem => invoiceItem.GetByProduct(product.Id)).Returns(new List<InvoiceItem> { new InvoiceItem() });

        //    Action action = () => { _productService.Delete(product); };
        //    action.Should().Throw<ProductDeleteWithDependencesException>();
        //    _mockProductRepository.VerifyNoOtherCalls();
        //}

        [Test]
        public void Test_ProductService_Get_ShouldBeOk()
        {
            long existentId = 1;

            _mockProductRepository.Setup(m => m.GetById(existentId)).Returns(ObjectMother.GetExistentValidProduct(new TaxProduct()));
            _mockIMapper.Setup(m => m.Map<ProductViewModel>(It.IsAny<Product>())).Returns(ObjectMother.GetExistentValidProductViewModel());

            ProductViewModel result = _productService.GetById(existentId);

            result.Id.Should().Be(existentId);
            _mockProductRepository.Verify(rp => rp.GetById(existentId));
        }

        [Test]
        public void Test_ProductService_Get_NonexistentId_ShouldBeOk()
        {
            long unexistentId = 100;

            _mockProductRepository.Setup(m => m.GetById(unexistentId)).Returns((Product)null);

            ProductViewModel result = _productService.GetById(unexistentId);

            result.Should().BeNull();
            _mockProductRepository.Verify(rp => rp.GetById(unexistentId));
        }

        [Test]
        public void Test_ProductService_GetAll_ShouldBeOk()
        {
            var repositoryMockValue = new List<Product>() { ObjectMother.GetExistentValidProduct(new TaxProduct()) }.AsQueryable();

            _mockProductRepository.Setup(m => m.GetAll()).Returns(repositoryMockValue);

            IEnumerable<Product> result = _productService.GetAll();

            result.Should().BeEquivalentTo(repositoryMockValue);
            _mockProductRepository.Verify(rp => rp.GetAll());
        }
        

        [Test]
        public void Test_ProductService_GetAll_Query_ShouldBeOk()
        {
            int amount = 1;

            ProductGetAllQuery query = ObjectMother.GetValidProductGetAllQuery();
            var repositoryMockValue = new List<Product>() { ObjectMother.GetExistentValidProduct(new TaxProduct()) }.AsQueryable();
            _mockProductRepository.Setup(m => m.GetAll(query.Size)).Returns(repositoryMockValue);

            var products = _productService.GetAll(query);

            _mockProductRepository.Verify(x => x.GetAll(query.Size));
            products.Should().NotBeNull();
            products.Should().HaveCount(amount);
            _mockProductRepository.VerifyNoOtherCalls();
        }

    }
}
