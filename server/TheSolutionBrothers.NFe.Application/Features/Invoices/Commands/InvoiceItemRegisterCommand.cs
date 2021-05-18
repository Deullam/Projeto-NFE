using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices.Commands
{
	public class InvoiceItemRegisterCommand
	{
		public long ProductId { get; set; }

		public long Amount { get; set; }

		public InvoiceItemRegisterCommand()
		{
		}

		public virtual ValidationResult Validate()
		{
			ValidationResult addressValidationResult = new ValidationResult();
			return new Validator().Validate(this);
		}

		class Validator : AbstractValidator<InvoiceItemRegisterCommand>
		{
			public Validator()
			{
				RuleFor(r => r.ProductId)
					.GreaterThan(0).WithMessage("Item de nota fiscal com identificador de produto inválido");

				RuleFor(r => r.Amount)
					.GreaterThan(0).WithMessage("Item de nota fiscal com quantidade inválida.");

			}
		}
	}
}
