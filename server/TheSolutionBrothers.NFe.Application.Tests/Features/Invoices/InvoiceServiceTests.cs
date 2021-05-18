using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Application.Features.Invoices;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
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
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using AutoMapper;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Commands;
using TheSolutionBrothers.NFe.Application.Features.Invoices.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Queries;

namespace TheSolutionBrothers.NFe.Application.Tests.Features.Invoices
{
    [TestFixture]
    public class InvoiceServiceTests
    {
        private Mock<IReceiverRepository> _mockRepositoryReceiver;
        private Mock<ICarrierRepository> _mockRepositoryCarrier;
        private Mock<ISenderRepository> _mockRepositorySender;
        private Mock<IProductRepository> _mockRepositoryProduct;
        private Mock<IInvoiceRepository> _mockRepositoryInvoice;
        private Mock<IInvoiceItemRepository> _mockRepositoryInvoiceItem;
        private Mock<IMapper> _mockIMapper;

        private Invoice _mappedInvoice;
        private Invoice _invoiceSaved;
        private Product _product;

        private InvoiceService _invoiceService;
        private CPF _validCpf;
        private CNPJ _validCnpj;

        private Address _address;
        private Receiver _legalReceiver;
        private Receiver _physicalReceiver;

        private Carrier _legalCarrier;
        private Carrier _physicalCarrier;

        private Sender _sender;

        private InvoiceItem _invoiceItem;

        private InvoiceItemRegisterCommand invoiceItemRegisterCommand;
        private InvoiceItemUpdateCommand invoiceItemUpdateCommand;

        private InvoiceRegisterCommand _invoiceRegisterCommand;
        private InvoiceUpdateCommand _invoiceUpdateCommand;

        [SetUp]
        public void Initialize()
        {
            _mockRepositoryReceiver = new Mock<IReceiverRepository>();
            _mockRepositoryCarrier = new Mock<ICarrierRepository>();
            _mockRepositorySender = new Mock<ISenderRepository>();
            _mockRepositoryProduct = new Mock<IProductRepository>();
            _mockRepositoryInvoice = new Mock<IInvoiceRepository>();
            _mockRepositoryInvoiceItem = new Mock<IInvoiceItemRepository>();
            _mockIMapper = new Mock<IMapper>();

            _invoiceService = new InvoiceService(_mockRepositoryInvoice.Object, _mockRepositoryCarrier.Object, _mockRepositoryReceiver.Object, _mockRepositorySender.Object, _mockRepositoryProduct.Object, _mockRepositoryInvoiceItem.Object, _mockIMapper.Object);

            _validCpf = ObjectMother.GetValidCPF();
            _validCnpj = ObjectMother.GetValidCNPJ();
            _address = ObjectMother.GetExistentValidAddress();

            _legalReceiver = ObjectMother.GetExistentValidLegalReceiver(_address, _validCnpj);
            _physicalReceiver = ObjectMother.GetExistentValidPhysicalReceiver(_address, _validCpf);

            _legalCarrier = ObjectMother.GetExistentValidCarrierLegal(_address, _validCnpj);
            _physicalCarrier = ObjectMother.GetExistentValidCarrierPhysical(_address, _validCpf);

            _sender = ObjectMother.GetExistentValidSender(_address, _validCnpj);

            _product = ObjectMother.GetExistentValidProduct(new TaxProduct());

            _mappedInvoice = ObjectMother.GetNewValidOpenedInvoice(_sender, _physicalReceiver, _physicalCarrier, null);
            _invoiceSaved = ObjectMother.GetExistentValidOpenedInvoice(_sender, _physicalReceiver, _physicalCarrier,null);
            _invoiceItem = ObjectMother.GetNewInvoiceItemOk(_mappedInvoice, _product);
            _mappedInvoice.InvoiceItems = new List<InvoiceItem>() { _invoiceItem };
            _invoiceSaved.InvoiceItems = new List<InvoiceItem>() { _invoiceItem };

            invoiceItemRegisterCommand = ObjectMother.GetValidInvoiceItemRegisterCommand();
            invoiceItemUpdateCommand = ObjectMother.GetValidInvoiceItemUpdateCommand();

            _invoiceRegisterCommand = ObjectMother.GetValidInvoiceRegisterCommand(new List<InvoiceItemRegisterCommand>() { invoiceItemRegisterCommand });
            _invoiceUpdateCommand = ObjectMother.GetValidInvoiceUpdateCommand();
        }

