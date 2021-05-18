using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
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
    public class InvoiceItemMapperTests
    {

        private IMapper<InvoiceItem, DetModel> _invoiceItemMapper;

        [SetUp]
        public void Initialize()
        {
            _invoiceItemMapper = new InvoiceItemMapper();
        }

        [Test]
        public void Test_InvoiceItemMapper_Map_ShouldBeOk()
        {
            InvoiceItem invoiceItem = ObjectMother.GetExistentInvoinceItemOk(null);

            DetModel det = _invoiceItemMapper.Map(invoiceItem);

            det.Prod.CProd.Should().Be(invoiceItem.Code);
            det.Prod.XProd.Should().Be(invoiceItem.Description);
            det.Prod.QCom.Should().Be(string.Format("{0:0.00000}", invoiceItem.Amount));
            det.Prod.VUmCom.Should().Be(string.Format("{0:0.00}", invoiceItem.UnitValue));
            det.Prod.VProd.Should().Be(string.Format("{0:0.00}", invoiceItem.TotalValue));
            det.Imposto.Icms.Icms00.PIcms.Should().Be(string.Format("{0:0.00}", (invoiceItem.IcmsAliquot * 100)));
            det.Imposto.Icms.Icms00.VIcms.Should().Be(string.Format("{0:0.00}", invoiceItem.IcmsValue));
        }

    }
}
