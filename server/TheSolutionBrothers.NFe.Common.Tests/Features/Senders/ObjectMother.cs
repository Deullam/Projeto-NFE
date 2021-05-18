using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Senders.Commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Senders.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Senders.Queries;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static Sender GetNewValidSender(Address address, CNPJ cnpj)
        {
            return new Sender()
            {
                FancyName = "Nome fantasia.",
                CompanyName = "Nome da companhia.",
                MunicipalRegistration = "123456789012",
                StateRegistration = "123456789012",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetExistentValidSender(Address address, CNPJ cnpj)
        {
            return new Sender()
            {
                Id = 1,
                FancyName = "Nome fantasia.",
                CompanyName = "Nome da companhia.",
                MunicipalRegistration = "123456789012",
                StateRegistration = "123456789012",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetExistentValidSenderWithoutDependecy(Address address, CNPJ cnpj)
        {
            return new Sender()
            {
                Id = 2,
                FancyName = "Nome fantasia.",
                CompanyName = "Nome da companhia.",
                MunicipalRegistration = "123456789012",
                StateRegistration = "123456789012",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithUninformedFancyName(Address address, CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = null,
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "123456789101",
                StateRegistration = "110987654321",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithUninformedCompanyName(Address address, CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = "Nome da fantasia companhia",
                CompanyName = null,
                MunicipalRegistration = "123456789101",
                StateRegistration = "110987654321",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithUninformedStateRegistration(Address address, CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = "Nome da fantasia companhia",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "123456789101",
                StateRegistration = null,
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithNullCnpj(Address address)
        {
            return new Sender
            {
                FancyName = "Nome da fantasia companhia",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "123456789101",
                StateRegistration = "123456789012",
                Cnpj = null,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithCnpjValueNull(Address address, CNPJ nullCnpj)
        {
            return new Sender
            {
                FancyName = "Nome da fantasia companhia",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "123456789101",
                StateRegistration = "123456789012",
                Cnpj = nullCnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithInvalidCnpj(Address address)
        {
            return new Sender
            {
                FancyName = "Nome da fantasia companhia",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "123456789101",
                StateRegistration = "123456789012",
                Cnpj = new CNPJ { Value = "123456789012345" },
                Address = address
            };
        }

        public static Sender GetInvalidSenderCnpjWithInvalidValue(Address address, CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = "Nome da fantasia companhia",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "123456789101",
                StateRegistration = "123456789012",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithUninformedMunicipalRegistration(Address address, CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = "Nome da fantasia companhia",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = null,
                StateRegistration = "123456789101",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithFancyNameLenghtOverflow(Address address, CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "123456789101",
                StateRegistration = "110987654321",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithCompanyNameLenghtOverflow(Address address, CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = "Nome fantasia da companhia",
                CompanyName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab",
                MunicipalRegistration = "123456789012",
                StateRegistration = "110987654321",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithStateRegistrationLenghtOverflow(Address address, CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = "Nome fantasia da companhia",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "123456789012",
                StateRegistration = "1234567890123456",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithMunicipalRegistrationLenghtOverflow(Address address, CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = "Nome fantasia da companhia",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "1234567890123456",
                StateRegistration = "123456789012",
                Cnpj = cnpj,
                Address = address
            };
        }

        public static Sender GetInvalidSenderWithNullAddress(CNPJ cnpj)
        {
            return new Sender
            {
                FancyName = "Nome fantasia da companhia",
                CompanyName = "Nome da companhia",
                MunicipalRegistration = "123456789012",
                StateRegistration = "123456789012",
                Cnpj = cnpj,
                Address = null
            };
        }

        public static Sender GetInvalidSenderWithInvalidAddress(Address invalidAddress, CNPJ cnpj)
        {
            return new Sender()
            {
                FancyName = "Nome fantasia.",
                CompanyName = "Nome da companhia.",
                MunicipalRegistration = "123456789012",
                StateRegistration = "123456789012",
                Cnpj = cnpj,
                Address = invalidAddress
            };
        }

		public static SenderRegisterCommand GetNewValidSenderRegisterCommand(AddressCommand address, CNPJ cnpj)
		{
			return new SenderRegisterCommand()
			{
				FancyName = "Nome fantasia",
				CompanyName = "Razão social",
				Cnpj = cnpj.Value,
				StateRegistration = "123456789012",
				MunicipalRegistration = "123456789012",
				Address = address
			};
		}


		public static SenderRegisterCommand GetInvalidSenderRegisterCommandWithUninformedCompanyName(AddressCommand address, CNPJ cnpj)
		{
			return new SenderRegisterCommand()
			{
				FancyName = "",
				CompanyName = "Razão social",
				Cnpj = cnpj.Value,
				StateRegistration = "123456789012",
				MunicipalRegistration = "123456789012",
				Address = address
			};
		}

		public static SenderRegisterCommand GetNewInvalidSenderRegisterCommandWithNullAddress(CNPJ cnpj)
		{
			return new SenderRegisterCommand()
			{
				FancyName = "Nome fantasia",
				CompanyName = "Razão social",
				Cnpj = cnpj.Value,
				StateRegistration = "123456789012",
				MunicipalRegistration = "123456789012",
				Address = null
			};
		}

		public static SenderRegisterCommand GetNewInvalidSenderRegisterCommanWithAddressInvalid(AddressCommand addressInvalid, CNPJ cnpj)
		{
			return new SenderRegisterCommand()
			{
				FancyName = "Nome fantasia",
				CompanyName = "Razão social",
				Cnpj = cnpj.Value,
				StateRegistration = "123456789012",
				MunicipalRegistration = "123456789012",
				Address = addressInvalid
			};
		}

		public static SenderUpdateCommand GetExistentValidSenderCommandToUpdate(AddressCommand address, CNPJ cnpj)
		{
			return new SenderUpdateCommand()
			{
				Id = 1,
				FancyName = "Nome fantasia",
				CompanyName = "Razão social",
				Cnpj = cnpj.Value,
				StateRegistration = "123456789012",
				MunicipalRegistration = "123456789012",
				Address = address
			};
		}

        public static SenderUpdateCommand GetNewInvalidSenderUpdateCommandWithUninformedCompanyName(AddressCommand address, CNPJ cnpj)
        {
            return new SenderUpdateCommand()
            {
                Id = 1,
                FancyName = "Nome fantasia",
                CompanyName = "",
                Cnpj = cnpj.Value,
                StateRegistration = "123456789012",
                MunicipalRegistration = "123456789012",
                Address = address
            };
        }

        public static SenderUpdateCommand GetExistentValidSenderCommandToUpdateWithInvalidId(AddressCommand address, CNPJ cnpj)
		{
			return new SenderUpdateCommand()
			{
				Id = -1,
				FancyName = "Nome fantasia",
				CompanyName = "Razão social",
				Cnpj = cnpj.Value,
				StateRegistration = "123456789012",
				MunicipalRegistration = "123456789012",
				Address = address
			};
		}

		public static SenderDeleteCommand GetSenderCommandToDelete(long[] senders)
		{
			return new SenderDeleteCommand()
			{
				SenderIds = senders
			};
		}

		public static SenderViewModel GetValidSenderViewModel(AddessViewModel address, CNPJ cnpj)
		{
			return new SenderViewModel()
			{
				Id = 1,
				FancyName = "Nome fantasia",
				CompanyName = "Razão social",
				Cnpj = cnpj.Value,
				StateRegistration = "123456789012",
				MunicipalRegistration = "123456789012",
				Address = address
			};
		}

		public static SenderGetAllQuery GetExistentValidSenderGetAllQuery(int size)
		{
			return new SenderGetAllQuery(size)
			{
				Size = size
			};
		}

        public static SenderDeleteCommand GetSenderDeleteCommand()
        {
            return new SenderDeleteCommand()
            {
                SenderIds = new long[] { 1 }
            };
        }

        public static SenderDeleteCommand GetSenderDeleteCommandWithNonExistentIds()
        {
            return new SenderDeleteCommand()
            {
                SenderIds = new long[] { 100, 200 }
            };
        }

        public static SenderDeleteCommand GetsenderDeleteCommandWithoutId()
        {
            return new SenderDeleteCommand()
            {
                SenderIds = new long[] { }
            };
        }
    }
}
