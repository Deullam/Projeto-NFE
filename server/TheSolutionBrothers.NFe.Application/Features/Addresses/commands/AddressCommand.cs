using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Application.Features.Addresses.commands
{
    public class AddressCommand 
    {
        public virtual string StreetName { get; set; }
        public virtual int Number { get; set; }
        public virtual string Neighborhood { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Country { get; set; }

        public AddressCommand()
        {
        }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<AddressCommand>
        {
            public Validator()
            {
                RuleFor(r => r.Number)
                    .GreaterThan(0).WithMessage("Endereço com numero menor que zero.");

                RuleFor(r => r.StreetName)
                    .NotEmpty().WithMessage("Endereço com nome da rua não informado.")
                    .MaximumLength(60).WithMessage("Endereço com nome de rua maior que 60 caracteres.");

                RuleFor(r => r.Neighborhood)
                    .NotEmpty().WithMessage("Endereço com nome do bairro não informado.")
                    .MaximumLength(40).WithMessage("Endereço com nome do bairro maior que 40 caracteres.");

                RuleFor(r => r.City)
                    .NotEmpty().WithMessage("Endereço com nome da cidade não informado.")
                    .MaximumLength(50).WithMessage("Endereço com nome da cidade maior que 50 caracteres.");

                RuleFor(r => r.State)
                    .NotEmpty().WithMessage("Endereço com nome do estado não informado.")
                    .MaximumLength(2).WithMessage("Endereço com nome do estado que 2 caracteres.");

                RuleFor(r => r.Country)
                    .NotEmpty().WithMessage("Endereço com nome do país não informado.")
                    .MaximumLength(50).WithMessage("Endereço com nome do país maior que 50 caracteres.");
            }
        }

    }
}
