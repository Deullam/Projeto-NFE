using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
    public class Receiver
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string StateRegistration { get; set; }
        public virtual CPF Cpf { get; set; }
        public virtual CNPJ Cnpj { get; set; }
        public virtual PersonType Type { get; set; }
        public virtual Address Address { get; set; }
        public long AddressId { get; set; }

        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new ReceiverUninformedNameException();
            }

            if (Name.Length > 60)
            {
                throw new ReceiverNameLengthOverflowException();
            }

            if (PersonType.PHYSICAL == Type)
            {
                if (Cpf == null)
                {
                    throw new ReceiverNullCpfException();
                }

                Cpf.Validate();

            }

            if (PersonType.LEGAL == Type)
            {
                if (Cnpj == null)
                {
                    throw new ReceiverNullCnpjException();
                }

                Cnpj.Validate();

                if (string.IsNullOrEmpty(CompanyName))
                {
                    throw new ReceiverUninformedCompanyNameException();
                }

                if (CompanyName.Length > 60)
                {
                    throw new ReceiverCompanyNameLengthOverflowException();
                }

                if (string.IsNullOrEmpty(StateRegistration))
                {
                    throw new ReceiverUninformedStateRegistrationException();
                }

                if (StateRegistration.Length > 15)
                {
                    throw new ReceiverStateRegistrationLengthOverflowException();
                }

            }

            if (Address == null)
            {
                throw new ReceiverUninformedAddressException();
            }

            Address.Validate();

        }
    }
}
