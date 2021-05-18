using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Base;
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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.Invoices
{
    
    [TestFixture]
    public class InvoiceTests
    {

        private Mock<KeyAccess> _fakeKeyAccess;
        private Mock<Sender> _fakeSender;
        private Mock<Receiver> _fakeReceiver;
        private Mock<Carrier> _fakeCarrier;
        private Mock<InvoiceSender> _fakeInvoiceSender;
        private Mock<InvoiceReceiver> _fakeInvoiceReceiver;
        private Mock<InvoiceCarrier> _fakeInvoiceCarrier;
        private Mock<InvoiceTax> _fakeInvoiceTax;
        private Mock<InvoiceItem> _fakeInvoiceItem;

        private IList<InvoiceItem> _items;

        [SetUp]
        public void Initialize()
        {
            _fakeKeyAccess = new Mock<KeyAccess>();
            _fakeSender = new Mock<Sender>();
            _fakeReceiver = new Mock<Receiver>();
            _fakeCarrier = new Mock<Carrier>();
            _fakeInvoiceSender = new Mock<InvoiceSender>();
            _fakeInvoiceReceiver = new Mock<InvoiceReceiver>();
            _fakeInvoiceCarrier = new Mock<InvoiceCarrier>();
            _fakeInvoiceTax = new Mock<InvoiceTax>();

            _fakeInvoiceItem = new Mock<InvoiceItem>();

            _items = new List<InvoiceItem>();
            _items.Add(_fakeInvoiceItem.Object);
        }
        
        //TODO criar métodos para quebrar o issue, validar se o número e chave de acesso são únicos e mudar tabela (UNIQUE)

        [Test]
        public void Test_Invoice_IsReceiverEqualThanSenderWithEqualCNPJs_ShouldBeOk()
        {
            Mock<Address> _dummyAddress = new Mock<Address>();

            Mock<CNPJ> _fakeCNPJ = new Mock<CNPJ>();
            CNPJ cnpj = _fakeCNPJ.Object;

            _fakeCNPJ.Setup(c => c.Equals(cnpj)).Returns(true);
            
            Invoice invoice = new Invoice()
            {
                Receiver = ObjectMother.GetNewValidLegalReceiver(_dummyAddress.Object, cnpj),
                Sender = ObjectMother.GetNewValidSender(_dummyAddress.Object, cnpj)
            };

            bool result = invoice.IsReceiverEqualThanSender();

            result.Should().BeTrue();
            _fakeCNPJ.Verify(c => c.Equals(cnpj));
        }

        [Test]
        public void Test_Invoice_IsReceiverEqualThanSenderWithDifferentCNPJs_ShouldBeOk()
        {
            Mock<Address> _dummyAddress = new Mock<Address>();

            Mock<CNPJ> _fakeCNPJ = new Mock<CNPJ>();
            CNPJ cnpj = _fakeCNPJ.Object;

            _fakeCNPJ.Setup(c => c.Equals(cnpj)).Returns(false);

            Invoice invoice = new Invoice()
            {
                Receiver = ObjectMother.GetNewValidLegalReceiver(_dummyAddress.Object, cnpj),
                Sender = ObjectMother.GetNewValidSender(_dummyAddress.Object, cnpj)
            };

            bool result = invoice.IsReceiverEqualThanSender();

            result.Should().BeFalse();
            _fakeCNPJ.Verify(c => c.Equals(cnpj));
        }

        [Test]
        public void Test_Invoice_IsReceiverEqualThanSenderWithDifferentPersonType_ShouldBeOk()
        {
            Mock<Address> _dummyAddress = new Mock<Address>();
            Mock<CPF> _dummyCPF = new Mock<CPF>();

            Mock<CNPJ> _fakeCNPJ = new Mock<CNPJ>();
            CNPJ cnpj = _fakeCNPJ.Object;
            
            Invoice invoice = new Invoice()
            {
                Receiver = ObjectMother.GetNewValidPhysicalReceiver(_dummyAddress.Object, _dummyCPF.Object),
                Sender = ObjectMother.GetNewValidSender(_dummyAddress.Object, cnpj)
            };

            bool result = invoice.IsReceiverEqualThanSender();

            result.Should().BeFalse();
            _fakeCNPJ.VerifyNoOtherCalls();
        }

        [Test]
        public void Test_Invoice_Issue_ShouldBeOk()
        {
            double freightValue = 321.04;

            _fakeSender.Setup(s => s.Id).Returns(1);
            _fakeReceiver.Setup(s => s.Id).Returns(1);
            _fakeCarrier.Setup(s => s.Id).Returns(1);

            double expectedIpiValue = 10;
            double expectedIcmsValue = 4;
            double expectedTotalValue = 100;

            double expectedInvoiceTotalValue = 435.04;

            _fakeInvoiceItem.Setup(i => i.IpiValue).Returns(expectedIpiValue);
            _fakeInvoiceItem.Setup(i => i.IcmsValue).Returns(expectedIcmsValue);
            _fakeInvoiceItem.Setup(i => i.TotalValue).Returns(expectedTotalValue);

            Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);
            invoice.Issue(freightValue);

            invoice.Validate();
            invoice.Status.Should().Be(InvoiceStatus.ISSUED);
            invoice.IssueDate.HasValue.Should().BeTrue();
            invoice.KeyAccess.Should().NotBeNull();
            invoice.KeyAccess.Validate();
            invoice.InvoiceSender.Should().NotBeNull();
            invoice.InvoiceSender.Sender.Id.Should().Be(invoice.Sender.Id);
            invoice.InvoiceReceiver.Should().NotBeNull();
            invoice.InvoiceReceiver.Receiver.Id.Should().Be(invoice.Receiver.Id);
            invoice.InvoiceCarrier.Should().NotBeNull();
            invoice.InvoiceCarrier.Carrier.Id.Should().Be(invoice.Carrier.Id);
            invoice.InvoiceTax.Freight.Should().Be(freightValue);
            invoice.InvoiceTax.TotalValueInvoice.Should().Be(expectedInvoiceTotalValue);
            invoice.InvoiceTax.TotalValueProducts.Should().Be(expectedTotalValue);
            invoice.InvoiceTax.IpiValue.Should().Be(expectedIpiValue);
            invoice.InvoiceTax.IcmsValue.Should().Be(expectedIcmsValue);

        }

        [Test]
        public void Test_Invoice_IssueWithoutCarrier_ShouldBeOk()
        {
            double freightValue = 321.04;

            _fakeSender.Setup(s => s.Id).Returns(1);

            _fakeInvoiceItem.Setup(s => s.Consolidate());
            _fakeInvoiceItem.Setup(s => s.Validate());

            double expectedIpiValue = 10;
            double expectedIcmsValue = 4;
            double expectedTotalValue = 100;

            double expectedInvoiceTotalValue = 435.04;

            _fakeInvoiceItem.Setup(i => i.IpiValue).Returns(expectedIpiValue);
            _fakeInvoiceItem.Setup(i => i.IcmsValue).Returns(expectedIcmsValue);
            _fakeInvoiceItem.Setup(i => i.TotalValue).Returns(expectedTotalValue);

            _fakeReceiver.Setup(s => s.Id).Returns(1);
            _fakeReceiver.Setup(s => s.Name).Returns("Teste");
            _fakeReceiver.Setup(s => s.Cpf).Returns(ObjectMother.GetValidCPF());
            _fakeReceiver.Setup(s => s.Type).Returns(PersonType.PHYSICAL);
            _fakeReceiver.Setup(s => s.Address).Returns(ObjectMother.GetNewValidAddress());

            Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(_fakeSender.Object, _fakeReceiver.Object, null, _items);
            invoice.Issue(freightValue);

            invoice.Validate();
            invoice.Status.Should().Be(InvoiceStatus.ISSUED);
            invoice.IssueDate.HasValue.Should().BeTrue();
            invoice.KeyAccess.Should().NotBeNull();
            invoice.KeyAccess.Validate();
            invoice.InvoiceSender.Should().NotBeNull();
            invoice.InvoiceSender.Sender.Id.Should().Be(invoice.Sender.Id);
            invoice.InvoiceReceiver.Should().NotBeNull();
            invoice.InvoiceReceiver.Receiver.Id.Should().Be(invoice.Receiver.Id);
            invoice.InvoiceCarrier.Should().NotBeNull();
            invoice.InvoiceCarrier.Carrier.Id.Should().Be(invoice.Receiver.Id);
            invoice.InvoiceTax.Freight.Should().Be(freightValue);
            invoice.InvoiceTax.TotalValueInvoice.Should().Be(expectedInvoiceTotalValue);
            invoice.InvoiceTax.TotalValueProducts.Should().Be(expectedTotalValue);
            invoice.InvoiceTax.IpiValue.Should().Be(expectedIpiValue);
            invoice.InvoiceTax.IcmsValue.Should().Be(expectedIcmsValue);
        }
        
        [Test]
        public void Test_Invoice_IssueWithInvalidOpenedInvoice_ShouldThrowException()
        {
            double freightValue = 321.04;

            Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithEntryDateAfterNow(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);
            Action action = () => invoice.Issue(freightValue);
            action.Should().Throw<InvoiceEntryDateAfterNowException>();
        }

        [Test]
        public void Test_Invoice_IssueWithNegativeFreight_ShouldThrowException()
        {
            double freightValue = -1;

            Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);

            _fakeInvoiceItem.Setup(s => s.Consolidate());
            _fakeInvoiceItem.Setup(s => s.Validate()).Throws<InvoiceTaxNegativeFreightException>();

            Action action = () => invoice.Issue(freightValue);
            action.Should().Throw<InvoiceTaxNegativeFreightException>();
        }

        [Test]
        public void Test_Invoice_ValidateOpened_ShouldBeOk()
        {
            Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);
            
            invoice.Validate();
        }

        [Test]
        public void Test_Invoice_ValidateWithNullCarrier_ShouldBeOk()
        {
            Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(_fakeSender.Object, _fakeReceiver.Object, null, _items);

            invoice.Validate();
        }

        [Test]
        public void Test_Invoice_ValidateOpenedInvalidWithUninformedNatureOperation_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithUniformedNatureOperation(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceUninformedNatureOperationException>();
        }
        
        [Test]
        public void Test_Invoice_ValidateOpenedInvalidWithNatureOperationLengthOverflow_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNatureOperationLengthOverflow(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNaruteOperationLengthOverflowException>();
        }

        [Test]
        public void Test_Invoice_ValidateOpenedInvalidWithEntryDateAfterNow_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithEntryDateAfterNow(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceEntryDateAfterNowException>();
        }

        [Test]
        public void Test_Invoice_ValidateOpenedInvalidWithNonPositiveNumber_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNonPositiveNumber(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNonPositiveNumberException>();
        }

        [Test]
        public void Test_Invoice_ValidateOpenedInvalidWithNullReceiver_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNullReceiver(_fakeSender.Object, _fakeCarrier.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNullReceiverException>();
        }

        [Test]
        public void Test_Invoice_ValidateOpenedInvalidWithNullSender_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidOpenedInvoiceWithNullSender(_fakeReceiver.Object, _fakeCarrier.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNullSenderException>();
        }

        [Test]
        public void Test_Invoice_ValidateOpenedInvalidWithSenderInvalid_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);

            _fakeSender.Setup(s => s.Validate()).Throws<SenderNullCnpjException>();

            Action action = invoice.Validate;
            action.Should().Throw<SenderNullCnpjException>();
        }

        [Test]
        public void Test_Invoice_ValidateOpenedInvalidWithReceiverInvalid_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);

            _fakeSender.Setup(s => s.Validate()).Throws<ReceiverNullCnpjException>();

            Action action = invoice.Validate;
            action.Should().Throw<ReceiverNullCnpjException>();
        }


        [Test]
        public void Test_Invoice_ValidateWithReceiverEqualToSender_ShouldBeOk()
        {
            Mock<CNPJ> _fakeCNPJ = new Mock<CNPJ>();
            CNPJ cnpj = _fakeCNPJ.Object;

            _fakeSender.Setup(s => s.Cnpj).Returns(cnpj);
            _fakeReceiver.Setup(r => r.Cnpj).Returns(cnpj);
            _fakeReceiver.Setup(r => r.Type).Returns(PersonType.LEGAL);

            _fakeCNPJ.Setup(c => c.Equals(cnpj)).Returns(true);

            Invoice invoice = ObjectMother.GetNewValidOpenedInvoice(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceReceiverEqualThanSenderException>();
            _fakeReceiver.Verify(r => r.Cnpj);
            _fakeSender.Verify(s => s.Cnpj);
            _fakeCNPJ.Verify(c => c.Equals(cnpj));
        }

        [Test]
        public void Test_Invoice_ValidateIssued_ShouldBeOk()
        {
            Invoice invoice = ObjectMother.GetNewValidIssuedInvoice(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);

            invoice.Validate();
        }
        
        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithUninformedKeyAccess_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithNullKeyAccess(_fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNullKeyAccessException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithKeyAccessLengthDifferentThan44_ShouldThrowException()
        {
            _fakeKeyAccess.Setup(ka => ka.Validate()).Throws<KeyAccessUninformedValueException>();

            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithInvalidKeyAccess(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<KeyAccessUninformedValueException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithNullIssueDate_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithNullIssueDate(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNullIssueDateException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithIssueDateAfterNow_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithIssueDateAfterNow(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceIssueDateAfterNowException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithIssueDateBeforeEntryDate_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithIssueDateBeforeEntryDate(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceIssueDateBeforeEntryDateException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithNullInvoiceCarrier_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithNullInvoiceCarrier(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceTax.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNullInvoiceCarrierException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithNullInvoiceReceiver_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithNullInvoiceReceiver(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNullInvoiceReceiverException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithNullInvoiceSender_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithNullInvoiceSender(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNullInvoiceSenderException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithNullInvoiceTax_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithNullInvoiceTax(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _items);

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceNullInvoiceTaxException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithAnyInvoiceItem_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetInvalidIssuedInvoiceWithAnyInvoiceItem(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object);
            
            Action action = invoice.Validate;
            action.Should().Throw<InvoiceEmptyInvoiceItemsException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithInvoiceSenderInvalid_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetNewValidIssuedInvoice(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);
            _fakeInvoiceSender.Setup(s => s.Validate()).Throws<InvoiceSenderNullSenderException>();

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceSenderNullSenderException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithInvoiceReceiverInvalid_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetNewValidIssuedInvoice(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);
            _fakeInvoiceReceiver.Setup(s => s.Validate()).Throws<InvoiceReceiverNullReceiverException>();

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceReceiverNullReceiverException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithInvoiceCarrierInvalid_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetNewValidIssuedInvoice(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);
            _fakeInvoiceCarrier.Setup(s => s.Validate()).Throws<InvoiceCarrierNullCarrierException>();

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceCarrierNullCarrierException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithInvoiceTaxInvalid_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetNewValidIssuedInvoice(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);
            _fakeInvoiceTax.Setup(s => s.Validate()).Throws<InvoiceTaxNegativeFreightException>();

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceTaxNegativeFreightException>();
        }

        [Test]
        public void Test_Invoice_ValidateIssuedInvalidWithInvoiceItemInvalid_ShouldThrowException()
        {
            Invoice invoice = ObjectMother.GetNewValidIssuedInvoice(_fakeKeyAccess.Object, _fakeSender.Object, _fakeReceiver.Object, _fakeCarrier.Object,
                                                                    _fakeInvoiceSender.Object, _fakeInvoiceReceiver.Object, _fakeInvoiceCarrier.Object, _fakeInvoiceTax.Object, _items);

            _fakeInvoiceItem.Setup(s => s.Validate()).Throws<InvoiceItemInvalidAmountException>();

            Action action = invoice.Validate;
            action.Should().Throw<InvoiceItemInvalidAmountException>();
        }

    }
}
