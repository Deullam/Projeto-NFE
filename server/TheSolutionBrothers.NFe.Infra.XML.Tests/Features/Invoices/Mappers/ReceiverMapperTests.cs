using FluentAssertions;
using Moq;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
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
    public class ReceiverMapperTests
    {

        private IMapper<Receiver, DestModel> _receiverMapper;

        private Mock<IMapper<Address, EnderModel>>_fakeAddressMapper;
        private Mock<Address> _dummyAddress;

        [SetUp]
        public void Initialize()
        {
            _dummyAddress = new Mock<Address>();
            _fakeAddressMapper = new Mock<IMapper<Address, EnderModel>>();
            _receiverMapper = new ReceiverMapper(_fakeAddressMapper.Object);
        }

        [Test]
        public void Test_ReceiverMapper_MapLegalPerson_ShouldBeOk()
        {
            Address address = _dummyAddress.Object;

            Mock<CNPJ> fakeCNPJ = new Mock<CNPJ>();
            CNPJ cnpj = fakeCNPJ.Object;

            Receiver receiver = ObjectMother.GetExistentValidLegalReceiver(address, cnpj);

            _fakeAddressMapper.Setup(am => am.Map(receiver.Address)).Returns(new EnderModel());
            fakeCNPJ.Setup(c => c.Value).Returns("50549009000124");

            DestModel destModel = _receiverMapper.Map(receiver);

            destModel.CNPJ.Should().Be(receiver.Cnpj.Value);
            destModel.CPF.Should().BeNull();
            destModel.XName.Should().Be(receiver.CompanyName);
            destModel.IE.Should().Be(receiver.StateRegistration);
            destModel.Ender.Should().NotBeNull();

            _fakeAddressMapper.Verify(am => am.Map(receiver.Address));
        }

        [Test]
        public void Test_ReceiverMapper_MapPhysicalPerson_ShouldBeOk()
        {
            Address address = _dummyAddress.Object;

            Mock<CPF> fakeCPF = new Mock<CPF>();
            CPF cpf = fakeCPF.Object;

            Receiver receiver = ObjectMother.GetExistentValidPhysicalReceiver(address, cpf);

            _fakeAddressMapper.Setup(am => am.Map(receiver.Address)).Returns(new EnderModel());
            fakeCPF.Setup(c => c.Value).Returns("52022932080");

            DestModel destModel = _receiverMapper.Map(receiver);

            destModel.CPF.Should().Be(receiver.Cpf.Value);
            destModel.CNPJ.Should().BeNull();
            destModel.XName.Should().Be(receiver.Name);
            destModel.IE.Should().Be(receiver.StateRegistration);
            destModel.Ender.Should().NotBeNull();

            _fakeAddressMapper.Verify(am => am.Map(receiver.Address));
        }

    }
}
