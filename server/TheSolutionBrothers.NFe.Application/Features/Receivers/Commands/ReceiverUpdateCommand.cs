using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CNPJ;
using TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CPF;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;

namespace TheSolutionBrothers.NFe.Application.Features.Receivers.Commands
{
    public class ReceiverUpdateCommand
    {
        public long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Cnpj { get; set; }
        public virtual string StateRegistration { get; set; }
        public virtual PersonType PersonType { get; set; }
        public virtual AddressCommand Address { get; set; }

        public ReceiverUpdateCommand()
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
        class Validator : AbstractValidator<ReceiverUpdateCommand>
        {
            public Validator()
            {
                RuleFor(r => r.Address)
                    .NotNull().WithMessage("Destinatário com endereço nulo");

                RuleFor(r => r.Id)
                    .GreaterThan(0).WithMessage("Destinatário com identificador inválido.");

                RuleFor(r => r.PersonType)
                    .NotEqual(PersonType.INVALID).WithMessage("Destinatário com tipo de pessoa não informado.");

                RuleFor( r=> r.Name)
                    .NotEmpty().WithMessage("Destinatário com nome não informado.")
                    .MaximumLength(60).WithMessage("Destinatário com nome maior que 60 caracteres.");

                RuleFor(r => r.CompanyName)
                    .NotEmpty().WithMessage("Destinatário com razão social não informado.")
                    .MaximumLength(60).WithMessage("Destinatário com nome da empresa maior que 60 caracteres.")
                    .When(x => x.PersonType == PersonType.LEGAL);

                RuleFor(r => r.StateRegistration)
                    .NotEmpty().WithMessage("Destinatário com inscrição estadual não informada.")
                    .MaximumLength(15).WithMessage("Destinatário com inscrição estadual maior que 15 caracteres.")
                    .When(x => x.PersonType == PersonType.LEGAL);

                RuleFor(r => r.Cpf)
                    .NotNull().WithMessage("Destinatário com CPF nulo.")
                    .When(x => x.PersonType.Equals(PersonType.PHYSICAL));

                RuleFor(r => r.Cpf)
                    .NotEmpty().WithMessage("Destinatário com CPF não informado.")
                    .MaximumLength(11).WithMessage("Destinatário com CPF maior que 11 caracteres")
                    .Must(CPFValidator.IsValid).WithMessage("Destinatário com CPF inválido.")
                    .When(x => x.PersonType.Equals(PersonType.PHYSICAL));

                RuleFor(r => r.Cnpj)
                    .NotNull().WithMessage("Destinatário com CNPJ nulo.")
                    .When(x => x.PersonType.Equals(PersonType.LEGAL));

                RuleFor(r => r.Cnpj)
                    .NotEmpty().WithMessage("Destinatário com CNPJ não informado.")
                    .MaximumLength(14).WithMessage("Destinatário com CNPJ maior que 14 caracteres")
                    .Must(CNPJValidator.IsValid).WithMessage("Destinatário com CNPJ inválido.")
                    .When(x => x.PersonType.Equals(PersonType.LEGAL));
            }
        }
    }
}
