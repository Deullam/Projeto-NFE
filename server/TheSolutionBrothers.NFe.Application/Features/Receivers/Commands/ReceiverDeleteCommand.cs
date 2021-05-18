using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Application.Features.Receivers.Commands
{
    public class ReceiverDeleteCommand
    {
        public virtual long[] ReceiverIds { get; set; }



        public ReceiverDeleteCommand()
        {
        }

        public ValidationResult Validate()
        {
            return new Validator().Validate(this);
        }

        class Validator : AbstractValidator<ReceiverDeleteCommand>
        {
            public Validator()
            {
                RuleFor(c => c.ReceiverIds).NotNull();
                RuleFor(c => c.ReceiverIds.Length).GreaterThan(0);
            }
        }
    }
}
