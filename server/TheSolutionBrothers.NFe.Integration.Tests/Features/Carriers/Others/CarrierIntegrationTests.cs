using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Application.Features.Carriers;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.Addresses;
using TheSolutionBrothers.NFe.Infra.Data.Features.Carriers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;
using System.Web.Http.Results;
using System.Net;
using TheSolutionBrothers.Nfe.API.Exceptions;
using FluentValidation.Results;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using Microsoft.AspNet.OData;
using System.Net.Http;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Carriers
{
    [TestFixture]
    public partial class CarrierIntegrationTests 
    {
        [Test]
        public void Test_CarrierIntegration_Delete_ShouldBeOk()
        {
            CarrierDeleteCommand carrierCommand = new CarrierDeleteCommand() {  CarrierIds = new long[] {2 } };

            var result = _controller.Delete(carrierCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_CarrierIntegration_Delete_InvalidId_ShouldThrowException()
        {
            var expectedMessage = "Registro não encontrado";
            CarrierDeleteCommand carrierCommand = ObjectMother.GetCarrierDeleteCommandWithNonExistentIds();

            var result = _controller.Delete(carrierCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public void Test_CarrierIntegration_Delete_InvalidCommand_ShouldThrowException()
        {
            CarrierDeleteCommand invalidCarrierCommand = ObjectMother.GetCarrierDeleteCommandWithoutId();

            var result = _controller.Delete(invalidCarrierCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }

        [Test]
        public void Test_CarrierIntegration_GetById_ShouldBeOk()
        {
            long id = 1;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<CarrierViewModel>>().Subject;

            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
        }

        [Test]
        public void Test_CarrierIntegration_Get_IdInvalid_ShouldThrowException()
        {
            long id = -1;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<CarrierViewModel>>().Subject;

            httpResponse.Content.Should().BeNull();
        }

        [Test]
        public void Test_CarrierIntegration_Get_NonexistentId_ShouldBeOk()
        {
            long id = 99999;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<CarrierViewModel>>().Subject;

            httpResponse.Content.Should().BeNull();
        }

        [Test]
        public void Test_CarrierIntegration_GetAll_ShouldBeOk()
        {
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Carrier>(_controller);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<CarrierViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Test_CarrierIntegration_GetWithSize_ShouldBeOk()
        {
            var expectedCount = 1;
            var size = 1;
            long expectedFirstId = 1;
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Carrier>(_controller);
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/carriers?size=" + size);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<CarrierViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(expectedFirstId);
            httpResponse.Content.Should().HaveCount(expectedCount);
        }

    }
}
