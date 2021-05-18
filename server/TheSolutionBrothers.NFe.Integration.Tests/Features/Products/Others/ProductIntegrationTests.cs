using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Application.Features.Products.Commands;
using System.Web.Http.Results;
using TheSolutionBrothers.Nfe.API.Exceptions;
using FluentValidation.Results;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;
using Microsoft.AspNet.OData;
using System.Web.Http;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Routing;
using System.Net;
using System.Net.Http;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using System.Linq;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Products
{
	[TestFixture]
	public partial class ProductIntegrationTests
    {

        [Test]
        public void Test_ProductIntegration_Delete_ShouldBeOk()
        {
            ProductDeleteCommand ProductCommand = ObjectMother.GetExistentValidProductWithoutDependencyDeleteCommand();

            IHttpActionResult result = _controller.Delete(ProductCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_ProductIntegration_Delete_InvalidCommand_ShouldThrowException()
        {
            ProductDeleteCommand invalidProductCommand = ObjectMother.GetExistentValidProductWithoutIdDeleteCommand();

            var result = _controller.Delete(invalidProductCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }

        [Test]
        public void Test_ProductIntegration_GetById_ShouldBeOk()
        {
            long id = 1;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<ProductViewModel>>().Subject;

            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
        }

        [Test]
        public void Test_ProductIntegration_Get_NonexistentId_ShouldBeOk()
        {
            long id = 99999;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<ProductViewModel>>().Subject;

            httpResponse.Content.Should().BeNull();
        }

        [Test]
        public void Test_ProductIntegration_GetAll_ShouldBeOk()
        {
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Product>(_controller);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ProductViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Test_ProductIntegration_GetWithSize_ShouldBeOk()
        {
            var expectedCount = 1;
            var size = 1;
            long expectedFirstId = 1;
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Product>(_controller);
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/Products?size=" + size);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ProductViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(expectedFirstId);
            httpResponse.Content.Should().HaveCount(expectedCount);
        }

    }
}
