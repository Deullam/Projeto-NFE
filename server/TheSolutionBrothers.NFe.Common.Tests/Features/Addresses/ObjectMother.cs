using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {

        public static AddessViewModel GetAddessViewModel()
        {
            return new AddessViewModel()
            {
                City = "cidade",
                Country = "país",
                Neighborhood = "bairro",
                Number = 123456,
                State = "SC",
                StreetName = "rua"
            };
        }

        public static AddressCommand GetValidAddresCommand()
        {
            return new AddressCommand()
            {
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "SC",
                Country = "Brasil"
            };
        }

		public static AddressCommand GetInvalidAddressCommandWithNegativeNumber()
		{
			return new AddressCommand()
			{
				StreetName = "Rua x de y",
				Number = -1,
				Neighborhood = "Guadalajara",
				City = "Lages",
				State = "SC",
				Country = "Brasil"
			};
		}

		public static Address GetNewValidAddress()
        {
            return new Address()
            {
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetExistentValidAddress()
        {
            return new Address()
            {
                Id = 1,
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetExistentValidAddressWithoutDependency()
        {
            return new Address()
            {
                Id = 2,
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithNegativeNumber()
        {
            return new Address()
            {
				Id = 1,
                StreetName = "Rua x de y",
                Number = -1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithUninformedStreetName()
        {
            return new Address()
            {
                StreetName = null,
                Number = 0,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithUninformedNeighborhood()
        {
            return new Address()
            {
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = null,
                City = "Lages",
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithUninformedCity()
        {
            return new Address()
            {
				Id = 1,
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = null,
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithUninformedState()
        {
            return new Address()
            {
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = null,
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithUninformedCountry()
        {
            return new Address()
            {
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "SC",
                Country = null
            };
        }

        public static Address GetInvalidAddressWithStreetNameLengthOverflow()
        {
            return new Address()
            {
                StreetName = "asdasasdasasdasasdasasdasasdasasdasasdasasdasasdasasdasasdasa",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithNeighborhoodNameLengthOverflow()
        {
            return new Address()
            {
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "asdasasdasasdasasdasasdasasdasasdasasdasa",
                City = "Lages",
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithCityNameLengthOverflow()
        {
            return new Address()
            {
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "asdasasdasasdasasdasasdasasdasasdasasdasasdasasdasa",
                State = "SC",
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithStateNameLengthOverflow()
        {
            return new Address()
            {
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "asdasasdasasdasasdasasdasasdasasdasasdasa",
                Country = "Brasil"
            };
        }

        public static Address GetInvalidAddressWithCountryNameLengthOverflow()
        {
            return new Address()
            {
                StreetName = "Rua x de y",
                Number = 1,
                Neighborhood = "Guadalajara",
                City = "Lages",
                State = "SC",
                Country = "asdasasdasasdasasdasasdasasdasasdasasdasasdasasdasa"
            };
        }
    }
}
