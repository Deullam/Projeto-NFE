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
using TheSolutionBrothers.Nfe.API.Controllers.Invoices;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Invoices;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Commands;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Queries;
using TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Controller.Tests.Initializer;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;

namespace TheSolutionBrothers.NFe.Controller.Tests.Features.Invoices
{
    [TestFixture]
    public class InvoiceControllerTests : TestControllerBase
    {
        private InvoicesController _invoiceController;
        private Mock<IInvoiceService> _invoiceServiceMock;
        private Mock<Invoice> invoice;
        private Invoice _invoice;

        private List<InvoiceItemRegisterCommand> unattachedItems;
        private List<InvoiceItemUpdateCommand> attachedItems;

        CPF _cpf;
        CNPJ _cnpj;
        Product _product;
        Address _address;
        Receiver _receiver;
        Carrier _carrier;
        Sender _sender;
        InvoiceItem _invoiceItem;

        [SetUp]
        public void Initialize()
        {
            _cpf = ObjectMother.GetValidCPF();
            _cnpj = ObjectMother.GetValidCNPJ();
            _product = ObjectMother.GetExistentValidProduct(new TaxProduct());
            _address = ObjectMother.GetExistentValidAddress();
            _receiver = ObjectMother.GetExistentValidPhysicalReceiver(_address,_cpf);
            _carrier = ObjectMother.GetExistentValidCarrierPhysical(_address,_cpf);
            _sender = ObjectMother.GetExistentValidSender(_address,_cnpj);
            _invoice = ObjectMother.GetExistentValidOpenedInvoice(_sender, _receiver, _carrier, null);
            _invoiceItem = ObjectMother.GetExistentConsolidatedInvoiceItem(_invoice,_product);
            _invoice.InvoiceItems = new List<InvoiceItem>() { _invoiceItem };

            HttpRequestMessage request = new HttpRequestMessage();
            request.SetConfiguration(new HttpConfiguration());
            _invoiceServiceMock = new Mock<IInvoiceService>();
            _invoiceController = new InvoicesController(_invoiceServiceMock.Object)
            {
                Request = request
            };

            invoice = new Mock<Invoice>();
            
            attachedItems = new List<InvoiceItemUpdateCommand>()
            {
                ObjectMother.GetValidInvoiceItemUpdateCommand()
            };
            
            unattachedItems = new List<InvoiceItemRegisterCommand>()
            {
                ObjectMother.GetValidInvoiceItemRegisterCommand()
            };
        }


