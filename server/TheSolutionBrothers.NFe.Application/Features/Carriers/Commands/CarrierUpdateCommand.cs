using FluentValidation;
using FluentValidation.Results;
using TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CNPJ;
using TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CPF;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;

namespace TheSolutionBrothers.NFe.Application.Features.Carriers.Commands
{
    public class CarrierUpdateCommand
    {
        public long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string CPF { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual string StateRegistration { get; set; }
        public virtual FreightResponsability FreightResponsability { get; set; }
        public virtual PersonType PersonType { get; set; }
        public virtual AddressCommand Address { get; set; }

        public CarrierUpdateCommand()
        {
        }

        public ValidationResult Validate()
        {
            ValidationResult addressValidationResult = new ValidationResult();
            if (this.Address != null)
            {
                addressValidationResult = this.Address.Validate();
            }
            if (!addressValidationResult.IsValid)
            {
                return addressValidationResult;
            }
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<CarrierUpdateCommand>
        {
            public Validator()
            {
                RuleFor(r => r.Id)
                    .GreaterThan(0).WithMessage("Transportador com identificador inválido.");
                
                RuleFor(r => r.Address)
                    .NotNull().WithMessage("Transportador com endereço nulo");

                RuleFor(r => r.Name)
                    .NotEmpty().WithMessage("Transportador com nome não informado.")
                    .MaximumLength(60).WithMessage("Transportador com nome maior que 60 caracteres.");

                RuleFor(r => r.CompanyName)
                    .NotEmpty().WithMessage("Transportador com razão social não informado.")
                    .MaximumLength(60).WithMessage("Transportador com razão social maior que 60 caracteres.")
                    .When(x => x.PersonType == PersonType.LEGAL);

                RuleFor(r => r.PersonType)
                    .NotEqual(PersonType.INVALID).WithMessage("Transportador com tipo de pessoa não informada.");

                RuleFor(r => r.FreightResponsability)
                    .NotEqual(FreightResponsability.INVALID).WithMessage("Transportador com responsabilidade de frete não informada.");

                RuleFor(r => r.StateRegistration)
                    .NotEmpty().WithMessage("Transportador com inscrição estadual não informada.")
                    .MaximumLength(15).WithMessage("Transportador com inscrição estadual maior que 15 caracteres.")
                    .When(x => x.PersonType == PersonType.LEGAL);

                RuleFor(r => r.CPF)
                    .NotNull().WithMessage("Transportador com CPF nulo.")
                    .When(x => x.PersonType.Equals(PersonType.PHYSICAL));

                RuleFor(r => r.CPF)
                    .NotEmpty().WithMessage("Transportador com CPF não informado.")
                    .MaximumLength(11).WithMessage("Transportador com CPF maior que 11 caracteres")
                    .Must(CPFValidator.IsValid).WithMessage("Transportador com CPF inválido.")
                    .When(x => x.PersonType.Equals(PersonType.PHYSICAL));

                RuleFor(r => r.CNPJ)
                    .NotNull().WithMessage("Transportador com CNPJ nulo.")
                    .When(x => x.PersonType.Equals(PersonType.LEGAL));

                RuleFor(r => r.CNPJ)
                    .NotEmpty().WithMessage("Transportador com CNPJ não informado.")
                    .MaximumLength(14).WithMessage("Transportador com CNPJ maior que 14 caracteres")
                    .Must(CNPJValidator.IsValid).WithMessage("Transportador com CNPJ inválido.")
                    .When(x => x.PersonType.Equals(PersonType.LEGAL));

            }
        }
    }
}
