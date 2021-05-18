using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
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
    public class SenderMapperTests
    {

        private IMapper<Sender, EmitModel> _senderMapper;

        private Mock<IMapper<Address, EnderModel>>_fakeAddressMapper;
        private Mock<Address> _dummyAddress;
        private Mock<CNPJ> _fakeCNPJ;

        [SetUp]
        public void Initialize()
        {
            _dummyAddress = new Mock<Address>();
            _fakeCNPJ = new Mock<CNPJ>();
            _fakeAddressMapper = new Mock<IMapper<Address, EnderModel>>();
            _senderMapper = new SenderMapper(_fakeAddressMapper.Object);
        }

        [Test]
        public void Test_SenderMapper_Map_ShouldBeOk()
        {
            Address address = _dummyAddress.Object;
            CNPJ cnpj = _fakeCNPJ.Object;

            Sender sender = ObjectMother.GetExistentValidSender(address, cnpj);

            _fakeAddressMapper.Setup(am => am.Map(sender.Address)).Returns(new EnderModel());
            _fakeCNPJ.Setup(c => c.Value).Returns("50549009000124");

            EmitModel emitModel = _senderMapper.Map(sender);

            emitModel.CNPJ.Should().Be(sender.Cnpj.Value);
            emitModel.XName.Should().Be(sender.CompanyName);
            emitModel.XFant.Should().Be(sender.FancyName);
            emitModel.IE.Should().Be(sender.StateRegistration);
            emitModel.IM.Should().Be(sender.MunicipalRegistration);
            emitModel.Ender.Should().NotBeNull();

            _fakeAddressMapper.Verify(am => am.Map(sender.Address));
        }

    }
}
