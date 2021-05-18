using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Application.Features.Products.Commands
{
    public class ProductDeleteCommand
    {
        public virtual long[] ProductIds { get; set; }

        public ProductDeleteCommand()
        {
        }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ProductDeleteCommand>
        {
            public Validator()
            {
                RuleFor(c => c.ProductIds).NotNull();
                RuleFor(c => c.ProductIds.Length).GreaterThan(0);
            }
        }
    }
}
