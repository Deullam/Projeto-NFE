using Microsoft.AspNet.OData;
using FluentAssertions;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using TheSolutionBrothers.Nfe.API.Controllers.Senders;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Senders;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Controller.Tests.Initializer;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Senders.Queries;

namespace TheSolutionBrothers.NFe.Controller.Tests.Features.Senders
{
	[TestFixture]
	public class SenderControllerTests : TestControllerBase
	{
		private SendersController _senderController;
		private Mock<ISenderService> _senderServiceMock;
		private Mock<Address> _fakeAddress;
		private AddessViewModel _addressViewModel;
		private Sender _sender;

		private CNPJ _validCnpj;

		private AddressCommand _addressCommand;

		[SetUp]
		public void Initialize()
		{
			HttpRequestMessage request = new HttpRequestMessage();
			request.SetConfiguration(new HttpConfiguration());
			_senderServiceMock = new Mock<ISenderService>();
			_senderController = new SendersController(_senderServiceMock.Object)
			{
				Request = request
			};
			_fakeAddress = new Mock<Address>();
            _validCnpj = ObjectMother.GetValidCNPJ();
			_addressViewModel = ObjectMother.GetAddessViewModel();
			_sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), _validCnpj);

			_validCnpj = ObjectMother.GetValidCNPJ();

			_addressCommand = ObjectMother.GetValidAddresCommand();
		}


		[Test]
		public void Test_SenderController_Get_ShouldBeOk()
		{
			var address = ObjectMother.GetExistentValidAddress();
			var sender = ObjectMother.GetExistentValidSender(address, _validCnpj);
			var response = new List<Sender>() { sender }.AsQueryable();
			_senderServiceMock.Setup(s => s.GetAll()).Returns(response);
			var id = 1;
			var odataOptions = GetOdataQueryOptions<Sender>(_senderController);

			var callback = _senderController.Get(odataOptions);

			_senderServiceMock.Verify(s => s.GetAll(), Times.Once);
			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<SenderViewModel>>>().Subject;
			httpResponse.Content.Should().NotBeNullOrEmpty();
			httpResponse.Content.First().Id.Should().Be(id);
		}

		[Test]
		public void Test_SenderController_GetWithSize_ShouldBeOk()
		{
			var size = 1;
			long id = 1;
			var odataOptions = GetOdataQueryOptions<Sender>(_senderController);
			_senderController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/senders?size=" + size);
			var response = new List<Sender>() { _sender }.AsQueryable();
			_senderServiceMock.Setup(s => s.GetAll(It.IsAny<SenderGetAllQuery>())).Returns(response);

			var callback = _senderController.Get(odataOptions);

			_senderServiceMock.Verify(s => s.GetAll(It.IsAny<SenderGetAllQuery>()), Times.Once);
			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<SenderViewModel>>>().Subject;
			httpResponse.Content.Should().NotBeNullOrEmpty();
			httpResponse.Content.First().Id.Should().Be(id);
		}

		[Test]
		public void Test_SenderController_GetById_ShouldBeOk()
		{
			long id = 1;
			var sender = ObjectMother.GetValidSenderViewModel(_addressViewModel, _validCnpj);
			_senderServiceMock.Setup(c => c.GetById(id)).Returns(sender);

			IHttpActionResult callback = _senderController.GetById(id);

			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<SenderViewModel>>().Subject;
			httpResponse.Content.Should().NotBeNull();
			httpResponse.Content.Id.Should().Be(id);
			_senderServiceMock.Verify(s => s.GetById(id));
		}

		[Test]
		public void Test_SenderController_Post_ShouldBeOk()
		{
			long expectedId = 1;
			var senderCommand = ObjectMother.GetNewValidSenderRegisterCommand(_addressCommand, _validCnpj);
			_senderServiceMock.Setup(c => c.Add(senderCommand)).Returns(expectedId);

			IHttpActionResult callback = _senderController.Post(senderCommand);

			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
			httpResponse.Content.Should().Be(expectedId);
			_senderServiceMock.Verify(s => s.Add(senderCommand));
		}

		[Test]
		public void Test_SenderController_Post_ShouldThrowException()
		{
			int expectedQtd = 1;
			var invalidSenderCommand = ObjectMother.GetInvalidSenderRegisterCommandWithUninformedCompanyName(_addressCommand, _validCnpj);

			IHttpActionResult callback = _senderController.Post(invalidSenderCommand);
			var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
			httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			httpResponse.Content.Should().HaveCount(expectedQtd);
			_senderServiceMock.VerifyNoOtherCalls();
		}

		[Test]
		public void Test_SenderController_Put_ShouldBeOk()
		{
			var isUpdated = true;
			var senderCommand = ObjectMother.GetExistentValidSenderCommandToUpdate(_addressCommand, _validCnpj);
			_senderServiceMock.Setup(c => c.Update(senderCommand)).Returns(isUpdated);

			IHttpActionResult callback = _senderController.Put(senderCommand);

			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
			httpResponse.Content.Should().BeTrue();
			_senderServiceMock.Verify(s => s.Update(senderCommand), Times.Once);
		}

		[Test]
		public void Test_SenderController_Put_ShouldThrowException()
		{
			int expectedQtd = 1;
			var invalidSenderCommand = ObjectMother.GetNewInvalidSenderUpdateCommandWithUninformedCompanyName(_addressCommand, _validCnpj);

			IHttpActionResult callback = _senderController.Put(invalidSenderCommand);
			var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
			httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			httpResponse.Content.Should().HaveCount(expectedQtd);
			_senderServiceMock.VerifyNoOtherCalls();
		}

		[Test]
		public void Test_SenderController_Delete_ShouldBeOk()
		{
			var senderCommand = ObjectMother.GetSenderDeleteCommand();
			var isUpdated = true;
			_senderServiceMock.Setup(c => c.Remove(senderCommand)).Returns(isUpdated);

			IHttpActionResult callback = _senderController.Delete(senderCommand);

			var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
			_senderServiceMock.Verify(s => s.Remove(senderCommand), Times.Once);
			httpResponse.Content.Should().BeTrue();
		}

		[Test]
		public void Test_AccountController_Delete_ShouldThrowException()
		{
			int expectedQtd = 1;
			long[] emptyArraySender = { };
			var invalidSenderCommand = ObjectMother.GetSenderCommandToDelete(emptyArraySender);

			IHttpActionResult callback = _senderController.Delete(invalidSenderCommand);
			var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
			httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			httpResponse.Content.Should().HaveCount(expectedQtd);
			_senderServiceMock.VerifyNoOtherCalls();
		}
	}
}

