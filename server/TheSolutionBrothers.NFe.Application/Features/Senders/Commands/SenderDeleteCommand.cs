using FluentValidation;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Application.Features.Senders.Commands
{
	public class SenderDeleteCommand
	{
		public virtual long[] SenderIds { get; set; }


		public SenderDeleteCommand()
		{
		}

		public ValidationResult Validate()
		{
			return new Validator().Validate(this);
		}

		class Validator : AbstractValidator<SenderDeleteCommand>
		{
			public Validator()
			{
				RuleFor(c => c.SenderIds).NotNull();
				RuleFor(c => c.SenderIds.Length).GreaterThan(0);
			}
		}
	}
}
