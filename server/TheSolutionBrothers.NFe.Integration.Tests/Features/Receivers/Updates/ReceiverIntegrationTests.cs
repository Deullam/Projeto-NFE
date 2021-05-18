using System;
using NUnit.Framework;
using FluentAssertions;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using System.Web.Http.Results;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Net;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Receivers
{
	[TestFixture]
	public partial class ReceiverIntegrationTests
	{

        [Test]
        public void Test_ReceiverIntegration_UpdatePhysical_ShouldBeOk()
        {
            ReceiverUpdateCommand receiverCommand = ObjectMother.GetExistentValidPhysicalReceiverUpdateCommand(_addressCommand, _cpf);

            var  result = _controller.Put(receiverCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_ReceiverIntegration_UpdateLegal_ShouldBeOk()
        {
            ReceiverUpdateCommand receiverCommand = ObjectMother.GetExistentValidLegalReceiverUpdateCommand(_addressCommand, _cnpj);

            var result = _controller.Put(receiverCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_ReceiverIntegration_Update_UninformedName_ShouldThrowException()
        {
            ReceiverUpdateCommand receiverCommand = ObjectMother.GetLegalReceiverUpdateCommandWithUninformedName(_addressCommand, _cnpj);

            var result = _controller.Put(receiverCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }
    }
}
