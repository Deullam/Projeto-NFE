using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using TheSolutionBrothers.Nfe.API.Controllers.Carriers;
using TheSolutionBrothers.NFe.Application.Features.Carriers;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Mappers;
using System.Data.Entity;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using System.Configuration;
using TheSolutionBrothers.NFe.Infra.Data.Features.Carriers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Addresses;
using TheSolutionBrothers.NFe.Application.Features.Receivers;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;
using System.Web.Http.Results;
using FluentValidation.Results;
using System.Net;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Carriers
{
    [TestFixture]
    public partial class CarrierIntegrationTests
    {
        private ContextNfe _context;
        private CarriersController _controller;
        private ICarrierRepository _receiverRepository;
        private IAddressRepository _addressRepository;
        private IInvoiceRepository _invoiceRepository;
        private ICarrierService _service;
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
            _receiverRepository = new CarrierRepository(_context);
            _addressRepository = new AddressRepository(_context);
            _invoiceRepository = new InvoiceRepository(_context);
            _service = new CarrierService(_receiverRepository, _addressRepository, _invoiceRepository, _mapper);

            _controller = new CarriersController(_service);

            _addressCommand = ObjectMother.GetValidAddresCommand();

            _cpf = ObjectMother.GetValidCPF();
            _cnpj = ObjectMother.GetValidCNPJ();
        }

        [Test]
        public void Test_CarrierIntegration_AddPhysical_ShouldBeOk()
        {
            long expectedId = 3;
            CarrierRegisterCommand carrierCommand = ObjectMother.GetValidPhysicalCarrierRegisterCommand(_addressCommand, _cpf);

            var result = _controller.Post(carrierCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
        }

        [Test]
        public void Test_CarrierIntegration_AddLegal_ShouldBeOk()
        {
            long expectedId = 3;
            CarrierRegisterCommand carrierCommand = ObjectMother.GetValidLegalCarrierRegisterCommand(_addressCommand, _cnpj);

            var result = _controller.Post(carrierCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
        }

        [Test]
        public void Test_CarrierIntegration_Add_UninformedName_ShouldThrowException()
        {
            CarrierRegisterCommand carrierCommand = ObjectMother.GetInvalidLegalCarrierRegisterCommandWithUninformdName(_addressCommand, _cnpj);

            var result = _controller.Post(carrierCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(3);
        }

    }        
}