        [Test]
        public void Test_InvoiceController_Get_ShouldBeOk()
        {
            var response = new List<Invoice>() { _invoice }.AsQueryable();
            _invoiceServiceMock.Setup(s => s.GetAll()).Returns(response);
            var id = 1;
            var odataOptions = GetOdataQueryOptions<Invoice>(_invoiceController);

            var callback = _invoiceController.Get(odataOptions);

            _invoiceServiceMock.Verify(s => s.GetAll(), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<InvoiceViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Test_InvoiceController_GetWithSize_ShouldBeOk()
        {
            var size = 1;
            long id = 1;
            var odataOptions = GetOdataQueryOptions<Invoice>(_invoiceController);
            _invoiceController.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/Invoices?size=" + size);
            
            var response = new List<Invoice>() { _invoice }.AsQueryable();
            _invoiceServiceMock.Setup(s => s.GetAll(It.IsAny<InvoiceGetAllQuery>())).Returns(response);

            var callback = _invoiceController.Get(odataOptions);

            _invoiceServiceMock.Verify(s => s.GetAll(It.IsAny<InvoiceGetAllQuery>()), Times.Once);
            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<InvoiceViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(id);
        }

        [Test]
        public void Test_InvoiceController_GetById_ShouldBeOk()
        {
            long id = 1;
            var invoiceViewModel = ObjectMother.GetInvoiceViewModel(It.IsAny<SenderViewModel>(),
                It.IsAny<ReceiverViewModel>(),
                It.IsAny<CarrierViewModel>(),
                It.IsAny<InvoiceTaxViewModel>(),
                new List<InvoiceItemViewModel>() { It.IsAny<InvoiceItemViewModel>() });
            _invoiceServiceMock.Setup(c => c.GetById(id)).Returns(invoiceViewModel);

            IHttpActionResult callback = _invoiceController.GetById(id);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<InvoiceViewModel>>().Subject;
            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
            _invoiceServiceMock.Verify(s => s.GetById(id));
        }

        [Test]
        public void Test_InvoiceController_Post_ShouldBeOk()
        {
            long expectedId = 1;
            var invoiceCommand = ObjectMother.GetValidInvoiceRegisterCommand(unattachedItems);

            _invoiceServiceMock.Setup(c => c.Add(invoiceCommand)).Returns(expectedId);

            IHttpActionResult callback = _invoiceController.Post(invoiceCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
            _invoiceServiceMock.Verify(s => s.Add(invoiceCommand));
        }

        [Test]
        public void Test_InvoiceController_Post_ShouldThrowException()
        {
            var invalidInvoiceCommand = ObjectMother.GetInvalidInvoiceRegisterCommandWithUninformedNatureOperation(unattachedItems);

            IHttpActionResult callback = _invoiceController.Post(invalidInvoiceCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _invoiceServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceController_Put_ShouldBeOk()
        {
            var isUpdated = true;
            var invoiceCommand = ObjectMother.GetValidInvoiceUpdateCommand();

            _invoiceServiceMock.Setup(c => c.Update(invoiceCommand)).Returns(isUpdated);

            IHttpActionResult callback = _invoiceController.Put(invoiceCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _invoiceServiceMock.Verify(s => s.Update(invoiceCommand), Times.Once);
        }

        [Test]
        public void Test_InvoiceController_Put_ShouldThrowException()
        {
            var invalidInvoiceCommand = ObjectMother.GetInvalidInvoiceUpdateCommandWithoutId();

            IHttpActionResult callback = _invoiceController.Put(invalidInvoiceCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCountGreaterThan(0);
            _invoiceServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceController_Delete_ShouldBeOk()
        {
            var InvoiceCommand = ObjectMother.GetInvoiceDeleteCommand();
            var isUpdated = true;
            _invoiceServiceMock.Setup(c => c.Remove(InvoiceCommand)).Returns(isUpdated);

            IHttpActionResult callback = _invoiceController.Delete(InvoiceCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _invoiceServiceMock.Verify(s => s.Remove(InvoiceCommand), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_AccountController_Delete_ShouldThrowException()
        {
            var invalidInvoiceCommand = ObjectMother.GetInvoiceDeleteCommandWithoutId();

            IHttpActionResult callback = _invoiceController.Delete(invalidInvoiceCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _invoiceServiceMock.VerifyNoOtherCalls();
        }
        
        [Test]
        public void Test_InvoiceController_PostItems_ShouldBeOk()
        {
            long expectedId = 1;
            long invoiceId = 1;

            var command = ObjectMother.GetValidInvoiceItemRegisterCommand();

            var commands = new List<InvoiceItemRegisterCommand>() { command };

            _invoiceServiceMock.Setup(c => c.AddItems(invoiceId, commands)).Returns(new List<long>() { expectedId });

            IHttpActionResult callback = _invoiceController.PostItem(invoiceId, commands);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<List<long>>>().Subject;
            httpResponse.Content.Should().HaveCountGreaterThan(0);
            httpResponse.Content.Should().Contain(expectedId);
            _invoiceServiceMock.Verify(s => s.AddItems(invoiceId, commands));
        }

        [Test]
        public void Test_InvoiceController_PostItems_ShouldThrowException()
        {
            long invoiceId = 1;

            var invalidCommand = ObjectMother.GetInvalidInvoiceItemRegisterCommandWithUninformedProduct();
            var commands = new List<InvoiceItemRegisterCommand>() { invalidCommand };

            IHttpActionResult callback = _invoiceController.PostItem(invoiceId, commands);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _invoiceServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceController_PutItems_ShouldBeOk()
        {
            var isUpdated = true;
            long invoiceId = 1;

            var command = ObjectMother.GetValidInvoiceItemUpdateCommand();

            var commands = new List<InvoiceItemUpdateCommand>() { command };
            
            _invoiceServiceMock.Setup(c => c.UpdateItems(invoiceId, commands)).Returns(isUpdated);

            IHttpActionResult callback = _invoiceController.PutItem(invoiceId, commands);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
            _invoiceServiceMock.Verify(s => s.UpdateItems(invoiceId, commands), Times.Once);
        }

        [Test]
        public void Test_InvoiceController_PutItems_ShouldThrowException()
        {
            long invoiceId = 1;
            var invalidCommand = ObjectMother.GetInvalidInvoiceItemUpdateCommandWithUninformedId();
            var commands = new List<InvoiceItemUpdateCommand>() { invalidCommand };

            IHttpActionResult callback = _invoiceController.PutItem(invoiceId, commands);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCountGreaterThan(0);
            _invoiceServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceController_DeleteItems_ShouldBeOk()
        {
            long invoiceId = 1;
            var InvoiceCommand = ObjectMother.GetValidInvoiceItemDeleteCommand();
            var isUpdated = true;
            _invoiceServiceMock.Setup(c => c.DeleteItems(invoiceId, InvoiceCommand)).Returns(isUpdated);

            IHttpActionResult callback = _invoiceController.DeleteItem(invoiceId, InvoiceCommand);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            _invoiceServiceMock.Verify(s => s.DeleteItems(invoiceId, InvoiceCommand), Times.Once);
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_AccountController_DeleteItems_ShouldThrowException()
        {
            long invoiceId = 1;
            var invalidCommand = ObjectMother.GetInvalidInvoiceItemDeleteCommandWithEmptyList();

            IHttpActionResult callback = _invoiceController.DeleteItem(invoiceId, invalidCommand);
            var httpResponse = callback.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
            _invoiceServiceMock.VerifyNoOtherCalls();
        }
    }
}
