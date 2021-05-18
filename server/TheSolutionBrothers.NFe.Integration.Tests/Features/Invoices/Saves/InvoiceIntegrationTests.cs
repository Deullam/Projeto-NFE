using FluentAssertions;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Application.Features.Invoices;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
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
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Infra.Data.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Infra.PDF.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Integration.Tests.Features.Invoices
{
    [TestFixture]
    public partial class InvoiceIntegrationTests
    {
  //      private IInvoiceRepository _invoiceRepository;
  //      private IInvoiceItemRepository _invoiceItemRepository;
  //      private IInvoiceTaxRepository _invoiceTaxRepository;
  //      private IInvoiceSenderRepository _invoiceSenderRepository;
  //      private IInvoiceReceiverRepository _invoiceReceiverRepository;
		//private IInvoiceCarrierRepository _invoiceCarrierRepository;
		//private IInvoiceRepositoryXML _invoiceXMLRepository;
  //      private IInvoiceRepositoryPDF _invoicePDFRepository;
  //      private InvoicePdfGenerator _invoicePDFGenerator;


		//public IMapper<Invoice, NFeModel> _invoiceMapper;
		//public IMapper<Sender, EmitModel> _senderMapper;
		//public IMapper<Receiver, DestModel> _receiverMapper;
		//public IMapper<InvoiceItem, DetModel> _invoiceItemMapper;
		//public IMapper<InvoiceTax, TotalModel> _invoiceTaxMapper;
		//public IMapper<Address, EnderModel> _addressMapper;

		//private IInvoiceService _invoiceService;

  //      [SetUp]
  //      public void Initialize()
  //      {
  //          BaseSqlTest.SeedDatabase();
  //          _invoiceRepository = new InvoiceRepository();
  //          _invoiceItemRepository = new InvoiceItemRepository();
  //          _invoiceTaxRepository = new InvoiceTaxRepository();
  //          _invoiceSenderRepository = new InvoiceSenderRepository();
  //          _invoiceReceiverRepository = new InvoiceReceiverRepository();
  //          _invoiceCarrierRepository = new InvoiceCarrierRepository();
  //          _invoicePDFGenerator = new InvoicePdfGenerator();
  //          _invoicePDFRepository = new InvoiceRepositoryPDF(_invoicePDFGenerator);

  //          _addressMapper = new AddressMapper();
  //          _senderMapper = new SenderMapper(_addressMapper);
  //          _receiverMapper = new ReceiverMapper(_addressMapper);
  //          _invoiceItemMapper = new InvoiceItemMapper();
  //          _invoiceTaxMapper = new InvoiceTaxMapper();
  //          _invoiceMapper = new InvoiceMapper(_senderMapper, _receiverMapper, _invoiceItemMapper, _invoiceTaxMapper);

		//	_invoiceXMLRepository = new InvoiceRepositoryXML(_invoiceMapper);

		//	_invoiceService = new InvoiceService(_invoiceRepository, _invoiceCarrierRepository, _invoiceReceiverRepository,
  //                                        _invoiceSenderRepository, _invoiceItemRepository, _invoiceTaxRepository, _invoiceXMLRepository,
  //                                        _invoicePDFRepository);
            
  //      }

  //      [Test]
  //      public void Test_InvoiceIntegration_Add_ShouldBeOk()
  //      {
  //          int validNumber = 10;
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);
  //          invoiceToSave.Number = validNumber;
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product,invoiceToSave);
  //          items.Add(item);

  //          Invoice invoiceSaved = _invoiceService.Add(invoiceToSave);
  //          invoiceSaved.Id.Should().BeGreaterThan(0);
  //      }

  //      [Test]
  //      public void Test_InvoiceService_Add_Issued_ShouldThrowException()
  //      {
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          InvoiceSender invoiceSender = new InvoiceSender();
  //          InvoiceReceiver invoiceReceiver = new InvoiceReceiver();
  //          InvoiceCarrier invoiceCarrier = new InvoiceCarrier();
  //          InvoiceTax invoiceTax = new InvoiceTax();
  //          KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();

  //          Invoice invoiceToSave = ObjectMother.GetNewValidIssuedInvoice(keyAccess,sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, items);
  //          invoiceToSave.InvoiceSender = ObjectMother.GetExistentInvoinceSenderOk(invoiceToSave,sender);
  //          invoiceToSave.InvoiceReceiver = ObjectMother.GetExistentValidInvoiceReceiver(invoiceToSave,receiver);
  //          invoiceToSave.InvoiceCarrier = ObjectMother.GetExistentInvoiceCarrier(carrier,invoiceToSave);
  //          invoiceToSave.InvoiceTax = ObjectMother.GetExistentValidInvoiceTax(invoiceToSave);

  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
  //          items.Add(item);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceSaveIssuedInvoiceException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceService_Add_ExistentNumber_ShouldThrowException()
  //      {
  //          int existentNumber = 4;
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          InvoiceSender invoiceSender = new InvoiceSender();
  //          InvoiceReceiver invoiceReceiver = new InvoiceReceiver();
  //          InvoiceCarrier invoiceCarrier = new InvoiceCarrier();
  //          InvoiceTax invoiceTax = new InvoiceTax();
  //          KeyAccess keyAccess = ObjectMother.GetValidKeyAccess();

  //          Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);
  //          invoiceToSave.Number = existentNumber;
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
  //          items.Add(item);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceExistentNumberException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceService_Add_UninformedNatureOperation_ShouldThrowException()
  //      {
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoiceToSave = ObjectMother.GetInvalidOpenedInvoiceWithUniformedNatureOperation(sender, receiver, carrier, items);

  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
  //          items.Add(item);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceUninformedNatureOperationException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceService_Add_NaruteOperationLengthOverflow_ShouldThrowException()
  //      {
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoiceToSave = ObjectMother.GetInvalidOpenedInvoiceWithNatureOperationLengthOverflow(sender, receiver, carrier, items);

  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
  //          items.Add(item);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceNaruteOperationLengthOverflowException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceService_Add_NonPositiveNumber_ShouldThrowException()
  //      {
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoiceToSave = ObjectMother.GetInvalidOpenedInvoiceWithNonPositiveNumber(sender, receiver, carrier, items);

  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
  //          items.Add(item);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceNonPositiveNumberException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceService_Add_EntryDateAfterNow_ShouldThrowException()
  //      {
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoiceToSave = ObjectMother.GetInvalidOpenedInvoiceWithEntryDateAfterNow(sender, receiver, carrier, items);

  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
  //          items.Add(item);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceEntryDateAfterNowException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceIntegration_Add_NullCarrier_ShouldBeOk()
  //      {
  //          int validNumber = 10;
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, null, items);
  //          invoice.Number = validNumber;
  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoice);
  //          items.Add(item);

  //          _invoiceService.Add(invoice);

  //          int expectedItemsCount = 1;

  //          Invoice invoiceFromDatabase = _invoiceService.Get(invoice.Id);
  //          invoiceFromDatabase.Status.Should().Be(invoice.Status);
  //          invoiceFromDatabase.IssueDate.HasValue.Should().BeFalse();
  //          invoiceFromDatabase.KeyAccess.Should().BeNull();
  //          invoiceFromDatabase.InvoiceCarrier.Should().BeNull();
  //          invoiceFromDatabase.InvoiceSender.Should().BeNull();
  //          invoiceFromDatabase.InvoiceReceiver.Should().BeNull();
  //          invoiceFromDatabase.InvoiceTax.Should().BeNull();
  //          invoiceFromDatabase.InvoiceItems.Count.Should().Be(expectedItemsCount);
  //          invoiceFromDatabase.InvoiceItems[0].Id.Should().Be(invoice.InvoiceItems[0].Id);
  //          invoiceFromDatabase.Carrier.Should().BeNull();
  //          invoiceFromDatabase.Receiver.Id.Should().Be(invoice.Receiver.Id);
  //          invoiceFromDatabase.Sender.Id.Should().Be(invoice.Sender.Id);
  //      }

  //      [Test]
  //      public void Test_InvoiceService_Add_EmptyInvoiceItems_ShouldThrowException()
  //      {
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceEmptyInvoiceItemsException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceService_Add_NullReceiver_ShouldThrowException()
  //      {
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoiceToSave = ObjectMother.GetInvalidOpenedInvoiceWithNullReceiver(sender, carrier, items);

  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
  //          items.Add(item);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceNullReceiverException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceService_Add_NullSender_ShouldThrowException()
  //      {
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = new CNPJ() { Value = "00745557000151" };

  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoiceToSave = ObjectMother.GetInvalidOpenedInvoiceWithNullSender(receiver, carrier, items);

  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
  //          items.Add(item);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceNullSenderException>();
  //      }

  //      [Test]
  //      public void Test_InvoiceIntegration_AddInvoiceWithReceiverEqualThanSender_ShouldThrowException()
  //      {
  //          CPF cpfCarrier = ObjectMother.GetValidCPF();
  //          CNPJ cnpjReceiver = ObjectMother.GetValidCNPJ();
  //          CNPJ cnpjSender = ObjectMother.GetValidCNPJ();

  //          Sender sender = ObjectMother.GetExistentValidSender(ObjectMother.GetExistentValidAddress(), cnpjSender);
  //          Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(ObjectMother.GetExistentValidAddress(), cnpjReceiver);
  //          Carrier carrier = ObjectMother.GetExistentValidCarrierPhysical(ObjectMother.GetExistentValidAddress(), cpfCarrier);

  //          IList<InvoiceItem> items = new List<InvoiceItem>();

  //          Invoice invoiceToSave = ObjectMother.GetNewValidOpenedInvoice(sender, receiver, carrier, items);

  //          Product product = ObjectMother.GetExistentValidProduct(ObjectMother.GetValidTaxProduct());
  //          InvoiceItem item = ObjectMother.GetExistentInvoinceItemOk(product, invoiceToSave);
  //          items.Add(item);

  //          Action action = () => _invoiceService.Add(invoiceToSave);
  //          action.Should().Throw<InvoiceReceiverEqualThanSenderException>();
  //      }
    }
}
