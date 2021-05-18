using FluentValidation;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Application.Features.Receivers.Queries
{
    public class ReceiverGetAllQuery
    {
        public virtual int Size { get; set; }

        public ReceiverGetAllQuery(int _size)
        {
            Size = _size;
        }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ReceiverGetAllQuery>
        {
            public Validator()
            {
                RuleFor(c => c.Size).GreaterThan(0).WithMessage("Quantidade inválida.");
            }
        }
    }
}
