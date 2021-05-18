using FluentValidation;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Application.Features.Carriers.Queries
{
    public class CarrierGetAllQuery
    {
        public virtual int Size { get; set; }

        public CarrierGetAllQuery(int _size)
        {
            Size = _size;
        }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<CarrierGetAllQuery>
        {
            public Validator()
            {
                RuleFor(c => c.Size).GreaterThan(0).WithMessage("Quantidade inválida.");
            }
        }
    }
}
