using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using TheSolutionBrothers.Nfe.API.Controllers.Senders;
using TheSolutionBrothers.NFe.Application.Features.Senders;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Application.Mappers;
using System.Data.Entity;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using System.Configuration;
using TheSolutionBrothers.NFe.Infra.Data.Features.Addresses;
using TheSolutionBrothers.NFe.Infra.Data.Features.Senders;
using TheSolutionBrothers.NFe.Application.Features.Senders.Commands;
using System.Web.Http.Results;
using System.Net;
using FluentValidation.Results;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Senders
{
    [TestFixture]
    public partial class SenderIntegrationTests
    {
        private ContextNfe _context;
        private SendersController _controller;
        private ISenderRepository _receiverRepository;
        private IAddressRepository _addressRepository;
        private IInvoiceRepository _invoiceRepository;
        private ISenderService _service;
        private IMapper _mapper;

        private AddressCommand _addressCommand;
        private Address _addressWithId;

        private CNPJ _cnpj;

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


            _mapper = Mapper.Instance;
            _receiverRepository = new SenderRepository(_context);
            _addressRepository = new AddressRepository(_context);
            _invoiceRepository = new InvoiceRepository(_context);
            _service = new SenderService(_receiverRepository, _addressRepository, _invoiceRepository, _mapper);

            _controller = new SendersController(_service);

            _addressCommand = ObjectMother.GetValidAddresCommand();

            _cnpj = ObjectMother.GetValidCNPJ();
        }

        [Test]
        public void Test_SenderIntegration_Add_ShouldBeOk()
        {
            long expectedId = 3;
            SenderRegisterCommand senderCommand = ObjectMother.GetNewValidSenderRegisterCommand(_addressCommand, _cnpj);

            var result = _controller.Post(senderCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
        }

        [Test]
        public void Test_SenderIntegration_Add_UninformedCompanyName_ShouldThrowException()
        {
            SenderRegisterCommand senderCommand = ObjectMother.GetInvalidSenderRegisterCommandWithUninformedCompanyName(_addressCommand, _cnpj);

            var result = _controller.Post(senderCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }
    }
}
