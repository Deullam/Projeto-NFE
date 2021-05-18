using FluentValidation;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Application.Features.Senders.Queries
{
	public class SenderGetAllQuery
	{
		public virtual int Size { get; set; }

		public SenderGetAllQuery(int _size)
		{
			Size = _size;
		}

		public ValidationResult Validate()
		{
			return new Validator().Validate(this);
		}

		class Validator : AbstractValidator<SenderGetAllQuery>
		{
			public Validator()
			{
				RuleFor(c => c.Size).GreaterThan(0).WithMessage("Quantidade inválida.");
			}
		}
	}
}