using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
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
    public class AddressMapperTests
    {

        private IMapper<Address, EnderModel> _addressMapper;

        [SetUp]
        public void Initialize()
        {
            _addressMapper = new AddressMapper();
        }

        [Test]
        public void Test_AddressMapper_Map_ShouldBeOk()
        {
            Address address = ObjectMother.GetExistentValidAddress();

            EnderModel enderModel = _addressMapper.Map(address);

            enderModel.XLgr.Should().Be(address.StreetName);
            enderModel.Nro.Should().Be(address.Number);
            enderModel.XBairro.Should().Be(address.Neighborhood);
            enderModel.XMun.Should().Be(address.City);
            enderModel.UF.Should().Be(address.State);
            enderModel.XPais.Should().Be(address.Country);
        }

    }
}
