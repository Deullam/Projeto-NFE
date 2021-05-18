using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Application.Features.Products.Commands
{
    public class ProductUpdateCommand
    {
        public long Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual double CurrentValue { get; set; }

        public ProductUpdateCommand()
        {
        }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ProductUpdateCommand>
        {
            public Validator()
            {
                RuleFor(r => r.Id)
               .GreaterThan(0).WithMessage("Produto com identificador inválido.");

                RuleFor(r => r.Code)
                .NotEmpty().WithMessage("Produto com Código não informado.")
                .MaximumLength(14).WithMessage("Produto com nome código maior que 15 caracteres.");

                RuleFor(r => r.Description)
                 .NotEmpty().WithMessage("Produto com Descrição não informada.")
                 .MaximumLength(60).WithMessage("Produto com Descrição maior que 14 caracteres.");

                RuleFor(r => r.CurrentValue)
                  .GreaterThanOrEqualTo(0).WithMessage("Produto com valor igual ou menor que zero.");

            }
        }
    }
}
