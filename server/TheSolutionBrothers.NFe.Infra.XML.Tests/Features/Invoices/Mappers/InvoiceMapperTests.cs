using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers;
using TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.XML.Tests.Features.Invoices.Mappers
{

    [TestFixture]
    public class InvoiceMapperTests
    {

        private IMapper<Invoice, NFeModel> _invoiceMapper;

        public Mock<IMapper<Sender, EmitModel>> _fakeSenderMapper;
        public Mock<IMapper<Receiver, DestModel>> _fakeReceiverMapper;
        public Mock<IMapper<InvoiceItem, DetModel>> _fakeInvoiceItemMapper;
        public Mock<IMapper<InvoiceTax, TotalModel>> _fakeInvoiceTaxMapper;

        private Mock<KeyAccess> _fakeKeyAccess;
        private Mock<Receiver> _dummyReceiver;
        private Mock<Sender> _dummySender;
        private Mock<Carrier> _dummyCarrier;
        private Mock<InvoiceReceiver> _dummyInvoiceReceiver;
        private Mock<InvoiceSender> _dummyInvoiceSender;
        private Mock<InvoiceCarrier> _dummyInvoiceCarrier;
        private Mock<InvoiceItem> _dummyInvoiceItem1;
        private Mock<InvoiceItem> _dummyInvoiceItem2;
        private Mock<InvoiceItem> _dummyInvoiceItem3;
        private Mock<InvoiceTax> _dummyInvoiceTax;

        [SetUp]
        public void Initialize()
        {
            _fakeKeyAccess = new Mock<KeyAccess>();

            _dummyReceiver = new Mock<Receiver>();
            _dummySender = new Mock<Sender>();
            _dummyCarrier = new Mock<Carrier>();
            _dummyInvoiceReceiver = new Mock<InvoiceReceiver>();
            _dummyInvoiceSender = new Mock<InvoiceSender>();
            _dummyInvoiceCarrier = new Mock<InvoiceCarrier>();

            _dummyInvoiceItem1 = new Mock<InvoiceItem>();
            _dummyInvoiceItem2 = new Mock<InvoiceItem>();
            _dummyInvoiceItem3 = new Mock<InvoiceItem>();
            _dummyInvoiceTax = new Mock<InvoiceTax>();

            _fakeReceiverMapper = new Mock<IMapper<Receiver, DestModel>>();
            _fakeSenderMapper = new Mock<IMapper<Sender, EmitModel>>();
            _fakeInvoiceItemMapper = new Mock<IMapper<InvoiceItem, DetModel>>();
            _fakeInvoiceTaxMapper = new Mock<IMapper<InvoiceTax, TotalModel>>();

            _invoiceMapper = new InvoiceMapper(_fakeSenderMapper.Object, _fakeReceiverMapper.Object,
                                    _fakeInvoiceItemMapper.Object, _fakeInvoiceTaxMapper.Object);
        }

        [Test]
        public void Test_InvoiceMapper_Map_ShouldBeOk()
        {
            Receiver receiver = _dummyReceiver.Object;
            Sender sender = _dummySender.Object;
            Carrier carrier = _dummyCarrier.Object;

            InvoiceReceiver invoiceReceiver = _dummyInvoiceReceiver.Object;
            InvoiceSender invoiceSender = _dummyInvoiceSender.Object;
            InvoiceCarrier invoiceCarrier = _dummyInvoiceCarrier.Object;
            
            InvoiceItem invoiceItem = _dummyInvoiceItem1.Object;
            InvoiceTax invoiceTax = _dummyInvoiceTax.Object;

            IList<InvoiceItem> items = new List<InvoiceItem>()
            {
                invoiceItem
            };

            Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(_fakeKeyAccess.Object, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, items);

            _fakeKeyAccess.Setup(ka => ka.Value).Returns("42180308723218000186656620000000011873157004");

            _dummyInvoiceReceiver.Setup(ic => ic.Receiver).Returns(receiver);
            _dummyInvoiceSender.Setup(ic => ic.Sender).Returns(sender);
            _dummyInvoiceCarrier.Setup(ic => ic.Carrier).Returns(carrier);

            _fakeSenderMapper.Setup(sm => sm.Map(invoice.InvoiceSender.Sender)).Returns(new EmitModel());
            _fakeReceiverMapper.Setup(rm => rm.Map(invoice.InvoiceReceiver.Receiver)).Returns(new DestModel());
            _fakeInvoiceTaxMapper.Setup(im => im.Map(invoice.InvoiceTax)).Returns(new TotalModel());

            int index = 1;
            foreach (InvoiceItem item in invoice.InvoiceItems)
            {
                _fakeInvoiceItemMapper.Setup(im => im.Map(item)).Returns(new DetModel() { NItem = index++ });
            }

            NFeModel nfeModel = _invoiceMapper.Map(invoice);

            int expectedLength = 1;

            nfeModel.InfNFe.Id.Should().Be("NFe42180308723218000186656620000000011873157004");
            nfeModel.InfNFe.Ide.DhEmi.Should().Be(invoice.IssueDate.Value.ToString("yyyy-MM-dd'T'HH:mm:sszzz"));
            nfeModel.InfNFe.Ide.NatOp.Should().Be(invoice.NatureOperation);
            nfeModel.InfNFe.Emit.Should().NotBeNull();
            nfeModel.InfNFe.Dest.Should().NotBeNull();
            nfeModel.InfNFe.Dets.Should().NotBeNull();
            nfeModel.InfNFe.Dets.Length.Should().Be(expectedLength);
            nfeModel.InfNFe.Total.Should().NotBeNull();

            index = 1;
            foreach (DetModel det in nfeModel.InfNFe.Dets)
            {
                det.NItem.Should().Be(index);
                index++;
            }

            _fakeSenderMapper.Verify(sm => sm.Map(invoice.InvoiceSender.Sender));
            _fakeReceiverMapper.Verify(rm => rm.Map(invoice.InvoiceReceiver.Receiver));
            _fakeInvoiceTaxMapper.Verify(im => im.Map(invoice.InvoiceTax));

            index = 1;
            foreach (InvoiceItem item in invoice.InvoiceItems)
            {
                _fakeInvoiceItemMapper.Verify(im => im.Map(item));
            }
        }

		[Test]
		public void Test_InvoiceMapper_MapWithVariousItems_ShouldBeOk()
		{
			Receiver receiver = _dummyReceiver.Object;
			Sender sender = _dummySender.Object;
			Carrier carrier = _dummyCarrier.Object;

			InvoiceReceiver invoiceReceiver = _dummyInvoiceReceiver.Object;
			InvoiceSender invoiceSender = _dummyInvoiceSender.Object;
			InvoiceCarrier invoiceCarrier = _dummyInvoiceCarrier.Object;

			InvoiceItem invoiceItem1 = _dummyInvoiceItem1.Object;
            InvoiceItem invoiceItem2 = _dummyInvoiceItem2.Object;
            InvoiceItem invoiceItem3 = _dummyInvoiceItem3.Object;
            InvoiceTax invoiceTax = _dummyInvoiceTax.Object;

			IList<InvoiceItem> items = new List<InvoiceItem>()
			{
				invoiceItem1,
				invoiceItem2,
				invoiceItem3

			};

			Invoice invoice = ObjectMother.GetExistentValidIssuedInvoice(_fakeKeyAccess.Object, sender, receiver, carrier, invoiceSender, invoiceReceiver, invoiceCarrier, invoiceTax, items);

			_fakeKeyAccess.Setup(ka => ka.Value).Returns("42180308723218000186656620000000011873157004");

			_dummyInvoiceReceiver.Setup(ic => ic.Receiver).Returns(receiver);
			_dummyInvoiceSender.Setup(ic => ic.Sender).Returns(sender);
			_dummyInvoiceCarrier.Setup(ic => ic.Carrier).Returns(carrier);

			_fakeSenderMapper.Setup(sm => sm.Map(invoice.InvoiceSender.Sender)).Returns(new EmitModel());
			_fakeReceiverMapper.Setup(rm => rm.Map(invoice.InvoiceReceiver.Receiver)).Returns(new DestModel());
			_fakeInvoiceTaxMapper.Setup(im => im.Map(invoice.InvoiceTax)).Returns(new TotalModel());

			int index = 1;
			foreach (InvoiceItem item in invoice.InvoiceItems)
			{
				_fakeInvoiceItemMapper.Setup(im => im.Map(item)).Returns(new DetModel() { NItem = index++ });
			}

			NFeModel nfeModel = _invoiceMapper.Map(invoice);

			int expectedLength = 3;

			nfeModel.InfNFe.Id.Should().Be("NFe42180308723218000186656620000000011873157004");
			nfeModel.InfNFe.Ide.DhEmi.Should().Be(invoice.IssueDate.Value.ToString("yyyy-MM-dd'T'HH:mm:sszzz"));
			nfeModel.InfNFe.Ide.NatOp.Should().Be(invoice.NatureOperation);
			nfeModel.InfNFe.Emit.Should().NotBeNull();
			nfeModel.InfNFe.Dest.Should().NotBeNull();
			nfeModel.InfNFe.Dets.Should().NotBeNull();
			nfeModel.InfNFe.Dets.Length.Should().Be(expectedLength);
			nfeModel.InfNFe.Total.Should().NotBeNull();

            index = 1; 
            foreach (DetModel det in nfeModel.InfNFe.Dets)
            {
                det.NItem.Should().Be(index);
                index++;
            }

			_fakeSenderMapper.Verify(sm => sm.Map(invoice.InvoiceSender.Sender));
			_fakeReceiverMapper.Verify(rm => rm.Map(invoice.InvoiceReceiver.Receiver));
			_fakeInvoiceTaxMapper.Verify(im => im.Map(invoice.InvoiceTax));
            
			foreach (InvoiceItem item in invoice.InvoiceItems)
			{
				_fakeInvoiceItemMapper.Verify(im => im.Map(item));
			}
		}
	}
}
