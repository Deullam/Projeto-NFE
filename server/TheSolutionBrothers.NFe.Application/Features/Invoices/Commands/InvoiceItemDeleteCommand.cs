using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices.Commands
{
	public class InvoiceItemDeleteCommand
	{
		public virtual long[] InvoiceItemIds { get; set; }
        

		public InvoiceItemDeleteCommand()
		{
		}

		public ValidationResult Validate()
		{
			return new Validator().Validate(this);
		}

		class Validator : AbstractValidator<InvoiceItemDeleteCommand>
		{
			public Validator()
			{
                RuleFor(c => c.InvoiceItemIds).NotNull().WithMessage("Nenhum ID informado.");

				RuleFor(c => c.InvoiceItemIds.Length).GreaterThan(0).WithMessage("Nenhum ID informado.");
			}
		}
	}
}
