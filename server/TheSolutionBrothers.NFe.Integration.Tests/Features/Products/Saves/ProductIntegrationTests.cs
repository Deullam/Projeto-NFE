using System;
using TheSolutionBrothers.NFe.Application.Features.Products;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Infra.Data.Features.Addresses;
using TheSolutionBrothers.NFe.Infra.Data.Features.Products;
using NUnit.Framework;
using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.Nfe.API.Controllers.Products;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using System.Configuration;
using System.Data.Entity;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Mappers;
using TheSolutionBrothers.NFe.Application.Features.Products.Commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Net;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Products
{
	[TestFixture]
	public partial class ProductIntegrationTests
	{
        private ContextNfe _context;
        private ProductsController _controller;
		private IProductRepository _ProductRepository;
		private IProductService _service;
        private IMapper _mapper;


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
            _ProductRepository = new ProductRepository(_context);

            _service = new ProductService(_ProductRepository, _mapper);

            _controller = new ProductsController(_service);
        }

        [Test]
        public void Test_ProductIntegration_Add_ShouldBeOk()
        {
            long expectedId = 2;
            ProductRegisterCommand ProductCommand = ObjectMother.GetValidProductRegisterCommand();

            var result = _controller.Post(ProductCommand);

            var httpResponse = result.Should().BeOfType<OkNegotiatedContentResult<long>>().Subject;
            httpResponse.Content.Should().Be(expectedId);
        }

        [Test]
        public void Test_ProductIntegration_Add_InvalidProduct_ShouldThrowException()
        {
            ProductRegisterCommand ProductCommand = ObjectMother.GetInvalidProductWithCodeLengthOverflowRegisterCommand();

            var result = _controller.Post(ProductCommand);

            var httpResponse = result.Should().BeOfType<NegotiatedContentResult<IList<ValidationFailure>>>().Subject;
            httpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            httpResponse.Content.Should().HaveCount(1);
        }
    }
}