        [Test]
        public void Test_InvoiceService_Add_ShouldBeOK()
        {
            long expectedId = 1;

            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceRegisterCommand)).Returns(_mappedInvoice);
            _mockRepositoryInvoice.Setup(r => r.Add(_mappedInvoice)).Returns(_invoiceSaved);
            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceRegisterCommand.CarrierId)).Returns(_invoiceSaved.Carrier);
            _mockRepositoryReceiver.Setup(ri => ri.GetById(_invoiceRegisterCommand.ReceiverId)).Returns(_invoiceSaved.Receiver);
            _mockRepositorySender.Setup(ri => ri.GetById(_invoiceRegisterCommand.SenderId)).Returns(_invoiceSaved.Sender);
            _mockRepositoryProduct.Setup(p => p.GetById(It.IsAny<long>())).Returns(_product);

            long result = _invoiceService.Add(_invoiceRegisterCommand);

            _mockIMapper.Verify(m => m.Map<Invoice>(_invoiceRegisterCommand));
            _mockRepositoryInvoice.Verify(r => r.Add(_mappedInvoice));
            result.Should().Be(expectedId);
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceRegisterCommand.CarrierId));
            _mockRepositoryReceiver.Verify(c => c.GetById(_invoiceRegisterCommand.ReceiverId));
            _mockRepositorySender.Verify(s => s.GetById(_invoiceRegisterCommand.SenderId));
            _mockRepositoryProduct.Verify(p => p.GetById(It.IsAny<long>()));
        }

        [Test]
        public void Test_InvoiceService_Add_InvalidInvoice_ShouldThrowException()
        {
            Invoice invalidInvoice = ObjectMother.GetInvalidOpenedInvoiceWithUniformedNatureOperation(_sender, _legalReceiver, _legalCarrier, new List<InvoiceItem>() { _invoiceItem });
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceRegisterCommand)).Returns(invalidInvoice);
            _mockRepositoryInvoice.Setup(r => r.Add(invalidInvoice));

            _mockRepositoryReceiver.Setup(r => r.GetById(_invoiceRegisterCommand.ReceiverId)).Returns(_legalReceiver);
            _mockRepositoryCarrier.Setup(r => r.GetById(_invoiceRegisterCommand.CarrierId)).Returns(_legalCarrier);
            _mockRepositorySender.Setup(r => r.GetById(_invoiceRegisterCommand.SenderId)).Returns(_sender);
            _mockRepositoryProduct.Setup(r => r.GetById(It.IsAny<long>())).Returns(_product);

            Action action = () => _invoiceService.Add(_invoiceRegisterCommand);

            action.Should().Throw<InvoiceUninformedNatureOperationException>();
            _mockRepositoryReceiver.Verify(c => c.GetById(_invoiceRegisterCommand.ReceiverId));
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceRegisterCommand.CarrierId));
            _mockRepositorySender.Verify(c => c.GetById(_invoiceRegisterCommand.SenderId));
            _mockRepositorySender.Verify(c => c.GetById(It.IsAny<long>()));
        }

        [Test]
        public void Test_InvoiceService_Add_InvoiceWithExistentNumber_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceRegisterCommand)).Returns(_mappedInvoice);
            _mockRepositoryInvoice.Setup(r => r.GetByNumber(_mappedInvoice.Number)).Returns(new Invoice());

            Action action = () => _invoiceService.Add(_invoiceRegisterCommand);

            action.Should().Throw<InvoiceExistentNumberException>();
            _mockRepositoryInvoice.Verify(i => i.GetByNumber(_mappedInvoice.Number));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Add_IssuedInvoice_ShouldThrowException()
        {
            _invoiceRegisterCommand.Status = InvoiceStatus.ISSUED;
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceRegisterCommand)).Returns(_mappedInvoice);

            Action action = () => _invoiceService.Add(_invoiceRegisterCommand);

            action.Should().Throw<InvoiceSaveIssuedInvoiceException>();
            _mockRepositoryInvoice.VerifyNoOtherCalls();
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }
        
        [Test]
        public void Test_InvoiceService_Add_CarrierNotFound_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceRegisterCommand)).Returns(_mappedInvoice);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id)).Returns(_invoiceSaved);

            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceRegisterCommand.CarrierId));

            Action action = () => _invoiceService.Add(_invoiceRegisterCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceRegisterCommand.CarrierId));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Add_ReceiverNotFound_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceRegisterCommand)).Returns(_mappedInvoice);
            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceRegisterCommand.CarrierId)).Returns(_invoiceSaved.Carrier);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id)).Returns(_invoiceSaved);

            _mockRepositoryReceiver.Setup(ri => ri.GetById(_invoiceRegisterCommand.ReceiverId));

            Action action = () => _invoiceService.Add(_invoiceRegisterCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceRegisterCommand.CarrierId));
            _mockRepositoryReceiver.Verify(c => c.GetById(_invoiceRegisterCommand.ReceiverId));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Add_SenderNotFound_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceRegisterCommand)).Returns(_mappedInvoice);
            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceRegisterCommand.CarrierId)).Returns(_invoiceSaved.Carrier);
            _mockRepositoryReceiver.Setup(ri => ri.GetById(_invoiceRegisterCommand.ReceiverId)).Returns(_invoiceSaved.Receiver);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id)).Returns(_invoiceSaved);

            _mockRepositorySender.Setup(ri => ri.GetById(_invoiceRegisterCommand.SenderId));

            Action action = () => _invoiceService.Add(_invoiceRegisterCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceRegisterCommand.CarrierId));
            _mockRepositoryReceiver.Verify(c => c.GetById(_invoiceRegisterCommand.ReceiverId));
            _mockRepositorySender.Verify(s => s.GetById(_invoiceRegisterCommand.SenderId));
            _mockRepositoryProduct.VerifyNoOtherCalls();
        }


        [Test]
        public void Test_InvoiceService_Add_ProductNotFound_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceRegisterCommand)).Returns(_mappedInvoice);
            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceRegisterCommand.CarrierId)).Returns(_invoiceSaved.Carrier);
            _mockRepositoryReceiver.Setup(ri => ri.GetById(_invoiceRegisterCommand.ReceiverId)).Returns(_invoiceSaved.Receiver);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id)).Returns(_invoiceSaved);
            _mockRepositorySender.Setup(r => r.GetById(_invoiceRegisterCommand.SenderId)).Returns(_sender);

            _mockRepositoryProduct.Setup(r => r.GetById(It.IsAny<long>()));
            
            Action action = () => _invoiceService.Add(_invoiceRegisterCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceRegisterCommand.CarrierId));
            _mockRepositoryReceiver.Verify(c => c.GetById(_invoiceRegisterCommand.ReceiverId));
            _mockRepositorySender.Verify(s => s.GetById(_invoiceRegisterCommand.SenderId));
            _mockRepositoryProduct.Verify(r => r.GetById(It.IsAny<long>()));
        }

        [Test]
        public void Test_InvoiceService_Update_ShouldBeOK()
        {
            var exitentInvoiceItem = ObjectMother.GetExistentConsolidatedInvoiceItem(_mappedInvoice, _product);
            _mappedInvoice.InvoiceItems.Add(exitentInvoiceItem);
            _invoiceSaved.InvoiceItems.Add(exitentInvoiceItem);
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceUpdateCommand)).Returns(_mappedInvoice);
            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceUpdateCommand.CarrierId)).Returns(_invoiceSaved.Carrier);
            _mockRepositoryReceiver.Setup(ri => ri.GetById(_invoiceUpdateCommand.ReceiverId)).Returns(_invoiceSaved.Receiver);
            _mockRepositorySender.Setup(ri => ri.GetById(_invoiceUpdateCommand.SenderId)).Returns(_invoiceSaved.Sender);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id)).Returns(_invoiceSaved);


            _invoiceService.Update(_invoiceUpdateCommand);

            _mockRepositoryInvoice.Verify(i => i.GetById(_mappedInvoice.Id));
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceUpdateCommand.CarrierId));
            _mockRepositoryReceiver.Verify(c => c.GetById(_invoiceUpdateCommand.ReceiverId));
            _mockRepositorySender.Verify(s => s.GetById(_invoiceUpdateCommand.SenderId));
        }
        
        [Test]
        public void Test_InvoiceService_Update_InvoiceNotFound_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceUpdateCommand)).Returns(_mappedInvoice);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id));


            Action action = () => _invoiceService.Update(_invoiceUpdateCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryInvoice.Verify(i => i.GetById(_mappedInvoice.Id));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Update_CarrierNotFound_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceUpdateCommand)).Returns(_mappedInvoice);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id)).Returns(_invoiceSaved);

            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceUpdateCommand.CarrierId));

            Action action = () => _invoiceService.Update(_invoiceUpdateCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryInvoice.Verify(i => i.GetById(_mappedInvoice.Id));
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceUpdateCommand.CarrierId));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Update_ReceiverNotFound_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceUpdateCommand)).Returns(_mappedInvoice);
            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceUpdateCommand.CarrierId)).Returns(_invoiceSaved.Carrier);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id)).Returns(_invoiceSaved);

            _mockRepositoryReceiver.Setup(ri => ri.GetById(_invoiceUpdateCommand.ReceiverId));
            
            Action action = () => _invoiceService.Update(_invoiceUpdateCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryInvoice.Verify(i => i.GetById(_mappedInvoice.Id));
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceUpdateCommand.CarrierId));
            _mockRepositoryReceiver.Verify(c => c.GetById(_invoiceUpdateCommand.ReceiverId));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Update_SenderNotFound_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceUpdateCommand)).Returns(_mappedInvoice);
            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceUpdateCommand.CarrierId)).Returns(_invoiceSaved.Carrier);
            _mockRepositoryReceiver.Setup(ri => ri.GetById(_invoiceUpdateCommand.ReceiverId)).Returns(_invoiceSaved.Receiver);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id)).Returns(_invoiceSaved);

            _mockRepositorySender.Setup(ri => ri.GetById(_invoiceUpdateCommand.SenderId));

            Action action = () => _invoiceService.Update(_invoiceUpdateCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryInvoice.Verify(i => i.GetById(_mappedInvoice.Id));
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceUpdateCommand.CarrierId));
            _mockRepositoryReceiver.Verify(c => c.GetById(_invoiceUpdateCommand.ReceiverId));
            _mockRepositorySender.Verify(s => s.GetById(_invoiceUpdateCommand.SenderId));
            _mockRepositoryProduct.VerifyNoOtherCalls();
        }
        
        [Test]
        public void Test_InvoiceService_Update_InvoiceWitExistentNumber_ShouldThrowException()
        {
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceUpdateCommand)).Returns(_mappedInvoice);
            _mockRepositoryInvoice.Setup(ri => ri.GetByNumber(_mappedInvoice.Number)).Returns(new Invoice { Id = 100, Number = _mappedInvoice.Number });
            Action action = () => _invoiceService.Update(_invoiceUpdateCommand);

            action.Should().Throw<InvoiceExistentNumberException>();
            _mockRepositoryInvoice.Verify(ri => ri.GetByNumber(_mappedInvoice.Number));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Update_IssuedInvoice_ShouldThrowException()
        {
            _invoiceUpdateCommand.Status = InvoiceStatus.ISSUED;
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceUpdateCommand)).Returns(_mappedInvoice);

            Action action = () => _invoiceService.Update(_invoiceUpdateCommand);
            action.Should().Throw<InvoiceUpdateIssuedInvoiceException>();
            _mockRepositoryInvoice.VerifyNoOtherCalls();
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Update_InvalidInvoice_ShouldThrowException()
        {
            _mappedInvoice.NatureOperation = "";
            _mockIMapper.Setup(m => m.Map<Invoice>(_invoiceUpdateCommand)).Returns(_mappedInvoice);
            _mockRepositoryCarrier.Setup(ri => ri.GetById(_invoiceUpdateCommand.CarrierId)).Returns(_invoiceSaved.Carrier);
            _mockRepositoryReceiver.Setup(ri => ri.GetById(_invoiceUpdateCommand.ReceiverId)).Returns(_invoiceSaved.Receiver);
            _mockRepositorySender.Setup(ri => ri.GetById(_invoiceUpdateCommand.SenderId)).Returns(_invoiceSaved.Sender);
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_mappedInvoice.Id)).Returns(_invoiceSaved);


            Action action = () => _invoiceService.Update(_invoiceUpdateCommand);

            action.Should().Throw<InvoiceUninformedNatureOperationException>();
            _mockRepositoryInvoice.Verify(i => i.GetById(_mappedInvoice.Id));
            _mockRepositoryCarrier.Verify(c => c.GetById(_invoiceUpdateCommand.CarrierId));
            _mockRepositoryReceiver.Verify(c => c.GetById(_invoiceUpdateCommand.ReceiverId));
            _mockRepositorySender.Verify(s => s.GetById(_invoiceUpdateCommand.SenderId));
        }

        [Test]
        public void Test_InvoiceService_Get_ShouldBeOk()
        {
            long existentId = 1;

            Mock<ReceiverViewModel> receiverViewModel = new Mock<ReceiverViewModel>();
            Mock<CarrierViewModel> carrierViewModel = new Mock<CarrierViewModel>();
            Mock<SenderViewModel> senderViewModel = new Mock<SenderViewModel>();
            Mock<InvoiceTaxViewModel> invoiceTaxViewModel = new Mock<InvoiceTaxViewModel>();
            Mock<InvoiceItemViewModel> invoiceItemViewModel = new Mock<InvoiceItemViewModel>();
            InvoiceViewModel invoiceViewModel = ObjectMother.GetInvoiceViewModel(senderViewModel.Object,receiverViewModel.Object,carrierViewModel.Object, invoiceTaxViewModel.Object,new List<InvoiceItemViewModel>() { invoiceItemViewModel.Object });

            _mockRepositoryInvoice.Setup(m => m.GetById(It.IsAny<long>())).Returns(_invoiceSaved);
            _mockIMapper.Setup(m => m.Map<InvoiceViewModel>(It.IsAny<Invoice>())).Returns(invoiceViewModel);

            InvoiceViewModel result = _invoiceService.GetById(existentId);

            result.Id.Should().Be(existentId);
            _mockRepositoryInvoice.Verify(rp => rp.GetById(existentId));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_invoiceService_Get_NonexistentId_ShouldBeThrowException()
        {
            long unexistentId = 100;
            _mockRepositoryReceiver.Setup(m => m.GetById(unexistentId));

            InvoiceViewModel result = _invoiceService.GetById(unexistentId);

            result.Should().BeNull();
            _mockRepositoryInvoice.Verify(rp => rp.GetById(unexistentId));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_GetAll_Query_ShouldBeOk()
        {
            int amount = 1;

            InvoiceGetAllQuery query = ObjectMother.GetValidInvoiceGetAllQuery();
            var repositoryMockValue = new List<Invoice>() { _invoiceSaved }.AsQueryable();
            _mockRepositoryInvoice.Setup(m => m.GetAll(query.Size)).Returns(repositoryMockValue);

            var receivers = _invoiceService.GetAll(query);

            _mockRepositoryInvoice.Verify(x => x.GetAll(query.Size));
            receivers.Should().NotBeNull();
            receivers.Should().HaveCount(amount);
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_GetAll_ShouldBeOk()
        {
            int amount = 1;

            var repositoryMockValue = new List<Invoice>() { _invoiceSaved }.AsQueryable();
            _mockRepositoryInvoice.Setup(m => m.GetAll()).Returns(repositoryMockValue);

            var customers = _invoiceService.GetAll();

            _mockRepositoryInvoice.Verify(x => x.GetAll());
            customers.Should().NotBeNull();
            customers.Should().HaveCount(amount);
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Delete_False_ShouldOk()
        {
            var deleteCommand = ObjectMother.GetInvoiceDeleteCommand();

            _mockRepositoryInvoice.Setup(m => m.Remove(It.IsAny<long>()));
            _mockRepositoryInvoice.Setup(m => m.GetById(It.IsAny<long>())).Returns(_invoiceSaved);

            var result = _invoiceService.Remove(deleteCommand);

            result.Should().BeFalse();
            _mockRepositoryInvoice.Verify(rp => rp.Remove(It.IsAny<long>()));
            _mockRepositoryInvoice.Verify(rp => rp.GetById(It.IsAny<long>()));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Delete_True_ShouldBeOk()
        {
            var isAllUpdated = true;

            var deleteCommand = ObjectMother.GetInvoiceDeleteCommand();

            _mockRepositoryInvoice.Setup(r => r.GetById(It.IsAny<long>())).Returns(_invoiceSaved);
            _mockRepositoryInvoice.Setup(m => m.Remove(It.IsAny<long>())).Returns(isAllUpdated);

            var result = _invoiceService.Remove(deleteCommand);

            result.Should().BeTrue();
            _mockRepositoryInvoice.Verify(rp => rp.GetById(It.IsAny<long>()));
            _mockRepositoryInvoice.Verify(rp => rp.Remove(It.IsAny<long>()));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Delete_IssuedInvoice_ShouldBeThrowException()
        {
            var deleteCommand = ObjectMother.GetInvoiceDeleteCommand();

            _invoiceSaved.Status = InvoiceStatus.ISSUED;
            _mockRepositoryInvoice.Setup(ri => ri.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);

            Action action = () => _invoiceService.Remove(deleteCommand);

            action.Should().Throw<InvoiceDeleteIssuedInvoiceException>();
            _mockRepositoryInvoice.Verify(ri => ri.GetById(_invoiceSaved.Id));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_Delete_NonexistentInvoice_ShouldBeThrowException()
        {
            var deleteCommand = ObjectMother.GetInvoiceDeleteCommand();
            
            _mockRepositoryInvoice.Setup(ri => ri.GetById(It.IsAny<long>()));

            Action action = () => _invoiceService.Remove(deleteCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryInvoice.Verify(ri => ri.GetById(It.IsAny<long>()));
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryCarrier.VerifyNoOtherCalls();
            _mockRepositorySender.VerifyNoOtherCalls();
            _mockRepositoryReceiver.VerifyNoOtherCalls();
        }
        
        [Test]
        public void Test_InvoiceService_AddItems_ShouldBeOK()
        {
            InvoiceItem newInvoiceItem = ObjectMother.GetNewValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemRegisterCommand registerCommand = ObjectMother.GetValidInvoiceItemRegisterCommand();

            _mockIMapper.Setup(m => m.Map<InvoiceItem>(registerCommand)).Returns(newInvoiceItem);
            _mockRepositoryInvoiceItem.Setup(it => it.Add(newInvoiceItem)).Returns(savedInvoiceItem);
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);
            _mockRepositoryProduct.Setup(p => p.GetById(It.IsAny<long>())).Returns(_product);

            List<long> result = _invoiceService.AddItems(_invoiceSaved.Id, new List<InvoiceItemRegisterCommand>() { registerCommand });

            _mockIMapper.Verify(m => m.Map<InvoiceItem>(registerCommand));
            _mockRepositoryInvoiceItem.Verify(it => it.Add(newInvoiceItem));
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockRepositoryProduct.Verify(p => p.GetById(It.IsAny<long>()));
            result.Should().HaveCountGreaterThan(0);
        }

        [Test]
        public void Test_InvoiceService_AddItems_NonexistentInvoice_ShouldThrowException()
        {
            InvoiceItem newInvoiceItem = ObjectMother.GetNewValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemRegisterCommand registerCommand = ObjectMother.GetValidInvoiceItemRegisterCommand();

            _mockIMapper.Setup(m => m.Map<InvoiceItem>(registerCommand)).Returns(newInvoiceItem);
            _mockRepositoryInvoiceItem.Setup(it => it.Add(newInvoiceItem)).Returns(savedInvoiceItem);
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id));
            _mockRepositoryProduct.Setup(p => p.GetById(It.IsAny<long>())).Returns(_product);


            Action action = () => _invoiceService.AddItems(_invoiceSaved.Id, new List<InvoiceItemRegisterCommand>() { registerCommand });

            action.Should().Throw<NotFoundException>();
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockIMapper.VerifyNoOtherCalls();
            _mockRepositoryInvoiceItem.VerifyNoOtherCalls();
            _mockRepositoryProduct.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_AddItems_IssuedInvoice_ShouldThrowException()
        {
            InvoiceItem newInvoiceItem = ObjectMother.GetNewValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemRegisterCommand registerCommand = ObjectMother.GetValidInvoiceItemRegisterCommand();

            _mockIMapper.Setup(m => m.Map<InvoiceItem>(registerCommand)).Returns(newInvoiceItem);
            _mockRepositoryInvoiceItem.Setup(it => it.Add(newInvoiceItem)).Returns(savedInvoiceItem);
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);
            _mockRepositoryProduct.Setup(p => p.GetById(It.IsAny<long>())).Returns(_product);

            _invoiceSaved.Status = InvoiceStatus.ISSUED;

            Action action = () => _invoiceService.AddItems(_invoiceSaved.Id, new List<InvoiceItemRegisterCommand>() { registerCommand });

            action.Should().Throw<InvoiceSaveIssuedInvoiceException>();
            _mockRepositoryInvoice.Verify(c => c.GetById(_invoiceRegisterCommand.CarrierId));
            _mockIMapper.VerifyNoOtherCalls();
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockRepositoryInvoiceItem.VerifyNoOtherCalls();
            _mockRepositoryProduct.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_AddItems_ProductNotFound_ShouldThrowException()
        {
            InvoiceItem newInvoiceItem = ObjectMother.GetNewValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemRegisterCommand registerCommand = ObjectMother.GetValidInvoiceItemRegisterCommand();

            _mockIMapper.Setup(m => m.Map<InvoiceItem>(registerCommand)).Returns(newInvoiceItem);
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);
            _mockRepositoryProduct.Setup(p => p.GetById(It.IsAny<long>()));
            
            Action action = () => _invoiceService.AddItems(_invoiceSaved.Id, new List<InvoiceItemRegisterCommand>() { registerCommand });

            action.Should().Throw<NotFoundException>();
            _mockRepositoryInvoice.Verify(c => c.GetById(_invoiceRegisterCommand.CarrierId));
            _mockIMapper.Verify(m => m.Map<InvoiceItem>(registerCommand));
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockRepositoryInvoiceItem.VerifyNoOtherCalls();
            _mockRepositoryProduct.Verify(p => p.GetById(It.IsAny<long>()));
        }
        
        [Test]
        public void Test_InvoiceService_UpdateItems_ShouldBeOK()
        {
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemUpdateCommand updateCommand = ObjectMother.GetValidInvoiceItemUpdateCommand();

            bool success = true;

            _mockIMapper.Setup(m => m.Map<InvoiceItem>(updateCommand)).Returns(savedInvoiceItem);
            _mockRepositoryInvoiceItem.Setup(it => it.Update(savedInvoiceItem)).Returns(success);
            _mockRepositoryInvoiceItem.Setup(r => r.GetById(updateCommand.Id)).Returns(savedInvoiceItem);
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);
            _mockRepositoryProduct.Setup(p => p.GetById(It.IsAny<long>())).Returns(_product);

            bool result = _invoiceService.UpdateItems(_invoiceSaved.Id, new List<InvoiceItemUpdateCommand>() { updateCommand });

            _mockIMapper.Verify(m => m.Map<InvoiceItem>(updateCommand));
            _mockRepositoryInvoiceItem.Verify(it => it.Update(savedInvoiceItem));
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockRepositoryProduct.Verify(p => p.GetById(It.IsAny<long>()));
            result.Should().BeTrue();
        }

        [Test]
        public void Test_InvoiceService_UpdateItems_NonexistentInvoice_ShouldThrowException()
        {
            InvoiceItem newInvoiceItem = ObjectMother.GetNewValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemUpdateCommand updateCommand = ObjectMother.GetValidInvoiceItemUpdateCommand();

            _mockIMapper.Setup(m => m.Map<InvoiceItem>(updateCommand)).Returns(newInvoiceItem);
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id));
            
            Action action = () => _invoiceService.UpdateItems(_invoiceSaved.Id, new List<InvoiceItemUpdateCommand>() { updateCommand });

            action.Should().Throw<NotFoundException>();
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockIMapper.VerifyNoOtherCalls();
            _mockRepositoryInvoiceItem.VerifyNoOtherCalls();
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryInvoiceItem.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_UpdateItems_IssuedInvoice_ShouldThrowException()
        {
            InvoiceItem newInvoiceItem = ObjectMother.GetNewValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemUpdateCommand updateCommand = ObjectMother.GetValidInvoiceItemUpdateCommand();
            
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);

            _invoiceSaved.Status = InvoiceStatus.ISSUED;

            Action action = () => _invoiceService.UpdateItems(_invoiceSaved.Id, new List<InvoiceItemUpdateCommand>() { updateCommand });

            action.Should().Throw<InvoiceSaveIssuedInvoiceException>();
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockIMapper.VerifyNoOtherCalls();
            _mockRepositoryProduct.VerifyNoOtherCalls();
            _mockRepositoryInvoiceItem.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_UpdateItems_ProductNotFound_ShouldThrowException()
        {
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemUpdateCommand updateCommand = ObjectMother.GetValidInvoiceItemUpdateCommand();

            _mockIMapper.Setup(m => m.Map<InvoiceItem>(updateCommand)).Returns(savedInvoiceItem);
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);
            _mockRepositoryProduct.Setup(p => p.GetById(It.IsAny<long>()));

            Action action = () => _invoiceService.UpdateItems(_invoiceSaved.Id, new List<InvoiceItemUpdateCommand>() { updateCommand });

            action.Should().Throw<NotFoundException>();
            _mockIMapper.Verify(m => m.Map<InvoiceItem>(updateCommand));
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockRepositoryProduct.Verify(p => p.GetById(It.IsAny<long>()));
            _mockRepositoryInvoiceItem.VerifyNoOtherCalls();
        }


        [Test]
        public void Test_InvoiceService_UpdateItems_InvoiceItemNotFound_ShouldThrowException()
        {
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemUpdateCommand updateCommand = ObjectMother.GetValidInvoiceItemUpdateCommand();

            _mockIMapper.Setup(m => m.Map<InvoiceItem>(updateCommand)).Returns(savedInvoiceItem);
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);
            _mockRepositoryInvoiceItem.Setup(r => r.GetById(updateCommand.Id));
            _mockRepositoryProduct.Setup(p => p.GetById(It.IsAny<long>())).Returns(_product);

            Action action = () => _invoiceService.UpdateItems(_invoiceSaved.Id, new List<InvoiceItemUpdateCommand>() { updateCommand });

            action.Should().Throw<NotFoundException>();
            _mockIMapper.Verify(m => m.Map<InvoiceItem>(updateCommand));
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockRepositoryProduct.Verify(p => p.GetById(It.IsAny<long>()));
            _mockRepositoryInvoiceItem.Verify(r => r.GetById(updateCommand.Id));
        }











        [Test]
        public void Test_InvoiceService_DeleteItems_ShouldBeOK()
        {
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemDeleteCommand deleteCommand = ObjectMother.GetValidInvoiceItemDeleteCommand();

            bool success = true;

            _mockRepositoryInvoiceItem.Setup(it => it.Remove(deleteCommand.InvoiceItemIds[0])).Returns(success);
            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);
            
            bool result = _invoiceService.DeleteItems(_invoiceSaved.Id, deleteCommand);
            
            _mockRepositoryInvoiceItem.Verify(it => it.Remove(deleteCommand.InvoiceItemIds[0]));
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            result.Should().BeTrue();
        }

        [Test]
        public void Test_InvoiceService_DeleteItems_NonexistentInvoice_ShouldThrowException()
        {
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemDeleteCommand deleteCommand = ObjectMother.GetValidInvoiceItemDeleteCommand();

            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id));

            Action action = () => _invoiceService.DeleteItems(_invoiceSaved.Id, deleteCommand);

            action.Should().Throw<NotFoundException>();
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockRepositoryInvoiceItem.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_InvoiceService_DeleteItems_IssuedInvoice_ShouldThrowException()
        {
            InvoiceItem savedInvoiceItem = ObjectMother.GetExistentValidInvoiceItem(_invoiceSaved, _product);
            InvoiceItemDeleteCommand deleteCommand = ObjectMother.GetValidInvoiceItemDeleteCommand();

            _mockRepositoryInvoice.Setup(r => r.GetById(_invoiceSaved.Id)).Returns(_invoiceSaved);

            _invoiceSaved.Status = InvoiceStatus.ISSUED;

            Action action = () => _invoiceService.DeleteItems(_invoiceSaved.Id, deleteCommand);

            action.Should().Throw<InvoiceSaveIssuedInvoiceException>();
            _mockRepositoryInvoice.Verify(r => r.GetById(_invoiceSaved.Id));
            _mockRepositoryInvoiceItem.VerifyNoOtherCalls();
        }
        
        //      [Test]
        //      public void Test_InvoiceService_Delete_InvalidId_ShouldBeThrowException()
        //      {
        //          Invoice invoiceToDelete = ObjectMother.GetNewValidOpenedInvoice(_sender, _receiver, _carrier, _invoiceItens);

        //          Action action = () => _service.Delete(invoiceToDelete);

        //          action.Should().Throw<IdentifierUndefinedException>();
        //          _repositoryInvoice.VerifyNoOtherCalls();
        //          _repositoryInvoiceItem.VerifyNoOtherCalls();
        //          _repositoryInvoiceCarrier.VerifyNoOtherCalls();
        //          _repositoryInvoiceReceiver.VerifyNoOtherCalls();
        //          _repositoryInvoiceSender.VerifyNoOtherCalls();
        //          _repositoryInvoiceTax.VerifyNoOtherCalls();
        //      }


        //      [Test]
        //      public void Test_InvoiceService_GetAllWithListEmpty_ShouldBeOK()
        //      {
        //          Invoice expectedInvoice = ObjectMother.GetExistentValidOpenedInvoice(_sender, _receiver, _carrier, _invoiceItens);
        //          _repositoryInvoice.Setup(sr => sr.GetAll()).Returns(new List<Invoice>());

        //          IList<Invoice> invoices = _service.GetAll();

        //          invoices.Should().BeNullOrEmpty();
        //          _repositoryInvoice.Verify(sr => sr.GetAll());
        //          _repositoryInvoiceItem.VerifyNoOtherCalls();
        //          _repositoryInvoiceCarrier.VerifyNoOtherCalls();
        //          _repositoryInvoiceReceiver.VerifyNoOtherCalls();
        //          _repositoryInvoiceSender.VerifyNoOtherCalls();
        //          _repositoryInvoiceTax.VerifyNoOtherCalls();
        //      }

        //      [Test]
        //      public void Test_InvoiceService_Issue_IssuedInvoice_ShouldThrowException()
        //      {
        //          double freightValue = 214.43;
        //          Invoice invoiceToIssue = ObjectMother.GetExistentValidIssuedInvoice(_keyAccess, _sender, _receiver, _carrier, _invoiceSender, _invoiceReceiver, _invoiceCarrier, _invoiceTax, _invoiceItens);

        //          Action action = () => _service.Issue(invoiceToIssue, freightValue);

        //          action.Should().Throw<InvoiceIssueIssuedInvoiceException>();
        //          _repositoryInvoice.VerifyNoOtherCalls();
        //          _repositoryInvoiceItem.VerifyNoOtherCalls();
        //          _repositoryInvoiceCarrier.VerifyNoOtherCalls();
        //          _repositoryInvoiceReceiver.VerifyNoOtherCalls();
        //          _repositoryInvoiceSender.VerifyNoOtherCalls();
        //          _repositoryInvoiceTax.VerifyNoOtherCalls();
        //      }

        //      [Test]
        //      public void Test_InvoiceService_Issue_InvalidId_ShouldThrowException()
        //      {
        //          double freightValue = 214.43;
        //          Invoice invoiceToIssue = ObjectMother.GetNewValidOpenedInvoice(_sender, _receiver, _carrier, _invoiceItens);

        //          Action action = () => _service.Issue(invoiceToIssue, freightValue);

        //          action.Should().Throw<IdentifierUndefinedException>();
        //          _repositoryInvoice.VerifyNoOtherCalls();
        //          _repositoryInvoiceItem.VerifyNoOtherCalls();
        //          _repositoryInvoiceCarrier.VerifyNoOtherCalls();
        //          _repositoryInvoiceReceiver.VerifyNoOtherCalls();
        //          _repositoryInvoiceSender.VerifyNoOtherCalls();
        //          _repositoryInvoiceTax.VerifyNoOtherCalls();
        //      }

        //      [Test]
        //      public void Test_InvoiceService_Issue_InvalidInvoice_ShouldThrowException()
        //      {
        //          double freightValue = 214.43;

        //          long existentId = 1;
        //          Invoice invoiceToIssue = ObjectMother.GetInvalidOpenedInvoiceWithUniformedNatureOperation(_sender, _receiver, _carrier, _invoiceItens);
        //          invoiceToIssue.Id = existentId;

        //          Action action = () => _service.Issue(invoiceToIssue, freightValue);

        //          action.Should().Throw<InvoiceUninformedNatureOperationException>();
        //          _repositoryInvoice.VerifyNoOtherCalls();
        //          _repositoryInvoiceItem.VerifyNoOtherCalls();
        //          _repositoryInvoiceCarrier.VerifyNoOtherCalls();
        //          _repositoryInvoiceReceiver.VerifyNoOtherCalls();
        //          _repositoryInvoiceSender.VerifyNoOtherCalls();
        //          _repositoryInvoiceTax.VerifyNoOtherCalls();
        //      }

        //      [Test]
        //      public void Test_InvoiceService_Issue_InvoiceWithExistentNumber_ShouldThrowException()
        //      {
        //          double freightValue = 214.43;

        //          long existentId = 1;
        //          Invoice invoiceToIssue = ObjectMother.GetExistentValidOpenedInvoice(_sender, _receiver, _carrier, _invoiceItens);
        //          invoiceToIssue.Id = existentId;
        //          _repositoryInvoice.Setup(ri => ri.GetByNumber(invoiceToIssue.Number)).Returns(new Invoice { Id = 100, Number = invoiceToIssue.Number});

        //          Action action = () => _service.Issue(invoiceToIssue, freightValue);

        //          action.Should().Throw<InvoiceExistentNumberException>();
        //          _repositoryInvoice.Verify(ri => ri.GetByNumber(invoiceToIssue.Number));
        //          _repositoryInvoiceItem.VerifyNoOtherCalls();
        //          _repositoryInvoiceCarrier.VerifyNoOtherCalls();
        //          _repositoryInvoiceReceiver.VerifyNoOtherCalls();
        //          _repositoryInvoiceSender.VerifyNoOtherCalls();
        //          _repositoryInvoiceTax.VerifyNoOtherCalls();
        //      }

        //      [Test]
        //      public void Test_InvoiceService_Issue_InvoiceWithExistentKeyAccess_ShouldThrowException()
        //       {
        //          string existentKeyAccess = "1234567890987563214501236547896523654125597";
        //          double freightValue = 214.43;

        //          long existentId = 1;
        //          Mock<Invoice> fakeInvoiceToIssue = new Mock<Invoice>();
        //          fakeInvoiceToIssue.Setup(i => i.Id).Returns(existentId);
        //          fakeInvoiceToIssue.Setup(i => i.KeyAccess.Value).Returns(existentKeyAccess);
        //          fakeInvoiceToIssue.Setup(i => i.Issue(freightValue));
        //          _repositoryInvoice.Setup(ri => ri.GetByKeyAccess(fakeInvoiceToIssue.Object.KeyAccess.Value)).Returns(new Invoice { Id = 100, KeyAccess = new KeyAccess { Value = existentKeyAccess} });

        //          Action action = () => _service.Issue(fakeInvoiceToIssue.Object, freightValue);

        //          action.Should().Throw<InvoiceKeyAccessExistentException>();
        //          _repositoryInvoice.Verify(ri => ri.GetByKeyAccess(fakeInvoiceToIssue.Object.KeyAccess.Value));
        //          _repositoryInvoiceItem.VerifyNoOtherCalls();
        //          _repositoryInvoiceCarrier.VerifyNoOtherCalls();
        //          _repositoryInvoiceReceiver.VerifyNoOtherCalls();
        //          _repositoryInvoiceSender.VerifyNoOtherCalls();
        //          _repositoryInvoiceTax.VerifyNoOtherCalls();
        //      }

        //      [Test]
        //      public void Test_InvoiceService_Issue_ShouldBeOk()
        //      {
        //          double freightValue = 214.43;

        //          long existentId = 1;
        //          Invoice invoiceToIssue = ObjectMother.GetExistentValidOpenedInvoice(_sender, _receiver, _carrier, _invoiceItens);
        //          invoiceToIssue.Id = existentId;

        //          Invoice invoiceIssued =_service.Issue(invoiceToIssue, freightValue);

        //          invoiceIssued.InvoiceCarrier.Should().Be(invoiceToIssue.InvoiceCarrier);
        //          invoiceIssued.InvoiceReceiver.Should().Be(invoiceToIssue.InvoiceReceiver);
        //          invoiceIssued.InvoiceSender.Should().Be(invoiceToIssue.InvoiceSender);
        //          invoiceIssued.InvoiceTax.Should().Be(invoiceToIssue.InvoiceTax);
        //          _repositoryInvoice.Verify(ri => ri.GetByNumber(invoiceToIssue.Number));
        //          _repositoryInvoice.Verify(ri => ri.GetByKeyAccess(invoiceToIssue.KeyAccess.Value));
        //          _repositoryInvoiceItem.Verify(rii => rii.Update(invoiceToIssue.InvoiceItems.First()));
        //          _repositoryInvoiceTax.Verify(ritx => ritx.Add(invoiceToIssue.InvoiceTax));
        //          _repositoryInvoiceCarrier.Verify(ric => ric.Add(invoiceToIssue.InvoiceCarrier));
        //          _repositoryInvoiceReceiver.Verify(rir => rir.Add(invoiceToIssue.InvoiceReceiver));
        //          _repositoryInvoiceSender.Verify(ris => ris.Add(invoiceToIssue.InvoiceSender));
        //      }

        //[Test]
        //public void Test_InvoiceService_ExportToXML_ShouldBeOk()
        //{
        //	long existentId = 1;
        //	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML NFe - Teste método ExportToXML Serviço Invoice (Valores aleatórios).xml";

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(existentId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.ISSUED);
        //	fakeInvoice.Setup(v => v.Validate());

        //	Invoice invoice = fakeInvoice.Object;
        //	_repositoryXML.Setup(ri => ri.Export(invoice, path));
        //	_repositoryInvoice.Setup(sr => sr.Get(invoice.Id)).Returns(invoice);

        //	_service.ExportToXML(invoice, path);

        //	_repositoryInvoice.Verify(ri => ri.Get(invoice.Id));
        //	_repositoryInvoiceItem.Verify(rii => rii.GetByInvoice(invoice.Id));
        //	fakeInvoice.Verify(v => v.Validate());
        //	_repositoryXML.Verify(ri => ri.Export(invoice, path));
        //}

        //[Test]
        //public void Test_InvoiceService_ExportToXMLWithInvoiceInvalidId_ShouldThrowException()
        //{
        //	long invalidId = -1;
        //	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML NFe - Teste método ExportToXML Serviço Invoice (Valores aleatórios).xml";

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(invalidId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.ISSUED);

        //	Invoice invoice = fakeInvoice.Object;

        //	Action action = () => _service.ExportToXML(invoice, path);
        //	action.Should().Throw<IdentifierUndefinedException>();

        //	_repositoryInvoice.VerifyNoOtherCalls();
        //	_repositoryInvoiceItem.VerifyNoOtherCalls();
        //	_repositoryXML.VerifyNoOtherCalls();
        //}

        //[Test]
        //public void Test_InvoiceService_ExportToXMLWithInvoiceStatusOpen_ShouldThrowException()
        //{
        //	long existentId = 1;
        //	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//XML NFe - Teste método ExportToXML Serviço Invoice (Valores aleatórios).xml";

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(existentId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.OPEN);

        //	Invoice invoice = fakeInvoice.Object;
        //	_repositoryXML.Setup(ri => ri.Export(invoice, path));
        //	_repositoryInvoice.Setup(sr => sr.Get(invoice.Id)).Returns(invoice);

        //	Action action = () => _service.ExportToXML(invoice, path);
        //	action.Should().Throw<InvoiceExportOpenInvoiceException>();

        //	_repositoryInvoice.Verify(ri => ri.Get(invoice.Id));
        //	_repositoryInvoiceItem.Verify(rii => rii.GetByInvoice(invoice.Id));
        //	_repositoryXML.VerifyNoOtherCalls();
        //}

        //[Test]
        //public void Test_InvoiceService_ExportToXMLWithPathNull_ShouldThrowException()
        //{
        //	long existentId = 1;
        //	string path = null;

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(existentId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.ISSUED);
        //	fakeInvoice.Setup(v => v.Validate());

        //	Invoice invoice = fakeInvoice.Object;
        //	_repositoryXML.Setup(ri => ri.Export(invoice, path));
        //	_repositoryInvoice.Setup(sr => sr.Get(invoice.Id)).Returns(invoice);

        //	Action action = () => _service.ExportToXML(invoice, path);
        //	action.Should().Throw<InvoiceExportInvalidPathException>();

        //	_repositoryInvoice.VerifyNoOtherCalls();
        //	_repositoryInvoiceItem.VerifyNoOtherCalls();
        //	_repositoryXML.VerifyNoOtherCalls();
        //}

        //[Test]
        //public void Test_InvoiceService_ExportToXMLWithPathEmpty_ShouldThrowException()
        //{
        //	long existentId = 1;
        //	string path = "";

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(existentId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.ISSUED);
        //	fakeInvoice.Setup(v => v.Validate());

        //	Invoice invoice = fakeInvoice.Object;
        //	_repositoryXML.Setup(ri => ri.Export(invoice, path));
        //	_repositoryInvoice.Setup(sr => sr.Get(invoice.Id)).Returns(invoice);

        //	Action action = () => _service.ExportToXML(invoice, path);
        //	action.Should().Throw<InvoiceExportInvalidPathException>();

        //	_repositoryInvoice.VerifyNoOtherCalls();
        //	_repositoryInvoiceItem.VerifyNoOtherCalls();
        //	_repositoryXML.VerifyNoOtherCalls();
        //}

        //[Test]
        //public void Test_InvoiceService_ExportToPDF_ShouldBeOk()
        //{
        //	long existentId = 1;
        //	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste método ExportToPDF Serviço Invoice (Valores aleatórios).xml";

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(existentId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.ISSUED);
        //	fakeInvoice.Setup(v => v.Validate());

        //	Invoice invoice = fakeInvoice.Object;
        //	_repositoryInvoicePDF.Setup(ri => ri.Export(invoice, path));
        //	_repositoryInvoice.Setup(sr => sr.Get(invoice.Id)).Returns(invoice);

        //	_service.ExportToPDF(invoice, path);

        //	_repositoryInvoice.Verify(ri => ri.Get(invoice.Id));
        //	_repositoryInvoiceItem.Verify(rii => rii.GetByInvoice(invoice.Id));
        //	fakeInvoice.Verify(v => v.Validate());
        //	_repositoryInvoicePDF.Verify(ri => ri.Export(invoice, path));
        //}

        //[Test]
        //public void Test_InvoiceServiceExportToPDFWithInvoiceInvalidId_ShouldThrowException()
        //{
        //	long invalidId = -1;
        //	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste método ExportToPDF Serviço Invoice (Valores aleatórios).xml";

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(invalidId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.ISSUED);

        //	Invoice invoice = fakeInvoice.Object;

        //	Action action = () => _service.ExportToPDF(invoice, path);
        //	action.Should().Throw<IdentifierUndefinedException>();

        //	_repositoryInvoice.VerifyNoOtherCalls();
        //	_repositoryInvoiceItem.VerifyNoOtherCalls();
        //	_repositoryInvoicePDF.VerifyNoOtherCalls();
        //}

        //[Test]
        //public void Test_InvoiceService_ExportToPDFWithInvoiceStatusOpen_ShouldThrowException()
        //{
        //	long existentId = 1;
        //	string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//PDF NFe - Teste método ExportToPDF Serviço Invoice (Valores aleatórios).xml";

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(existentId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.OPEN);

        //	Invoice invoice = fakeInvoice.Object;
        //	_repositoryInvoicePDF.Setup(ri => ri.Export(invoice, path));
        //	_repositoryInvoice.Setup(sr => sr.Get(invoice.Id)).Returns(invoice);

        //	Action action = () => _service.ExportToPDF(invoice, path);
        //	action.Should().Throw<InvoiceExportOpenInvoiceException>();

        //	_repositoryInvoice.Verify(ri => ri.Get(invoice.Id));
        //	_repositoryInvoiceItem.Verify(rii => rii.GetByInvoice(invoice.Id));
        //	_repositoryInvoicePDF.VerifyNoOtherCalls();
        //}

        //[Test]
        //public void Test_InvoiceService_ExportToPDFWithPathNull_ShouldThrowException()
        //{
        //	long existentId = 1;
        //	string path = null;

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(existentId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.ISSUED);
        //	fakeInvoice.Setup(v => v.Validate());

        //	Invoice invoice = fakeInvoice.Object;
        //	_repositoryInvoicePDF.Setup(ri => ri.Export(invoice, path));
        //	_repositoryInvoice.Setup(sr => sr.Get(invoice.Id)).Returns(invoice);

        //	Action action = () => _service.ExportToPDF(invoice, path);
        //	action.Should().Throw<InvoiceExportInvalidPathException>();

        //	_repositoryInvoice.VerifyNoOtherCalls();
        //	_repositoryInvoiceItem.VerifyNoOtherCalls();
        //	_repositoryInvoicePDF.VerifyNoOtherCalls();
        //}

        //[Test]
        //public void Test_InvoiceService_ExportToPDFWithPathEmpty_ShouldThrowException()
        //{
        //	long existentId = 1;
        //	string path = null;

        //	Mock<Invoice> fakeInvoice = new Mock<Invoice>();
        //	fakeInvoice.Setup(i => i.Id).Returns(existentId);
        //	fakeInvoice.Setup(s => s.Status).Returns(InvoiceStatus.ISSUED);
        //	fakeInvoice.Setup(v => v.Validate());

        //	Invoice invoice = fakeInvoice.Object;
        //	_repositoryInvoicePDF.Setup(ri => ri.Export(invoice, path));
        //	_repositoryInvoice.Setup(sr => sr.Get(invoice.Id)).Returns(invoice);

        //	Action action = () => _service.ExportToPDF(invoice, path);
        //	action.Should().Throw<InvoiceExportInvalidPathException>();

        //	_repositoryInvoice.VerifyNoOtherCalls();
        //	_repositoryInvoiceItem.VerifyNoOtherCalls();
        //	_repositoryInvoicePDF.VerifyNoOtherCalls();
        //}
    }
}