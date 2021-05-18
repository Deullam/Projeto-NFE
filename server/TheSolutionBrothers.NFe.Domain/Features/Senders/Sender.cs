using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using System;

namespace TheSolutionBrothers.NFe.Domain.Features.Senders
{
    public class Sender
    {
		public virtual long Id { get; set; }
		public virtual string FancyName { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual CNPJ Cnpj { get; set; }
        public virtual string StateRegistration { get; set; }
        public virtual string MunicipalRegistration { get; set; }
        public virtual Address Address { get; set; }
		public long AddressId { get; set; }

		public virtual void Validate()
        {
            if (String.IsNullOrEmpty(FancyName))
            {
                throw new SenderUninformedFancyNameException();
            }

            if (FancyName.Length > 60)
            {
                throw new SenderFancyNameLenghtOverflowException();
            }

            if (String.IsNullOrEmpty(CompanyName))
            {
                throw new SenderUninformedCompanyNameException();
            }

            if (CompanyName.Length > 60)
            {
                throw new SenderCompanyNameLenghtOverflowException();
            }

            if (Cnpj == null)
            {
                throw new SenderNullCnpjException();
            }

            Cnpj.Validate();

            if (String.IsNullOrEmpty(StateRegistration))
            {
                throw new SenderUninformedStateRegistrationException();
            }

            if (StateRegistration.Length > 15)
            {
                throw new SenderStateRegistrationLenghtOverflowException();
            }

            if (String.IsNullOrEmpty(MunicipalRegistration))
            {
                throw new SenderUninformedMunicipalRegistrationException();
            }

            if (MunicipalRegistration.Length > 15)
            {
                throw new SenderMunicipalRegistrationLenghtOverflowException();
            }

            if (Address == null)
            {
                throw new SenderNullAddressException();
            }

            Address.Validate();
        }
    }
}
