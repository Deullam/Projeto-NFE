using FluentValidation;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Application.Features.Carriers.Commands
{
    public class CarrierDeleteCommand
    {

        public virtual long[] CarrierIds { get; set; }

        public CarrierDeleteCommand()
        {
        }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<CarrierDeleteCommand>
        {
            public Validator()
            {
                RuleFor(c => c.CarrierIds).NotNull();
                RuleFor(c => c.CarrierIds.Length).GreaterThan(0);
            }
        }

    }
}
