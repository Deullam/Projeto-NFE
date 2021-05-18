using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TheSolutionBrothers.Nfe.API.Controllers.Products;
using TheSolutionBrothers.NFe.Application.Features.Products;
using TheSolutionBrothers.NFe.Application.Features.Products.Queries;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Controller.Tests.Initializer;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;

namespace TheSolutionBrothers.NFe.Controller.Tests.Features.Products
{
    [TestFixture]
    public class ProductControllerTests : TestControllerBase
    {
        private ProductsController _ProductController;
        private Mock<IProductService> _ProductServiceMock;
        private Product _Product;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _ProductServiceMock = new Mock<IProductService>();
            _ProductController = new ProductsController(_ProductServiceMock.Object)
            {
                Request = request
            };

            _Product = ObjectMother.GetExistentValidProduct(new TaxProduct()); ;
        }


        [Test]
        public void Test_ProductController_Get_ShouldBeOk()
        {
            var product = ObjectMother.GetExistentValidProduct(new TaxProduct());
            var response = new List<Product>() { product }.AsQueryable();
            _ProductServiceMock.Setup(s => s.GetAll()).Returns(response);
            var id = 1;
            var odataOptions = GetOdataQueryOptions<Product>(_ProductController);

            var callback = _ProductController.Get(odataOptions);

            _ProductServiceMock.Verify(s => s.GetAll(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ProductViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Test_ProductController_GetWithSize_ShouldBeOk()
        {
            var size = 1;
            long id = 1;
            var odataOptions = GetOdataQueryOptions<Product>(_ProductController);
            _ProductController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/Products?size=" + size);
            var response = new List<Product>() { _Product }.AsQueryable();
            _ProductServiceMock.Setup(s => s.GetAll(It.IsAny<ProductGetAllQuery>())).Returns(response);

            var callback = _ProductController.Get(odataOptions);

            _ProductServiceMock.Verify(s => s.GetAll(It.IsAny<ProductGetAllQuery>()), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ProductViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Test_ProductController_GetById_ShouldBeOk()
        {
            long id = 1;
            var productViewModel = ObjectMother.GetExistentValidProductViewModel();
            _ProductServiceMock.Setup(c => c.GetById(id)).Returns(productViewModel);

            IHttpActionResult callback = _ProductController.GetById(id);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ProductViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _ProductServiceMock.Verify(s => s.GetById(id));
        }

        [Test]
        public void Test_ProductController_Post_ShouldBeOk()
        {
            long expectedId = 1;
            var productCommand = ObjectMother.GetValidProductRegisterCommand();
            _ProductServiceMock.Setup(c => c.Add(productCommand)).Returns(expectedId);

            IHttpActionResult callback = _ProductController.Post(productCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
            _ProductServiceMock.Verify(s => s.Add(productCommand));
        }

        [Test]
        public void Test_ProductController_Post_ShouldThrowException()
        {
            var invalidProductCommand = ObjectMother.GetInvalidProductWithCodeLengthOverflowRegisterCommand();

            IHttpActionResult callback = _ProductController.Post(invalidProductCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _ProductServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ProductController_Put_ShouldBeOk()
        {
            var isUpdated = true;
            var productCommand = ObjectMother.GetExistentValidProductUpdateCommand();
            _ProductServiceMock.Setup(c => c.Update(productCommand)).Returns(isUpdated);

            IHttpActionResult callback = _ProductController.Put(productCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _ProductServiceMock.Verify(s => s.Update(productCommand), Times.Once);
        }

        [Test]
        public void Test_ProductController_Put_ShouldThrowException()
        {
            var invalidProductCommand = ObjectMother.GetInvalidProductWithCurrentValueEqualThanZeroUpdateCommand();

            IHttpActionResult callback = _ProductController.Put(invalidProductCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _ProductServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ProductController_Delete_ShouldBeOk()
        {
            var ProductCommand = ObjectMother.GetExistentValidProductWithoutDependencyDeleteCommand();
            var isUpdated = true;
            _ProductServiceMock.Setup(c => c.Remove(ProductCommand)).Returns(isUpdated);

            IHttpActionResult callback = _ProductController.Delete(ProductCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _ProductServiceMock.Verify(s => s.Remove(ProductCommand), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_AccountController_Delete_ShouldThrowException()
        {
            var invalidProductCommand = ObjectMother.GetExistentValidProductWithoutIdDeleteCommand();

            IHttpActionResult callback = _ProductController.Delete(invalidProductCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _ProductServiceMock.VerifyNoOtherCalls();
        }
    }
}
