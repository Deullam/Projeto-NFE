using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Queries;
using TheSolutionBrothers.NFe.Application.Features.Carriers.Commands;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static CarrierDeleteCommand GetCarrierDeleteCommand()
        {
            return new CarrierDeleteCommand()
            {
                CarrierIds = new long[] { 1, 2 }
            };
        }

        public static CarrierDeleteCommand GetCarrierDeleteCommandWithNonExistentIds()
        {
            return new CarrierDeleteCommand()
            {
                CarrierIds = new long[] { 100, 200 }
            };
        }

        public static CarrierDeleteCommand GetCarrierDeleteCommandWithoutId()
        {
            return new CarrierDeleteCommand()
            {
                CarrierIds = new long[] { }
            };
        }

        public static Carrier GetNewValidCarrierPhysical(Address address, CPF cpf)
        {
            return new Carrier()
            {
                Name = "Solution Brothers",
                CPF = cpf,
                CNPJ = new CNPJ(),
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.PHYSICAL,
                Address = address
            };
        }

        public static CarrierRegisterCommand GetValidPhysicalCarrierRegisterCommand(AddressCommand address, CPF cpf)
        {

            return new CarrierRegisterCommand()
            {
                Address = address,
                CPF = cpf.Value,
                CompanyName = "",
                CNPJ = "",
                StateRegistration = "",
                PersonType = PersonType.PHYSICAL,
                Name = "Nome teste",
                FreightResponsability = FreightResponsability.RECEIVER,
            };
        }

        public static CarrierRegisterCommand GetInvalidPhysicalCarrierRegisterCommandWithNullAddress(CPF cpf)
        {

            return new CarrierRegisterCommand()
            {
                Address = null,
                CPF = cpf.Value,
                CompanyName = "",
                CNPJ = "",
                StateRegistration = "",
                PersonType = PersonType.PHYSICAL,
                Name = "Nome teste",
                FreightResponsability = FreightResponsability.RECEIVER,
            };
        }

        public static Carrier GetNewValidCarrierLegal(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "Solutions Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = cnpj,
                CPF = new CPF(),
                StateRegistration = "11234567890",
                FreightResponsability = FreightResponsability.RECEIVER,
                PersonType = PersonType.LEGAL,
                Address = address
            };
        }

        public static CarrierRegisterCommand GetValidLegalCarrierRegisterCommand(AddressCommand address, CNPJ cnpj)
        {

            return new CarrierRegisterCommand()
            {
                Address = address,
                CPF = "",
                CompanyName = "Razao Social Teste",
                CNPJ = cnpj.Value,
                StateRegistration = "588675245",
                PersonType = PersonType.LEGAL,
                Name = "Nome teste",
                FreightResponsability = FreightResponsability.RECEIVER,
            };
        }

        public static CarrierRegisterCommand GetInvalidLegalCarrierRegisterCommandWithUninformdName(AddressCommand address, CNPJ cnpj)
        {

            return new CarrierRegisterCommand()
            {
                Address = address,
                CPF = "",
                CompanyName = "Razao Social Teste",
                CNPJ = cnpj.Value,
                StateRegistration = "588675245",
                PersonType = PersonType.PHYSICAL,
                Name = "",
                FreightResponsability = FreightResponsability.RECEIVER,
            };
        }


        public static Carrier GetInvalidCarrierLegalNameEmpty(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "",
                CNPJ = cnpj,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address,
            };
        }

        public static Carrier GetInvalidCarrierPhysicalNameEmpty(Address address, CPF cpf)
        {
            return new Carrier()
            {
                Name = "",
                CPF = cpf,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.PHYSICAL,
                Address = address,
            };
        }

        public static Carrier GetInvalidCarrierNameEmptyToSave(Address address, CPF cpf)
        {
            return new Carrier()
            {
                Name = "",
                CPF = cpf,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.PHYSICAL,
                Address = address
            };
        }

        public static Carrier GetExistentValidCarrierPhysical(Address address, CPF cpf)
        {
            return new Carrier()
            {
                Id = 1,
                Name = "Solutions Brothers",
                CPF = cpf,
                CNPJ = new CNPJ(),
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.PHYSICAL,
                Address = address,
            };
        }
        
        public static CarrierUpdateCommand GetExistentValidPhysicalCarrierUpdateCommand(AddressCommand address, CPF cpf)
        {
            return new CarrierUpdateCommand()
            {
                Id = 1,
                Name = "Nome teste",
                CompanyName = null,
                CPF = cpf.Value,
                CNPJ = null,
                StateRegistration = null,
                PersonType = PersonType.PHYSICAL,
                Address = address,
                FreightResponsability = FreightResponsability.RECEIVER,
            };
        }
        public static CarrierUpdateCommand GetExistentInvalidPhysicalCarrierUpdateCommandWithNullAddress(CPF cpf)
        {
            return new CarrierUpdateCommand()
            {
                Id = 1,
                Name = "Nome teste",
                CompanyName = null,
                CPF = cpf.Value,
                CNPJ = null,
                StateRegistration = null,
                PersonType = PersonType.PHYSICAL,
                Address = null,
                FreightResponsability = FreightResponsability.RECEIVER,
            };
        }

        public static CarrierUpdateCommand GetinvalidPhysicalCarrierUpdateCommandWithUninformedName(AddressCommand address, CPF cpf)
        {
            return new CarrierUpdateCommand()
            {
                Id = 1,
                Name = "",
                CompanyName = "",
                CPF = cpf.Value,
                CNPJ = "",
                StateRegistration = "",
                PersonType = PersonType.PHYSICAL,
                Address = address,
                FreightResponsability = FreightResponsability.RECEIVER,
            };
        }

        public static Carrier GetExistentValidCarrierLegal(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Id = 1,
                Name = "'Solution Brothers",
                CompanyName = "Solutions Brothers",
                CNPJ = cnpj,
                StateRegistration = "12345678",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address
            };
        }


        public static Carrier GetInvalidCarrierPhysicalNameLenghtOverflow(Address address, CPF cpf)
        {
            return new Carrier()
            {
                Name = "12345678901234567890123456789012345678901234567890123456789070",
                CPF = cpf,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.PHYSICAL,
                Address = address,
            };
        }

        public static CarrierUpdateCommand GetExistentValidLegalCarrierUpdateCommand(AddressCommand address, CNPJ cnpj)
        {
            return new CarrierUpdateCommand()
            {
                Id = 1,
                Name = "Nome teste",
                CompanyName = "Razao Social Teste",
                CPF = null,
                CNPJ = cnpj.Value,
                StateRegistration = "588675245",
                PersonType = PersonType.LEGAL,
                Address = address,
                FreightResponsability = FreightResponsability.RECEIVER,
            };
        }

        public static Carrier GetInvalidCarrierLegalNameLenghtOverflow(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "12345678901234567890123456789012345678901234567890123456789070",
                CNPJ = cnpj,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address,
            };
        }

        public static Carrier GetInvalidCarrierLegalCompanyNameEmpty(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "Solutions Brothers",
                CompanyName = "",
                CNPJ = cnpj,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address,
            };
        }

        public static Carrier GetInvalidCarrierLegalCompanyNameLenghtOverflow(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "Solutions Brothers",
                CompanyName = "12345678901234567890123456789012345678901234567890123456789070",
                CNPJ = cnpj,
                StateRegistration = "1234567",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address,
            };
        }
        
        public static Carrier GetInvalidCarrierCPFEmpty(Address address, CPF cpf)
        {
            cpf.Value = "";
            return new Carrier()
            {

                Name = "Solution Brothers",
                CPF = cpf,
                StateRegistration = "1234567",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.PHYSICAL,
                Address = address
            };
        }
        public static Carrier GetInvalidCarrierCNPJEmpty(Address address, CNPJ cnpj)
        {
            cnpj.Value = "";
            return new Carrier()
            {
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = cnpj,
                StateRegistration = "1234567",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address
            };
        }
        public static Carrier GetInvalidCarrierCPF(Address address, CPF cpf)
        { 
            return new Carrier()
            {

                Name = "Solution Brothers",
                CPF = cpf,
                StateRegistration = "1234567",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.PHYSICAL,
                Address = address
            };
        }

        public static Carrier GetInvalidCarrierCNPJ(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = cnpj,
                StateRegistration = "1234567",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address,
            };
        }

        public static Carrier GetInvalidCarrierCPFNull(Address address)
        {
            return new Carrier()
            {

                Name = "Solution Brothers",
                CPF = null,
                StateRegistration = "1234567",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.PHYSICAL,
                Address = address
            };
        }

        public static Carrier GetInvalidCarrierLegalNullCNPJ(Address address)
        {
            return new Carrier()
            {
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = null,
                StateRegistration = "1234567",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address,
            };
        }
        
        public static Carrier GetInvalidCarrierStateRegistrationEmpty(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = cnpj,
                StateRegistration = "",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address,
            };
        }
        
        public static Carrier GetInvalidCarrierStateRegistrationLenghtOverflow(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = cnpj,
                StateRegistration = "12345678901234516",
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL,
                Address = address,
            };
        }

        public static Carrier GetInvalidCarrierPhysicalFreightResponsabilityEmpty(Address address, CPF cpf)
        {
            return new Carrier()
            {

                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CPF = cpf,
                FreightResponsability = FreightResponsability.INVALID,
                PersonType = PersonType.PHYSICAL,
                Address = address
            };
        }

        public static Carrier GetInvalidCarrierLegalFreightResponsabilityEmpty(Address address, CPF cpf)
        {
            return new Carrier()
            {

                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CPF = cpf,
                FreightResponsability = FreightResponsability.INVALID,
                PersonType = PersonType.LEGAL,
                Address = address
            };
        }

        public static Carrier GetInvalidCarrierLegalNullAddress(CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = cnpj,
                StateRegistration = "2241235123",
                Address = null,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.LEGAL
            };
        }

        public static Carrier GetInvalidCarrierPhysicalNullAddress(CPF cpf)
        {
            return new Carrier()
            {
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CPF = cpf,
                StateRegistration = "2241235123",
                Address = null,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.PHYSICAL
            };
        }
        public static Carrier GetInvalidCarrierLegalFreightResponsabilityEmpty(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = cnpj,
                StateRegistration = "2241235123",
                FreightResponsability = FreightResponsability.INVALID,
                PersonType = PersonType.LEGAL,
                Address = address,
            };
        }

        public static Carrier GetInvalidInvalidPersonType(Address address, CPF cpf)
        {
            return new Carrier()
            {
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CPF = cpf,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.INVALID,
                Address = address,
            };
        }
        public static Carrier GetInvalidCarrierLegalPersonType(Address address, CNPJ cnpj)
        {
            return new Carrier()
            {
                Id = 2,
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = cnpj,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.INVALID,
                Address = address,
            };
        }

        public static Carrier GetInvalidCarrierInvalidPersonType(Address address)
        {
            return new Carrier()
            {
                Id = 2,
                Name = "Solution Brothers",
                CompanyName = "Solution Brothers",
                CNPJ = null,
                FreightResponsability = FreightResponsability.SENDER,
                PersonType = PersonType.INVALID,
                Address = address,
            };
        }

        public static CarrierViewModel GetPhysicalCarrierViewModel(AddessViewModel address, CPF cpf)
        {
            return new CarrierViewModel()
            {
                Id = 1,
                Address = address,
                Name = "Nome teste",
                CompanyName = null,
                CPF = cpf.Value,
                CNPJ = null,
                StateRegistration = null,
                PersonType = PersonType.PHYSICAL.ToString(),
                FreightResponsability = FreightResponsability.RECEIVER.ToString(),
            };
        }

        public static CarrierGetAllQuery GetValidCarrierGetAllQuery()
        {
            var size = 2;
            return new CarrierGetAllQuery(size);
        }

    }
}
