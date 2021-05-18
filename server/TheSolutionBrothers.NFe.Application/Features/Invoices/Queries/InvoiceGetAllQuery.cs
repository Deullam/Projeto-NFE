using FluentValidation;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Application.Features.Invoices.Queries
{
	public class InvoiceGetAllQuery
	{
		public virtual int Size { get; set; }

		public InvoiceGetAllQuery(int _size)
		{
			Size = _size;
		}

		public ValidationResult Validate()
		{
			return new Validator().Validate(this);
		}

		class Validator : AbstractValidator<InvoiceGetAllQuery>
		{
			public Validator()
			{
				RuleFor(c => c.Size).GreaterThan(0).WithMessage("Quantidade inválida.");
			}
		}
	}
}
