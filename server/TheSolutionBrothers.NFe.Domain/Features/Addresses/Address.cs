using TheSolutionBrothers.NFe.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Addresses
{
    public class Address
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Neighborhood { get; set; }
        public string State { get; set; }
        public string StreetName { get; set; }
        public int Number { get; set; }

        public virtual void Validate()
        {
            if (Number < 0)
                throw new AddressNegativeNumberException();


            if (string.IsNullOrEmpty(StreetName))
                throw new AddressUninformedStreetNameException();


            if (StreetName.Length > 60)
                throw new AddressStreetNameLengthOverflowException();


            if (string.IsNullOrEmpty(Neighborhood))
                throw new AddressUninformedNeighborhoodException();


            if (Neighborhood.Length > 40)
                throw new AddressNeighborhoodLengthOverflowException();


            if (string.IsNullOrEmpty(City))
                throw new AddressUninformedCityException();


            if (City.Length > 50)
                throw new AddressCityLengthOverflowException();


            if (string.IsNullOrEmpty(State))
                throw new AddressUninformedStateException();


            if (State.Length > 2)
                throw new AddressStateLengthOverflowException();


            if (string.IsNullOrEmpty(Country))
                throw new AddressUninformedCountryException();


            if (Country.Length > 50)
                throw new AddressCountryLengthOverflowException();

        }
    }
}
