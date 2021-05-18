using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNet.OData;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TheSolutionBrothers.Nfe.API.Controllers.Carriers;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Application.Features.Carriers;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Queries;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Controller.Tests.Initializer;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;

namespace TheSolutionBrothers.NFe.Controller.Tests.Features.Carriers
{
    [TestFixture]
    public class CarrierControllerTests : TestControllerBase
    {
        private CarriersController _carrierController;
        private Mock<ICarrierService> _carrierServiceMock;
        private Mock<Carrier> _carrier;
        private CPF _cpf;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _carrierServiceMock = new Mock<ICarrierService>();
            _carrierController = new CarriersController(_carrierServiceMock.Object)
            {
                Request = request
            };

            _carrier = new Mock<Carrier>();
            _cpf = ObjectMother.GetValidCPF();
        }


        [Test]
        public void Test_CarrierController_Get_ShouldBeOk()
        {
            var address = ObjectMother.GetExistentValidAddress();
            var carrier = ObjectMother.GetExistentValidCarrierPhysical(address, _cpf);
            var response = new List<Carrier>() { carrier }.AsQueryable();
            _carrierServiceMock.Setup(s => s.GetAll()).Returns(response);
            var id = 1;
            var odataOptions = GetOdataQueryOptions<Carrier>(_carrierController);

            var callback = _carrierController.Get(odataOptions);

            _carrierServiceMock.Verify(s => s.GetAll(), Times.Once);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<CarrierViewModel>>>().Subject;

            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Test_CarrierController_GetWithSize_ShouldBeOk()
        {
            var size = 1;
            long id = 1;
            var address = ObjectMother.GetExistentValidAddress();
            var carrier = ObjectMother.GetExistentValidCarrierPhysical(address, _cpf);
            var odataOptions = GetOdataQueryOptions<Carrier>(_carrierController);
            _carrierController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/cariers?size=" + size);
            var response = new List<Carrier>() { carrier }.AsQueryable();
            _carrierServiceMock.Setup(s => s.GetAll(It.IsAny<CarrierGetAllQuery>())).Returns(response);

            var callback = _carrierController.Get(odataOptions);

            _carrierServiceMock.Verify(s => s.GetAll(It.IsAny<CarrierGetAllQuery>()), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<CarrierViewModel>>>().Subject;

            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Test_CarrierController_GetById_ShouldBeOk()
        {
            long id = 1;
            var addressViewModel = ObjectMother.GetAddessViewModel();
            var carrierViewModel = ObjectMother.GetPhysicalCarrierViewModel(addressViewModel, _cpf);
            _carrierServiceMock.Setup(c => c.GetById(id)).Returns(carrierViewModel);

            IHttpActionResult callback = _carrierController.GetById(id);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<CarrierViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _carrierServiceMock.Verify(s => s.GetById(id));
        }

        [Test]
        public void Test_CarrierController_Post_ShouldBeOk()
        {
            long expectedId = 1;
            var newAddressCommand = ObjectMother.GetValidAddresCommand();
            var carrierCommand = ObjectMother.GetValidPhysicalCarrierRegisterCommand(newAddressCommand, _cpf);

            _carrierServiceMock.Setup(c => c.Add(carrierCommand)).Returns(expectedId);

            IHttpActionResult callback = _carrierController.Post(carrierCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
            _carrierServiceMock.Verify(s => s.Add(carrierCommand));
        }

        [Test]
        public void Test_CarrierController_Post_ShouldThrowException()
        {
            var invalidCarrierCommand = ObjectMother.GetInvalidPhysicalCarrierRegisterCommandWithNullAddress(_cpf);

            IHttpActionResult callback = _carrierController.Post(invalidCarrierCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _carrierServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_CarrierController_Put_ShouldBeOk()
        {
            var isUpdated = true;
            var newAddressCommand = ObjectMother.GetValidAddresCommand();
            var carrierCommand = ObjectMother.GetExistentValidPhysicalCarrierUpdateCommand(newAddressCommand, _cpf);

            _carrierServiceMock.Setup(c => c.Update(carrierCommand)).Returns(isUpdated);

            IHttpActionResult callback = _carrierController.Put(carrierCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _carrierServiceMock.Verify(s => s.Update(carrierCommand), Times.Once);
        }

        [Test]
        public void Test_CarrierController_Put_ShouldThrowException()
        {
            var invalidCarrierCommand = ObjectMother.GetExistentInvalidPhysicalCarrierUpdateCommandWithNullAddress(_cpf);

            IHttpActionResult callback = _carrierController.Put(invalidCarrierCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _carrierServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_CarrierController_Delete_ShouldBeOk()
        {
            var CarrierCommand = ObjectMother.GetCarrierDeleteCommand();
            var isUpdated = true;
            _carrierServiceMock.Setup(c => c.Remove(CarrierCommand)).Returns(isUpdated);

            IHttpActionResult callback = _carrierController.Delete(CarrierCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _carrierServiceMock.Verify(s => s.Remove(CarrierCommand), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_AccountController_Delete_ShouldThrowException()
        {
            var invalidCarrierCommand = ObjectMother.GetCarrierDeleteCommandWithoutId();

            IHttpActionResult callback = _carrierController.Delete(invalidCarrierCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _carrierServiceMock.VerifyNoOtherCalls();
        }
    }
}
