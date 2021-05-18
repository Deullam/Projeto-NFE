using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Senders.Commands;
using System.Web.Http.Results;
using System.Net;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Senders
{
    [TestFixture]
    public partial class SenderIntegrationTests
    {
        [Test]
        public void Test_SenderIntegration_Update_ShouldBeOk()
        {
            SenderUpdateCommand senderCommand = ObjectMother.GetExistentValidSenderCommandToUpdate(_addressCommand, _cnpj);

            var result = _controller.Put(senderCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_SenderIntegration_Update_UninformedCompanyName_ShouldThrowException()
        {
            SenderUpdateCommand senderCommand = ObjectMother.GetNewInvalidSenderUpdateCommandWithUninformedCompanyName(_addressCommand, _cnpj);

            var result = _controller.Put(senderCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }
    }
}
