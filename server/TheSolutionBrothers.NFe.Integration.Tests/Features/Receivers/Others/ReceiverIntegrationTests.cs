using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using System.Web.Http.Results;
using TheSolutionBrothers.Nfe.API.Exceptions;
using FluentValidation.Results;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
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

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Receivers
{
	[TestFixture]
	public partial class ReceiverIntegrationTests
	{

        [Test]
        public void Test_ReceiverIntegration_Delete_ShouldBeOk()
        {
            ReceiverDeleteCommand receiverCommand = new ReceiverDeleteCommand() { ReceiverIds = new long[] { 2 } };


            IHttpActionResult result = _controller.Delete(receiverCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_ReceiverIntegration_Delete_InvalidId_ShouldThrowException()
        {
            var expectedMessage = "Registro não encontrado";
            ReceiverDeleteCommand receiverCommand = ObjectMother.GetReceiverDeleteCommandWithNonExistentIds();

            var result = _controller.Delete(receiverCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public void Test_ReceiverIntegration_Delete_InvalidCommand_ShouldThrowException()
        {
            ReceiverDeleteCommand invalidReceiverCommand = ObjectMother.GetReceiverDeleteCommandWithoutId();

            var result = _controller.Delete(invalidReceiverCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }

        [Test]
        public void Test_ReceiverIntegration_GetById_ShouldBeOk()
        {
            long id = 1;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<ReceiverViewModel>>().Subject;

            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
        }

        [Test]
        public void Test_ReceiverIntegration_Get_IdInvalid_ShouldThrowException()
        {
            long id = -1;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<ReceiverViewModel>>().Subject;

            httpResponse.Content.Should().BeNull();
        }

        [Test]
        public void Test_ReceiverIntegration_Get_NonexistentId_ShouldBeOk()
        {
            long id = 99999;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<ReceiverViewModel>>().Subject;

            httpResponse.Content.Should().BeNull();
        }

        [Test]
        public void Test_ReceiverIntegration_GetAll_ShouldBeOk()
        {
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Receiver>(_controller);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ReceiverViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Test_ReceiverIntegration_GetWithSize_ShouldBeOk()
        {
            var expectedCount = 1;
            var size = 1;
            long expectedFirstId = 1;
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Receiver>(_controller);
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/receivers?size=" + size);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ReceiverViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(expectedFirstId);
            httpResponse.Content.Should().HaveCount(expectedCount);
        }

    }
}
