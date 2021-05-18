using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using TheSolutionBrothers.NFe.Infra.Data.Features.Products;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Infra.Data.Tests.Initializer;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.Products
{

    [TestFixture]
    public class ProductRepositoryTests : EffortTestBase
    {

        private IProductRepository _repository;

        [SetUp]
        public override void Initialize()
        {
            base.Initialize();
            _repository = new ProductRepository(_contexto);
        }

        [Test]
        public void Test_ProductRepository_Save_ShouldBeOk()
        {
            Product product = ObjectMother.GetNewValidProduct(new TaxProduct());
            product = _repository.Add(product);
            product.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void Test_ProductRepository_Update_ShouldBeOk()
        {
            var wasUpdated = false;
            var newProductDescription = "Nova descricao";
            _productSeed.Description = newProductDescription;
            var action = new Action(() => { wasUpdated = _repository.Update(_productSeed); });
            action.Should().NotThrow<Exception>();
            wasUpdated.Should().BeTrue();
        }

        [Test]
        public void Test_ProductRepository_Delete_ShouldBeOk()
        {
            Product product = ObjectMother.GetExistentValidProductWithoutDependency(new TaxProduct());
            _repository.Remove(product.Id);
            _repository.GetById(product.Id).Should().BeNull();
        }

        [Test]
        public void Test_ProductRepository_Delete_ShouldHandleNotFoundException()
        {
            long notFoundId = 10;

            Action action = () => _repository.Remove(notFoundId);

            action.Should().Throw<NotFoundException>();
        }


        [Test]
        public void Test_ProductRepository_GetById_ShouldBeOk()
        {
            long existentId = 1;
            Product product = _repository.GetById(existentId);
            product.Should().NotBeNull();
        }


        [Test]
        public void Test_ProductRepository_GetNonexistentId_ShouldBeOk()
        {
            long nonexistentId = 100;
            Product product = _repository.GetById(nonexistentId);
            product.Should().BeNull();
        }

        [Test]
        public void Test_ProductRepository_GetAll_ShouldBeOk()
        {
            int expectedCount = 2;
            IQueryable<Product> productes = _repository.GetAll();
            productes.ToList().Count.Should().Be(expectedCount);
        }

        [Test]
        public void Test_ProductRepository_GetAllWithAmount_ShouldBeOk()
        {
            int amount = 2;
            var products = _repository.GetAll(amount);

            products.Should().NotBeNull();
            products.Should().HaveCount(amount);
            products.Should().Contain(_productSeed);
        }

    }

}
