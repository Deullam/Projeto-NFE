using FluentAssertions;
using Moq;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceItems;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Infra.Data.Tests.Initializer;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.InvoiceItems
{

    [TestFixture]
    public class InvoiceItemRepositoryTests : EffortTestBase
	{
		//private IInvoiceItemRepository _repository;
		//private TaxProduct _taxproduct;
		//private Product _product;
		//private Mock<Invoice> _fakeInvoice;

		//[SetUp]
		//public void Initialize()
		//{
		//	base.Initialize();

		//	_repository = new InvoiceItemRepository(_contexto);

		//	_taxproduct = ObjectMother.GetValidTaxProduct();
		//	_product = ObjectMother.GetExistentValidProduct(_taxproduct);
		//	_fakeInvoice = new Mock<Invoice>();
		//}

		//[Test]
		//public void Test_InvoiceItemRepository_Add_ShouldBeOk()
		//{
		//	var invoiceItemRegistered = _repository.Add(_invoiceItemSeed);

		//	invoiceItemRegistered.Should().NotBeNull();
		//	invoiceItemRegistered.Should().Be(_invoiceItemSeed);

		//}

		//[Test]
		//public void Test_InvoiceItemRepository_Add_Invalid_ShouldThrowException()
		//{
		//	long existentId = 2;
		//	_fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		//	InvoiceItem invoiceItem = ObjectMother.GetInvalidInvoiceItemWithUninformedAmount(_fakeInvoice.Object, _product);
		//	Action action = () => _repository.Add(invoiceItem);
		//	action.Should().Throw<InvoiceItemUninformedAmountException>();
		//}

		////[Test]
		////public void Test_InvoiceItemRepository_Update_ShouldBeOk()
		////{
		////    long existentId = 2;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetExistentInvoinceItemOk(_fakeInvoice.Object, _product);


		////    invoiceItem = _repository.Update(invoiceItem);

		////    InvoiceItem result = _repository.Get(invoiceItem.Id);
		////    result.Should().NotBeNull();
		////    result.Amount.Should().Be(invoiceItem.Amount);
		////    result.Code.Should().BeNullOrEmpty();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Update_Invalid_ShouldThrowException()
		////{
		////    long existentId = 2;
		////    long existentInvoiceItemId = 1;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetInvalidInvoiceItemWithUninformedAmount(_fakeInvoice.Object, _product);
		////    invoiceItem.Id = existentInvoiceItemId;

		////    Action action = () => _repository.Update(invoiceItem);

		////    action.Should().Throw<InvoiceItemUninformedAmountException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Update_InvalidId_ShouldThrowException()
		////{
		////    long existentId = 2;
		////    long invalidId = 0;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetExistentInvoinceItemOk(_fakeInvoice.Object, _product);
		////    invoiceItem.Id = invalidId;

		////    Action action = () => _repository.Update(invoiceItem);

		////    action.Should().Throw<IdentifierUndefinedException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Delete_ShouldBeOk()
		////{
		////    long existentId = 2;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetExistentInvoinceItemOk(_fakeInvoice.Object, _product);
		////    _repository.Delete(invoiceItem);
		////    var result = _repository.Get(invoiceItem.Id);
		////    result.Should().BeNull();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Delete_InvalidId_ShouldBeThrowException()
		////{
		////    long existentId = 2;
		////    long invalidId = 0;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetExistentInvoinceItemOk(_fakeInvoice.Object, _product);
		////    invoiceItem.Id = invalidId;
		////    Action action = () => _repository.Delete(invoiceItem);

		////    action.Should().Throw<IdentifierUndefinedException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Save_ConsolidatedInvoiceItem_ShouldBeOk()
		////{
		////    long existentId = 2;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetNewConsolidatedInvoiceItem(_fakeInvoice.Object, _product);
		////    invoiceItem = _repository.Save(invoiceItem);
		////    invoiceItem.Id.Should().BeGreaterThan(0);
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Save_InvalidConsolidatedInvoiceItem_ShouldThrowException()
		////{
		////    long existentId = 2;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetInvalidConsolidatedInvoiceItemWithUninformedAmount(_fakeInvoice.Object, _product);
		////    Action action = () => _repository.Save(invoiceItem);
		////    action.Should().Throw<InvoiceItemUninformedAmountException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Update_ConsolidatedInvoiceItem_ShouldBeOk()
		////{
		////    long existentId = 2;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetExistentConsolidatedInvoiceItem(_fakeInvoice.Object, _product);

		////    invoiceItem = _repository.Update(invoiceItem);

		////    InvoiceItem result = _repository.Get(invoiceItem.Id);
		////    result.Should().NotBeNull();
		////    result.Amount.Should().Be(invoiceItem.Amount);
		////    result.Code.Should().Be(invoiceItem.Code);
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Update_ConsolidatedInvoiceItemWithInvalidId_ShouldThrowException()
		////{
		////    long existentId = 2;
		////    long invalidId = 0;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetExistentConsolidatedInvoiceItem(_fakeInvoice.Object, _product);
		////    invoiceItem.Id = invalidId;

		////    Action action = () => _repository.Update(invoiceItem);

		////    action.Should().Throw<IdentifierUndefinedException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Update_InvalidConsolidatedInvoiceItem_ShouldThrowException()
		////{
		////    long existentId = 2;
		////    long existentInvoiceItemId = 1;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetInvalidConsolidatedInvoiceItemWithUninformedAmount(_fakeInvoice.Object, _product);
		////    invoiceItem.Id = existentInvoiceItemId;

		////    Action action = () => _repository.Update(invoiceItem);

		////    action.Should().Throw<InvoiceItemUninformedAmountException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Delete_ConsolidatedInvoiceItem_ShouldBeOk()
		////{
		////    long existentId = 2;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetExistentConsolidatedInvoiceItem(_fakeInvoice.Object, _product);
		////    _repository.Delete(invoiceItem);
		////    var result = _repository.Get(invoiceItem.Id);
		////    result.Should().BeNull();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Delete_ConsolidatedInvoiceItemWithInvalidId_ShouldBeOk()
		////{
		////    long existentId = 2;
		////    long invalidId = 0;
		////    _fakeInvoice.Setup(invoice => invoice.Id).Returns(existentId);
		////    InvoiceItem invoiceItem = ObjectMother.GetExistentConsolidatedInvoiceItem(_fakeInvoice.Object, _product);
		////    invoiceItem.Id = invalidId;
		////    Action action = () => _repository.Delete(invoiceItem);

		////    action.Should().Throw<IdentifierUndefinedException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Get_ShouldBeOk()
		////{
		////    long existentId = 1;
		////    InvoiceItem invoiceItem = _repository.Get(existentId);
		////    invoiceItem.Should().NotBeNull();
		////    invoiceItem.Product.Code.Should().NotBeNullOrEmpty();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_GetByInvoice_ShouldBeOk()
		////{
		////    int expectedAmount = 1;
		////    long invoiceId = 1;
		////    IList<InvoiceItem> invoiceItens = _repository.GetByInvoice(invoiceId);
		////    invoiceItens.Should().NotBeNull();
		////    invoiceItens.Count.Should().Be(expectedAmount);
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_GetByInvoice_IdInvalid_ShouldThrowException()
		////{
		////    long invalidId = -1;
		////    Action action = () => _repository.GetByInvoice(invalidId);
		////    action.Should().Throw<IdentifierUndefinedException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_GetByInvoice_NonexistentId_ShouldBeOk()
		////{
		////    long nonexistentId = 1000;
		////    IList<InvoiceItem> result = _repository.GetByInvoice(nonexistentId);

		////    result.Should().NotBeNull();
		////    result.Count.Should().Be(0);
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_GetByProduct_ShouldBeOk()
		////{
		////    int expectedAmount = 1;
		////    long invoiceId = 1;
		////    IList<InvoiceItem> invoiceItens = _repository.GetByProduct(invoiceId);
		////    invoiceItens.Should().NotBeNull();
		////    invoiceItens.Count.Should().Be(expectedAmount);
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_GetByProduct_IdInvalid_ShouldThrowException()
		////{
		////    long invalidId = -1;
		////    Action action = () => _repository.GetByProduct(invalidId);
		////    action.Should().Throw<IdentifierUndefinedException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_GetByProduct_NonexistentId_ShouldBeOk()
		////{
		////    long nonexistentId = 1000;
		////    IList<InvoiceItem> result = _repository.GetByProduct(nonexistentId);

		////    result.Should().NotBeNull();
		////    result.Count.Should().Be(0);
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Get_IdInvalid_ShouldThrowException()
		////{
		////    long invalidId = -1;
		////    Action action = () => _repository.Get(invalidId);
		////    action.Should().Throw<IdentifierUndefinedException>();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_Get_NonexistentId_ShouldBeOk()
		////{
		////    long nonexistentId = 100;
		////    InvoiceItem invoiceItem = _repository.Get(nonexistentId);
		////    invoiceItem.Should().BeNull();
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_GetAll_ShouldBeOk()
		////{
		////    int expectedCount = 1;
		////    IList<InvoiceItem> invoiceItens = _repository.GetAll();
		////    invoiceItens.Should().NotBeNullOrEmpty();
		////    invoiceItens.Count.Should().Be(expectedCount);
		////}

		////[Test]
		////public void Test_InvoiceItemRepository_GetAll_EmptyList_ShouldBeOk()
		////{
		////    BaseSqlTest.ResetDatabase();
		////    int expectedCount = 0;
		////    IList<InvoiceItem> invoiceItens = _repository.GetAll();
		////    invoiceItens.Should().BeNullOrEmpty();
		////    invoiceItens.Count.Should().Be(expectedCount);
		////}
	}
}
