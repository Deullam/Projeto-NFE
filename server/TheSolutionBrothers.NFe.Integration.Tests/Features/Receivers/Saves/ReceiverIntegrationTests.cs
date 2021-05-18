using System;
using TheSolutionBrothers.NFe.Application.Features.Receivers;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Addresses;
using TheSolutionBrothers.NFe.Infra.Data.Features.Receivers;
using NUnit.Framework;
using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.Nfe.API.Controllers.Receivers;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using System.Configuration;
using System.Data.Entity;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Mappers;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Net;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Receivers
{
	[TestFixture]
	public partial class ReceiverIntegrationTests
	{
        private ContextNfe _context;
        private ReceiversController _controller;
		private IReceiverRepository _receiverRepository;
		private IAddressRepository _addressRepository;
        private IInvoiceRepository _invoiceRepository;
        private IReceiverService _service;
        private IMapper _mapper;

		private AddressCommand _addressCommand;
		private Address _addressWithId;

		private CPF _cpf;
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
            _receiverRepository = new ReceiverRepository(_context);
            _addressRepository = new AddressRepository(_context);
            _invoiceRepository = new InvoiceRepository(_context);
            _service = new ReceiverService(_receiverRepository, _addressRepository, _invoiceRepository, _mapper);

            _controller = new ReceiversController(_service);

            _addressCommand = ObjectMother.GetValidAddresCommand();

            _cpf = ObjectMother.GetValidCPF();
            _cnpj = ObjectMother.GetValidCNPJ();
        }

        [Test]
        public void Test_ReceiverIntegration_AddPhysical_ShouldBeOk()
        {
            long expectedId = 3;
            ReceiverRegisterCommand receiverCommand = ObjectMother.GetValidPhysicalReceiverRegisterCommand(_addressCommand, _cpf);

            var result = _controller.Post(receiverCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
        }

        [Test]
        public void Test_ReceiverIntegration_AddLegal_ShouldBeOk()
        {
            long expectedId = 3;
            ReceiverRegisterCommand receiverCommand = ObjectMother.GetValidLegalReceiverRegisterCommand(_addressCommand, _cnpj);

            var result = _controller.Post(receiverCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
        }

        [Test]
        public void Test_ReceiverIntegration_Add_UninformedName_ShouldThrowException()
        {
            ReceiverRegisterCommand receiverCommand = ObjectMother.GetInvalidLegalReceiverRegisterCommandWithUninformedName(_addressCommand, _cnpj);

            var result = _controller.Post(receiverCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(3);
        }
    }
}
