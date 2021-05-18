using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Application.Features.Senders;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.Data.Features.Addresses;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.Senders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Senders.Commands;
using System.Web.Http.Results;
using TheSolutionBrothers.Nfe.API.Exceptions;
using System.Net;
using FluentValidation.Results;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using Microsoft.AspNet.OData;
using System.Net.Http;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Senders
{
    [TestFixture]
    public partial class SenderIntegrationTests 
    {
        [Test]
        public void Test_SenderIntegration_Delete_ShouldBeOk()
        {
            SenderDeleteCommand senderCommand = new SenderDeleteCommand() { SenderIds = new long []{ 2 } };

            var result = _controller.Delete(senderCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_SenderIntegration_Delete_InvalidId_ShouldThrowException()
        {
            var expectedMessage = "Registro não encontrado";
            SenderDeleteCommand senderCommand = ObjectMother.GetSenderDeleteCommandWithNonExistentIds();

            var result = _controller.Delete(senderCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public void Test_SenderIntegration_Delete_InvalidCommand_ShouldThrowException()
        {
            SenderDeleteCommand invalidSenderCommand = ObjectMother.GetsenderDeleteCommandWithoutId();

            var result = _controller.Delete(invalidSenderCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }

        [Test]
        public void Test_SenderIntegration_GetById_ShouldBeOk()
        {
            long id = 1;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<SenderViewModel>>().Subject;

            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
        }

        [Test]
        public void Test_SenderIntegration_Get_IdInvalid_ShouldThrowException()
        {
            long id = -1;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<SenderViewModel>>().Subject;

            httpResponse.Content.Should().BeNull();
        }

        [Test]
        public void Test_SenderIntegration_Get_NonexistentId_ShouldBeOk()
        {
            long id = 99999;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<SenderViewModel>>().Subject;

            httpResponse.Content.Should().BeNull();
        }

        [Test]
        public void Test_SenderIntegration_GetAll_ShouldBeOk()
        {
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Sender>(_controller);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<SenderViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Test_SenderIntegration_GetWithSize_ShouldBeOk()
        {
            var expectedCount = 1;
            var size = 1;
            long expectedFirstId = 1;
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Sender>(_controller);
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/senders?size=" + size);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<SenderViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(expectedFirstId);
            httpResponse.Content.Should().HaveCount(expectedCount);
        }

    }
}
