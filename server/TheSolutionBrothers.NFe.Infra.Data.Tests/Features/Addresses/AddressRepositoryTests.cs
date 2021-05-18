using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Base;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Infra.Data.Features.Addresses;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Features.Addresses
{

    [TestFixture]
    public class AddressRepositoryTests
    {

        private IAddressRepository _repository;
        
        //[SetUp]
        //public void Initialize()
        //{
        //    BaseSqlTest.SeedDatabase();
        //    _repository = new AddressRepository();
        //}

        //[Test]
        //public void Test_AddressRepository_Save_ShouldBeOk()
        //{
        //    Address address = ObjectMother.GetNewValidAddress();
        //    address = _repository.Save(address);
        //    address.Id.Should().BeGreaterThan(0);
        //}

        //[Test]
        //public void Test_AddressRepository_SaveInvalid_ShouldThrowException()
        //{
        //    Address address = ObjectMother.GetInvalidAddressWithUninformedNeighborhood();
        //    Action action = () => _repository.Save(address);
        //    action.Should().Throw<BusinessException>();
        //}

        //[Test]
        //public void Test_AddressRepository_Update_ShouldBeOk()
        //{
        //    Address address = ObjectMother.GetExistentValidAddress();

        //    address = _repository.Update(address);

        //    Address result = _repository.Get(address.Id);
        //    result.Should().NotBeNull();
        //    result.StreetName.Should().Be(address.StreetName);
        //}

        //[Test]
        //public void Test_AddressRepository_UpdateInvalid_ShouldThrowException()
        //{
        //    long existentId = 1;
        //    Address address = ObjectMother.GetInvalidAddressWithUninformedNeighborhood();
        //    address.Id = existentId;

        //    Action action = () => _repository.Update(address);
        //    action.Should().Throw<BusinessException>();

        //    Address result = _repository.Get(address.Id);
        //    result.StreetName.Should().NotBe(address.StreetName);
        //}

        //[Test]
        //public void Test_AddressRepository_UpdateInvalidId_ShouldThrowException()
        //{
        //    Address address = ObjectMother.GetNewValidAddress();
        //    Action action = () => _repository.Update(address);
        //    action.Should().Throw<IdentifierUndefinedException>();
        //}

        //[Test]
        //public void Test_AddressRepository_Delete_ShouldBeOk()
        //{
        //    Address address = ObjectMother.GetExistentValidAddressWithoutDependency();
        //    _repository.Delete(address);
        //    _repository.Get(address.Id).Should().BeNull();
        //}

        //[Test]
        //public void Test_AddressRepository_DeleteInvalidId_ShouldThrowException()
        //{
        //    Address address = ObjectMother.GetNewValidAddress();

        //    Action action = () => _repository.Delete(address);
        //    action.Should().Throw<IdentifierUndefinedException>();
        //}

        //[Test]
        //public void Test_AddressRepository_GetById_ShouldBeOk()
        //{
        //    long existentId = 1;
        //    Address address = _repository.Get(existentId);
        //    address.Should().NotBeNull();
        //}

        //[Test]
        //public void Test_AddressRepository_GetByIdInvalid_ShouldThrowException()
        //{
        //    long invalidId = -1;
        //    Action action = () => _repository.Get(invalidId);
        //    action.Should().Throw<IdentifierUndefinedException>();
        //}

        //[Test]
        //public void Test_AddressRepository_GetNonexistentId_ShouldBeOk()
        //{
        //    long nonexistentId = 100;
        //    Address address = _repository.Get(nonexistentId);
        //    address.Should().BeNull();
        //}

        //[Test]
        //public void Test_AddressRepository_GetAll_ShouldBeOk()
        //{
        //    int expectedCount = 2;
        //    IList<Address> addresses = _repository.GetAll();
        //    addresses.Count.Should().Be(expectedCount);
        //}

    }
}
