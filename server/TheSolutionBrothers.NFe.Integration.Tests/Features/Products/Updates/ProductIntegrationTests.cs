using System;
using NUnit.Framework;
using FluentAssertions;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Application.Features.Products.Commands;
using System.Web.Http.Results;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Net;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Products
{
	[TestFixture]
	public partial class ProductIntegrationTests
	{

        [Test]
        public void Test_ProductIntegration_Update_ShouldBeOk()
        {
            ProductUpdateCommand ProductCommand = ObjectMother.GetExistentValidProductUpdateCommand();

            var  result = _controller.Put(ProductCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_ProductIntegration_Update_InvalidProduct_ShouldThrowException()
        {
            ProductUpdateCommand ProductCommand = ObjectMother.GetInvalidProductWithCurrentValueEqualThanZeroUpdateCommand();

            var result = _controller.Put(ProductCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }
    }
}
