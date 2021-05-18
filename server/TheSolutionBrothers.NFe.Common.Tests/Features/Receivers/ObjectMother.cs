using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Receivers.Queries;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
	public static partial class ObjectMother
	{

        public static ReceiverDeleteCommand GetReceiverDeleteCommand()
        {
            return new ReceiverDeleteCommand()
            {
                ReceiverIds = new long[] { 1, 2 }
            };
        }

        public static ReceiverDeleteCommand GetReceiverDeleteCommandWithNonExistentIds()
        {
            return new ReceiverDeleteCommand()
            {
                ReceiverIds = new long[] { 100, 200 }
            };
        }

        public static ReceiverDeleteCommand GetReceiverDeleteCommandWithoutId()
        {
            return new ReceiverDeleteCommand()
            {
                ReceiverIds = new long[] { }
            };
        }

        public static ReceiverRegisterCommand GetValidPhysicalReceiverRegisterCommand(AddressCommand address, CPF cpf)
        {

            return new ReceiverRegisterCommand()
            {
                Address = address,
                Cpf = cpf.Value,
                CompanyName = null,
                Cnpj = null,
                StateRegistration = null,
                PersonType = PersonType.PHYSICAL,
                Name = "Nome teste"
            };
        }


		public static Receiver GetNewValidPhysicalReceiver(Address address, CPF cpf)
		{
			return new Receiver()
			{
				Name = "Nome teste",
				CompanyName = null,
				Cpf = cpf,
				Cnpj = new CNPJ(),
				StateRegistration = null,
				Type = PersonType.PHYSICAL,
				Address = address
			};
		}

        public static ReceiverRegisterCommand GetValidLegalReceiverRegisterCommand(AddressCommand address, CNPJ cnpj)
        {

            return new ReceiverRegisterCommand()
            {
                Address = address,
                Cpf = "",
                CompanyName = "Razao Social Teste",
                Cnpj = cnpj.Value,
                StateRegistration = "588675245",
                PersonType = PersonType.LEGAL,
                Name = "Nome teste"
            };
        }

        public static ReceiverRegisterCommand GetInvalidLegalReceiverRegisterCommandWithUninformedName(AddressCommand address, CNPJ cnpj)
        {

            return new ReceiverRegisterCommand()
            {
                Address = address,
                Cpf = "",
                CompanyName = "Razao Social Teste",
                Cnpj = cnpj.Value,
                StateRegistration = "588675245",
                PersonType = PersonType.PHYSICAL,
                Name = ""
            };
        }


        public static Receiver GetNewValidLegalReceiver(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Name = "Nome teste",
				CompanyName = "Razao Social Teste",
				Cpf = new CPF(),
				Cnpj = cnpj,
				StateRegistration = "588675245",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

        public static ReceiverUpdateCommand GetExistentValidPhysicalReceiverUpdateCommand(AddressCommand address, CPF cpf)
        {
            return new ReceiverUpdateCommand()
            {
                Id = 1,
                Name = "Nome teste",
                CompanyName = null,
                Cpf = cpf.Value,
                Cnpj = null,
                StateRegistration = null,
                PersonType = PersonType.PHYSICAL,
                Address = address
            };
        }

		public static Receiver GetExistentValidPhysicalReceiver(Address address, CPF cpf)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = null,
				Cpf = cpf,
				Cnpj = new CNPJ(),
				StateRegistration = null,
				Type = PersonType.PHYSICAL,
				Address = address
			};
		}

        public static ReceiverUpdateCommand GetExistentValidLegalReceiverUpdateCommand(AddressCommand address, CNPJ cnpj)
        {
            return new ReceiverUpdateCommand()
            {
                Id = 1,
                Name = "Nome teste",
                CompanyName = "Razao Social Teste",
                Cpf = null,
                Cnpj = cnpj.Value,
                StateRegistration = "588675245",
                PersonType = PersonType.LEGAL,
                Address = address
            };
        }

        public static ReceiverUpdateCommand GetLegalReceiverUpdateCommandWithUninformedName(AddressCommand address, CNPJ cnpj)
        {
            return new ReceiverUpdateCommand()
            {
                Id = 1,
                Name = "",
                CompanyName = "Razao Social Teste",
                Cpf = null,
                Cnpj = cnpj.Value,
                StateRegistration = "588675245",
                PersonType = PersonType.LEGAL,
                Address = address
            };
        }

        public static ReceiverViewModel GetPhysicalReceiverViewModel(AddessViewModel address, CPF cpf)
        {
            return new ReceiverViewModel()
            {
                Id = 1,
                Address = address,
                Name = "Nome teste",
                CompanyName = null,
                Cpf = cpf.Value,
                Cnpj = null,
                StateRegistration = null,
                PersonType = PersonType.PHYSICAL.ToString(),
            };
        }

        public static ReceiverGetAllQuery GetValidReceiverGetAllQuery()
        {
            var size = 2;
            return new ReceiverGetAllQuery(size)
            {
            };
        }


        public static Receiver GetExistentValidLegalReceiver(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "Razao Social Teste",
				Cpf = new CPF(),
				Cnpj = cnpj,
				StateRegistration = "588675245",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidPhysicalReceiverEmptyCpf(Address address, CPF cpf)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = null,
				Cpf = cpf,
				Cnpj = null,
				StateRegistration = null,
				Type = PersonType.PHYSICAL,
				Address = address
			};
		}

		public static Receiver GetInvalidPhysicalReceiverNullCpf(Address address)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = null,
				Cpf = null,
				Cnpj = null,
				StateRegistration = null,
				Type = PersonType.PHYSICAL,
				Address = address
			};
		}

		public static Receiver GetInvalidPhysicalReceiverCpfInvalid(Address address, CPF cpf)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = null,
				Cpf = cpf,
				Cnpj = null,
				StateRegistration = null,
				Type = PersonType.PHYSICAL,
				Address = address
			};
		}

		public static Receiver GetInvalidPhysicalReceiverEmptyName(Address address, CPF cpf)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "",
				CompanyName = null,
				Cpf = cpf,
				Cnpj = null,
				StateRegistration = null,
				Type = PersonType.PHYSICAL,
				Address = address
			};
		}

		public static Receiver GetInvalidPhysicalReceiverNullName(Address address, CPF cpf)
		{
			return new Receiver()
			{
				Id = 1,
				Name = null,
				CompanyName = null,
				Cpf = cpf,
				Cnpj = null,
				StateRegistration = null,
				Type = PersonType.PHYSICAL,
				Address = address
			};
		}

		public static Receiver GetInvalidPhysicalReceiverNameLength(Address address, CPF cpf)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "0123456789012345678901234567890123456789012345678901234567890",
				CompanyName = null,
				Cpf = cpf,
				Cnpj = null,
				StateRegistration = null,
				Type = PersonType.PHYSICAL,
				Address = address

			};
		}

		public static Receiver GetInvalidPhysicalReceiverNullAddress(CPF cpf)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = null,
				Cpf = cpf,
				Cnpj = null,
				StateRegistration = null,
				Type = PersonType.PHYSICAL,
				Address = null
			};
		}

        public static Receiver GetInvalidLegalReceiverNullName(Address address, CNPJ cnpj)
        {
            return new Receiver()
            {
                Id = 1,
                Name = null,
                CompanyName = null,
                Cpf = null,
                Cnpj = cnpj,
                StateRegistration = null,
                Type = PersonType.LEGAL,
                Address = address
            };
        }

        public static Receiver GetInvalidLegalReceiverNameLength(Address address, CNPJ cnpj)
        {
            return new Receiver()
            {
                Id = 1,
                Name = "0123456789012345678901234567890123456789012345678901234567890",
                CompanyName = null,
                Cpf = null,
                Cnpj = cnpj,
                StateRegistration = null,
                Type = PersonType.PHYSICAL,
                Address = address

            };
        }

        public static Receiver GetInvalidLegalReceiverEmptyCnpj(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "Razao Social Teste",
				Cpf = null,
				Cnpj = cnpj,
				StateRegistration = "588675245",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidLegalReceiverNullCnpj(Address address)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "Razao Social Teste",
				Cpf = null,
				Cnpj = null,
				StateRegistration = "588675245",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidLegalReceiverCnpjInvalid(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "Razao Social Teste",
				Cpf = null,
				Cnpj = cnpj,
				StateRegistration = "588675245",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidLegalReceiverEmptyCompanyName(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "",
				Cpf = null,
				Cnpj = cnpj,
				StateRegistration = "588675245",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidLegalReceiverNullCompanyName(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = null,
				Cpf = null,
				Cnpj = cnpj,
				StateRegistration = "588675245",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidLegalReceiverCompanyNameLengthOverflow(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "0123456789012345678901234567890123456789012345678901234567890",
				Cpf = null,
				Cnpj = cnpj,
				StateRegistration = "588675245",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidLegalReceiverEmptyStateRegistration(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "Razao Social Teste",
				Cpf = null,
				Cnpj = cnpj,
				StateRegistration = "",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidLegalReceiverNullStateRegistration(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "Razao Social Teste",
				Cpf = null,
				Cnpj = cnpj,
				StateRegistration = null,
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidLegalReceiverStateRegistrationLengthOverflow(Address address, CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "Razao Social Teste",
				Cpf = null,
				Cnpj = cnpj,
				StateRegistration = "01234567890123456789",
				Type = PersonType.LEGAL,
				Address = address
			};
		}

		public static Receiver GetInvalidLegalReceiverNullAddress(CNPJ cnpj)
		{
			return new Receiver()
			{
				Id = 1,
				Name = "Nome teste",
				CompanyName = "Razao Social Teste",
				Cpf = null,
				Cnpj = cnpj,
				StateRegistration = "588675245",
				Type = PersonType.LEGAL,
				Address = null
			};
		}

	}
}
