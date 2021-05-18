using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
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
    public class InvoiceTaxMapperTests
    {

        private IMapper<InvoiceTax, TotalModel> _invoiceTaxMapper;

        [SetUp]
        public void Initialize()
        {
            _invoiceTaxMapper = new InvoiceTaxMapper();
        }

        [Test]
        public void Test_InvoiceTaxMapper_Map_ShouldBeOk()
        {
            InvoiceTax address = ObjectMother.GetExistentValidInvoiceTax(null);

            TotalModel totalModel = _invoiceTaxMapper.Map(address);

            totalModel.ICMSTot.VICMS.Should().Be(string.Format("{0:0.00}", address.IcmsValue));
            totalModel.ICMSTot.VIPI.Should().Be(string.Format("{0:0.00}", address.IpiValue));
            totalModel.ICMSTot.VNF.Should().Be(string.Format("{0:0.00}", address.TotalValueProducts));
            totalModel.ICMSTot.VTotTrib.Should().Be(string.Format("{0:0.00}", address.TotalValueInvoice));
            totalModel.ICMSTot.VFrete.Should().Be(string.Format("{0:0.00}", address.Freight));
        }

    }
}
