using FluentValidation;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices.Commands
{
	public class InvoiceItemUpdateCommand
    {
        public long Id { get; set; }
        public long ProductId { get; set; }

		public long Amount { get; set; }

		public InvoiceItemUpdateCommand()
		{
		}

		public virtual ValidationResult Validate()
		{
			ValidationResult addressValidationResult = new ValidationResult();
			return new Validator().Validate(this);
		}

		class Validator : AbstractValidator<InvoiceItemUpdateCommand>
		{
			public Validator()
            {
                RuleFor(r => r.Id)
                      .GreaterThan(0).WithMessage("Item de nota fiscal com identificador inválido.");

                RuleFor(r => r.ProductId)
					.GreaterThan(0).WithMessage("Item de nota fiscal com identificador de produto inválido");

				RuleFor(r => r.Amount)
					.GreaterThan(0).WithMessage("Item de nota fiscal com quantidade inválida.");

			}
		}
	}
}
