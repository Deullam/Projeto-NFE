using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;
using System.Web.Http.Results;
using FluentValidation.Results;
using System.Net;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Carriers
{
    [TestFixture]
    public partial class CarrierIntegrationTests
    {
        [Test]
        public void Test_CarrierIntegration_UpdatePhysical_ShouldBeOk()
        {
            CarrierUpdateCommand carrierCommand = ObjectMother.GetExistentValidPhysicalCarrierUpdateCommand(_addressCommand, _cpf);

            var result = _controller.Put(carrierCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_CarrierIntegration_UpdateLegal_ShouldBeOk()
        {
            CarrierUpdateCommand carrierCommand = ObjectMother.GetExistentValidLegalCarrierUpdateCommand(_addressCommand, _cnpj);

            var result = _controller.Put(carrierCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_CarrierIntegration_Update_UninformedName_ShouldThrowException()
        {
            CarrierUpdateCommand carrierCommand = ObjectMother.GetinvalidPhysicalCarrierUpdateCommandWithUninformedName(_addressCommand, _cpf);

            var result = _controller.Put(carrierCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }
    }
}
