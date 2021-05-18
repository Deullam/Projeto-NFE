using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Application.Features.Invoices;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceTaxes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using TheSolutionBrothers.NFe.Application.Features.Senders;
using TheSolutionBrothers.NFe.Application.Features.Receivers;
using TheSolutionBrothers.NFe.Application.Features.Carriers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Products;
using TheSolutionBrothers.NFe.Infra.Data.Features.Senders;
using TheSolutionBrothers.NFe.Infra.Data.Features.Receivers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Carriers;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Mappers;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Commands;
using System.Web.Http;
using TheSolutionBrothers.Nfe.API.Controllers.Invoices;
using System.Web.Http.Results;
using TheSolutionBrothers.Nfe.API.Exceptions;
using System.Net;
using FluentValidation.Results;
using TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels;
using Microsoft.AspNet.OData;
using System.Net.Http;
using System.Linq;
using System.Data.Entity;
using System.Configuration;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Invoices
{
    [TestFixture]
    public partial class InvoiceIntegrationTests
    {
        private InvoicesController _controller;
        private ContextNfe _context;
        private IInvoiceRepository _invoiceRepository;
        private IInvoiceService _invoiceService;
        private ISenderRepository _senderRepoisitory;
        private IReceiverRepository _receiverRepository;
        private ICarrierRepository _carrierRepository;
        private ProductRepository _productRepository;
        private IInvoiceItemRepository _invoiceItemRepository;

        private IMapper _mapper;

        private InvoiceItemUpdateCommand invoiceItem;
        private InvoiceItemRegisterCommand newinvoiceItem;

        private List<InvoiceItemUpdateCommand> attachedItems;
        private List<InvoiceItemRegisterCommand> unattachedItems;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            AutoMapperConfig.Reset();
            AutoMapperConfig.RegisterMappings();
        }

        [SetUp]
        public void Initialize()
        {
            Database.SetInitializer(new DbCreator<ContextNfe>());
            _context = new ContextNfe(ConfigurationManager.AppSettings.Get("ConnectionStringName"));

            _invoiceRepository = new InvoiceRepository(_context);
            _senderRepoisitory = new SenderRepository(_context);
            _receiverRepository = new ReceiverRepository(_context);
            _carrierRepository= new CarrierRepository(_context);
            _productRepository= new ProductRepository(_context);
            _invoiceItemRepository= new InvoiceItemRepository(_context);
            _mapper = Mapper.Instance;
            _invoiceService = new InvoiceService(_invoiceRepository, _carrierRepository, _receiverRepository, _senderRepoisitory, _productRepository, _invoiceItemRepository, _mapper);
            _controller = new InvoicesController(_invoiceService);


            invoiceItem = ObjectMother.GetValidInvoiceItemUpdateCommand();
            newinvoiceItem = ObjectMother.GetValidInvoiceItemRegisterCommand();

            attachedItems = new List<InvoiceItemUpdateCommand>() { invoiceItem };
            unattachedItems = new List<InvoiceItemRegisterCommand>() { newinvoiceItem };
        }

        [Test]
        public void Test_InvoiceIntegration_Delete_ShouldBeOk()
        {
            InvoiceDeleteCommand receiverCommand = ObjectMother.GetInvoiceDeleteCommand();

            IHttpActionResult result = _controller.Delete(receiverCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }


        [Test]
        public void Test_InvoiceIntegration_Delete_NonExistentId_ShouldThrowException()
        {
            var expectedMessage = "Registro não encontrado";
            InvoiceDeleteCommand InvoiceCommand = ObjectMother.GetInvoiceDeleteCommandWithNonExistentIds();

            var result = _controller.Delete(InvoiceCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<ExceptionPayload>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.ErrorMessage.Should().Be(expectedMessage);
        }

        [Test]
        public void Test_InvoiceIntegration_Delete_InvalidCommand_ShouldThrowException()
        {
            InvoiceDeleteCommand invalidInvoiceCommand = ObjectMother.GetInvoiceDeleteCommandWithoutId();

            var result = _controller.Delete(invalidInvoiceCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }

        [Test]
        public void Test_InvoiceIntegration_GetById_ShouldBeOk()
        {
            long id = 1;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<InvoiceViewModel>>().Subject;

            httpResponse.Content.Should().NotBeNull();
            httpResponse.Content.Id.Should().Be(id);
        }

        [Test]
        public void Test_InvoiceIntegration_Get_IdInvalid_ShouldThrowException()
        {
            long id = -1;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<InvoiceViewModel>>().Subject;

            httpResponse.Content.Should().BeNull();
        }

        [Test]
        public void Test_InvoiceIntegration_Get_NonexistentId_ShouldBeOk()
        {
            long id = 99999;
            var result = _controller.GetById(id);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<InvoiceViewModel>>().Subject;

            httpResponse.Content.Should().BeNull();
        }

        [Test]
        public void Test_InvoiceIntegration_GetAll_ShouldBeOk()
        {
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Invoice>(_controller);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<InvoiceViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void Test_InvoiceIntegration_GetWithSize_ShouldBeOk()
        {
            var expectedCount = 1;
            var size = 1;
            long expectedFirstId = 1;
            var odataOptions = OdataQueryCreator.GetOdataQueryOptions<Invoice>(_controller);
            _controller.Request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:61552/api/Invoices?size=" + size);

            var callback = _controller.Get(odataOptions);

            var httpResponse = callback.Should().BeOfType<OkNegotiatedContentResult<PageResult<InvoiceViewModel>>>().Subject;
            httpResponse.Content.Should().NotBeNullOrEmpty();
            httpResponse.Content.First().Id.Should().Be(expectedFirstId);
            httpResponse.Content.Should().HaveCount(expectedCount);
        }

        [Test]
        public void Test_InvoiceIntegration_Update_ShouldBeOk()
        {
            InvoiceItemUpdateCommand invoiceItem = ObjectMother.GetValidInvoiceItemUpdateCommand();
            invoiceItem.Id = 2;
            attachedItems.Add(invoiceItem);
            InvoiceUpdateCommand InvoiceCommand = ObjectMother.GetValidInvoiceUpdateCommand();

            var result = _controller.Put(InvoiceCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();
        }

        [Test]
        public void Test_InvoiceIntegration_Update_RemovingItem_ShouldBeOk()
        {
            int expectedAcount = 1;
            InvoiceUpdateCommand InvoiceCommand = ObjectMother.GetValidInvoiceUpdateCommand();

            var result = _controller.Put(InvoiceCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();

            var callback = _controller.GetById(InvoiceCommand.Id);
            var invoice = callback.Should().BeOfType<OkNegotiatedContentResult<InvoiceViewModel>>().Subject;

            invoice.Content.Should().NotBeNull();
            invoice.Content.InvoiceItems.Should().HaveCount(expectedAcount);
        }

        [Test]
        public void Test_InvoiceIntegration_Update_AddingItem_ShouldBeOk()
        {
            int expectedAcount = 3;

            InvoiceItemRegisterCommand newinvoiceItem = ObjectMother.GetValidInvoiceItemRegisterCommand();
            unattachedItems.Add(newinvoiceItem);
            InvoiceUpdateCommand InvoiceCommand = ObjectMother.GetValidInvoiceUpdateCommand();

            var result = _controller.Put(InvoiceCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<bool>>().Subject;
            httpResponse.Content.Should().BeTrue();

            var callback = _controller.GetById(InvoiceCommand.Id);
            var invoice = callback.Should().BeOfType<OkNegotiatedContentResult<InvoiceViewModel>>().Subject;

            invoice.Content.Should().NotBeNull();
            invoice.Content.InvoiceItems.Should().HaveCount(expectedAcount);
        }

        [Test]
        public void Test_InvoiceIntegration_Update_UninformedNatureOperation_ShouldThrowException()
        {

            InvoiceUpdateCommand InvoiceCommand = ObjectMother.GetInvalidInvoiceUpdateCommandWithoutUninformedNatureOperation();

            var result = _controller.Put(InvoiceCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }

        //[Test]
        //public void Test_InvoiceIntegration_DeleteWithInvoiceStatusEqualsIssued_ShouldThrowException()
        //{
        //          long existentId = 4;
        //	CPF cpfCarrier = ObjectMother.GetValidCPF();
        //	CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
        //	CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

        //	Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
        //	Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
        //	Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

        //	IList<InvoiceItem> items = new List<InvoiceItem>();

        //	KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();
        //	InvoiceSender invoiceSender = ObjectMother.GetExistentInvoinceSenderOk(null, sender);
        //	InvoiceReceiver invoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(null, receiver);
        //	InvoiceCarrier invoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(carrier, null);
        //	InvoiceTax invoiceTax = ObjectMother.GetExistentValidInvoiceTax(null);


        //	Invoice invoice = ObjectMother.GetNewValidIssuedInvoice(keyAccess, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, items);
        //          invoice.Id = existentId;
        //	Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
        //	InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product);

        //	Action action = () => _invoiceService.Delete(invoice);
        //	action.Should().Throw<InvoiceDeleteIssuedInvoiceException>();
        //}
    }
}
