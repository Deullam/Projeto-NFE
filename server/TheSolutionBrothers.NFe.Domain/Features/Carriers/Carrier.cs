using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using System;

namespace TheSolutionBrothers.NFe.Domain.Features.Carriers
{
    public class Carrier
    {

        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual CPF CPF { get; set; }
        public virtual CNPJ CNPJ { get; set; }
        public virtual string StateRegistration { get; set; }
        public virtual FreightResponsability FreightResponsability { get; set; }
        public virtual PersonType PersonType { get; set; }
        public virtual Address Address { get; set; }
        public long AddressId { get; set; }


        public virtual void Validate()
        {
            if (String.IsNullOrEmpty(Name))
            {
                throw new CarrierUninformedNameException();
            }

            if (Name.Length > 60)
            {
                throw new CarrierNameLenghtOverflowException();
            }

            if (PersonType == PersonType.PHYSICAL)
            {
                if (CPF ==null)
                {
                    throw new CarrierNullCPFException();
                }

                CPF.Validate();
            }
            else if (PersonType == PersonType.LEGAL)
            {
                if (String.IsNullOrEmpty(CompanyName))
                {
                    throw new CarrierUninformedCompanyNameException();
                }

                if (CompanyName.Length > 60)
                {
                    throw new CarrierCompanyNameLenghtOverflowException();
                }

                if (CNPJ == null)
                {
                    throw new CarrierNullCNPJException();
                }

                CNPJ.Validate();

                if (String.IsNullOrEmpty(StateRegistration))
                {
                    throw new CarrierUninformedStateRegistrationException();
                }

                if (StateRegistration.Length > 15)
                {
                    throw new CarrierStateRegistrationLenghtOverflowException();
                }

            }
            else
            {
                throw new CarrierUninformedPersonTypeException();
            }

            if (FreightResponsability == FreightResponsability.INVALID)
            {
                throw new CarrierUninformedFreightResponsabilityException();
            }

            if (Address == null)
            {
                throw new CarrierNullAddressException();
            }

            Address.Validate();
        }
    }
}
