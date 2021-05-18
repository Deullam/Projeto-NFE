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
using TheSolutionBrothers.Nfe.API.Controllers.Receivers;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Receivers;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Queries;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Controller.Tests.Initializer;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;

namespace TheSolutionBrothers.NFe.Controller.Tests.Features.Receivers
{
    [TestFixture]
    public class ReceiverControllerTests : TestControllerBase
    {
        private ReceiversController _receiverController;
        private Mock<IReceiverService> _receiverServiceMock;
        private Mock<Address> _fakeAddress;
        private AddessViewModel _addressViewModel;
        private Mock<Receiver> _receiver;

        private CPF _validCpf;
        private CNPJ _validCnpj;

        private AddressCommand _addressCommand;

        [SetUp]
        public void Initialize()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _receiverServiceMock = new Mock<IReceiverService>();
            _receiverController = new ReceiversController(_receiverServiceMock.Object)
            {
                Request = request
            };
            _fakeAddress = new Mock<Address>();
            _addressViewModel = ObjectMother.GetAddessViewModel();
            _receiver = new Mock<Receiver>();

            _validCpf = ObjectMother.GetValidCPF();
            _validCnpj = ObjectMother.GetValidCNPJ();

            _addressCommand = ObjectMother.GetValidAddresCommand();
        }


        [Test]
        public void Test_ReceiverController_Get_ShouldBeOk()
        {
            var address = ObjectMother.GetExistentValidAddress();
            var receiver = ObjectMother.GetExistentValidLegalReceiver(address, _validCnpj);
            var response = new List<Receiver>() { receiver }.AsQueryable();
            _receiverServiceMock.Setup(s => s.GetAll()).Returns(response);
            var id = 1;
            var odataOptions = GetOdataQueryOptions<Receiver>(_receiverController);

            var callback = _receiverController.Get(odataOptions);

            _receiverServiceMock.Verify(s => s.GetAll(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ReceiverViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Test_ReceiverController_GetWithSize_ShouldBeOk()
        {
            var size = 1;
            long id = 1;
            var address = ObjectMother.GetExistentValidAddress();
            var receiver = ObjectMother.GetExistentValidLegalReceiver(address, _validCnpj);
            var odataOptions = GetOdataQueryOptions<Receiver>(_receiverController);
            _receiverController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/receivers?size=" + size);
            var response = new List<Receiver>() { receiver }.AsQueryable();
            _receiverServiceMock.Setup(s => s.GetAll(It.IsAny<ReceiverGetAllQuery>())).Returns(response);

            var callback = _receiverController.Get(odataOptions);

            _receiverServiceMock.Verify(s => s.GetAll(It.IsAny<ReceiverGetAllQuery>()), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<ReceiverViewModel>>>().Subject;

            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Test_ReceiverController_GetById_ShouldBeOk()
        {
            long id = 1;
            var receiver = ObjectMother.GetPhysicalReceiverViewModel(_addressViewModel, _validCpf);
            _receiverServiceMock.Setup(c => c.GetById(id)).Returns(receiver);

            IHttpActionResult callback = _receiverController.GetById(id);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<ReceiverViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _receiverServiceMock.Verify(s => s.GetById(id));
        }

        [Test]
        public void Test_ReceiverController_Post_ShouldBeOk()
        {
            long expectedId = 1;
            var receiverCommand = ObjectMother.GetValidLegalReceiverRegisterCommand(_addressCommand, _validCnpj);
            _receiverServiceMock.Setup(c => c.Add(receiverCommand)).Returns(expectedId);

            IHttpActionResult callback = _receiverController.Post(receiverCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
            _receiverServiceMock.Verify(s => s.Add(receiverCommand));
        }

        [Test]
        public void Test_ReceiverController_Post_ShouldThrowException()
        {
            var invalidReceiverCommand = ObjectMother.GetInvalidLegalReceiverRegisterCommandWithUninformedName(_addressCommand, _validCnpj);

            IHttpActionResult callback = _receiverController.Post(invalidReceiverCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(3);
            _receiverServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ReceiverController_Put_ShouldBeOk()
        {
            var isUpdated = true;
            var receiverCommand = ObjectMother.GetExistentValidLegalReceiverUpdateCommand(_addressCommand, _validCnpj);
            _receiverServiceMock.Setup(c => c.Update(receiverCommand)).Returns(isUpdated);

            IHttpActionResult callback = _receiverController.Put(receiverCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _receiverServiceMock.Verify(s => s.Update(receiverCommand), Times.Once);
        }

        [Test]
        public void Test_ReceiverController_Put_ShouldThrowException()
        {
            var invalidReceiverCommand = ObjectMother.GetLegalReceiverUpdateCommandWithUninformedName(_addressCommand, _validCnpj);

            IHttpActionResult callback = _receiverController.Put(invalidReceiverCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _receiverServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_ReceiverController_Delete_ShouldBeOk()
        {
            var receiverCommand = ObjectMother.GetReceiverDeleteCommand();
            var isUpdated = true;
            _receiverServiceMock.Setup(c => c.Remove(receiverCommand)).Returns(isUpdated);

            IHttpActionResult callback = _receiverController.Delete(receiverCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _receiverServiceMock.Verify(s => s.Remove(receiverCommand), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_AccountController_Delete_ShouldThrowException()
        {
            var invalidReceiverCommand = ObjectMother.GetReceiverDeleteCommandWithoutId();

            IHttpActionResult callback = _receiverController.Delete(invalidReceiverCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _receiverServiceMock.VerifyNoOtherCalls();
        }
    }
}
