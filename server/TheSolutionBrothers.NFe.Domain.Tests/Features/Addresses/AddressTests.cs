using FluentAssertions;
using TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Tests.Features.Addresses
{

    [TestFixture]
    public class AddressTests
    {

        [Test]
        public void Test_Address_Validate_ShouldBeOk()
        {
            Address address = ObjectMother.GetNewValidAddress();
            address.Validate();
        }

        [Test]
        public void Test_Address_ValidateNegative_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithNegativeNumber();
            Action action = address.Validate;
            action.Should().Throw<AddressNegativeNumberException>();
        }

        [Test]
        public void Test_Address_ValidateWithUninformedStreetName_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithUninformedStreetName();

            Action action = address.Validate;
            action.Should().Throw<AddressUninformedStreetNameException>();
        }

        [Test]
        public void Test_Address_ValidateWithUninformedNeighborhood_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithUninformedNeighborhood();

            Action action = address.Validate;
            action.Should().Throw<AddressUninformedNeighborhoodException>();
        }

        [Test]
        public void Test_Address_ValidateWithUninformedCity_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithUninformedCity();

            Action action = address.Validate;
            action.Should().Throw<AddressUninformedCityException>();
        }

        [Test]
        public void Test_Address_ValidateWithUninformedState_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithUninformedState();

            Action action = address.Validate;
            action.Should().Throw<AddressUninformedStateException>();
        }

        [Test]
        public void Test_Address_ValidateWithUninformedCountry_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithUninformedCountry();

            Action action = address.Validate;
            action.Should().Throw<AddressUninformedCountryException>();
        }

        [Test]
        public void Test_Address_ValidateWithStreetNameLengthOverflow_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithStreetNameLengthOverflow();

            Action action = address.Validate;
            action.Should().Throw<AddressStreetNameLengthOverflowException>();
        }

        [Test]
        public void Test_Address_ValidateWithNeighborhoodNameLengthOverflow_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithNeighborhoodNameLengthOverflow();

            Action action = address.Validate;
            action.Should().Throw<AddressNeighborhoodLengthOverflowException>();
        }

        [Test]
        public void Test_Address_ValidateWithCityNameLengthOverflow_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithCityNameLengthOverflow();

            Action action = address.Validate;
            action.Should().Throw<AddressCityLengthOverflowException>();
        }

        [Test]
        public void Test_Address_ValidateWithStateNameLengthOverflow_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithStateNameLengthOverflow();

            Action action = address.Validate;
            action.Should().Throw<AddressStateLengthOverflowException>();
        }

        [Test]
        public void Test_Address_ValidateWithCountryNameLengthOverflow_ShouldThrowException()
        {
            Address address = ObjectMother.GetInvalidAddressWithCountryNameLengthOverflow();

            Action action = address.Validate;
            action.Should().Throw<AddressCountryLengthOverflowException>();
        }

    }
}