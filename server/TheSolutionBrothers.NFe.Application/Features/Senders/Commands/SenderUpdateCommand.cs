using TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CNPJ;
using TheSolutionBrothers.NFe.Application.Features.Addresses.commands;
using FluentValidation;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Application.Features.Senders.Commands
{
	public class SenderUpdateCommand
	{
		public long Id { get; set; }
		public virtual string FancyName { get; set; }
		public virtual string CompanyName { get; set; }
		public virtual string Cnpj { get; set; }
		public virtual string StateRegistration { get; set; }
		public virtual string MunicipalRegistration { get; set; }
		public virtual AddressCommand Address { get; set; }

		public SenderUpdateCommand()
		{
		}

		public ValidationResult Validate()
		{
			ValidationResult addressValidationResult = new ValidationResult();
			if (this.Address != null)
			{
				addressValidationResult = this.Address.Validate();
			}
			if (!addressValidationResult.IsValid)
			{
				return addressValidationResult;
			}
			return new Validator().Validate(this);
		}

		class Validator : AbstractValidator<SenderUpdateCommand>
		{
			public Validator()
			{
				RuleFor(r => r.Id)
				   .GreaterThan(0).WithMessage("Emitente com identificador inválido.");

				RuleFor(r => r.FancyName)
					.NotEmpty().WithMessage("Emitente com nome fantasia não informado.")
					.MaximumLength(60).WithMessage("Emitente com nome fantasia maior que 60 caracteres.");

				RuleFor(r => r.CompanyName)
					.NotEmpty().WithMessage("Emitente com razão social não informada.")
					.MaximumLength(60).WithMessage("Emitente com razão social maior que 60 caracteres.");

				RuleFor(r => r.Cnpj)
					.NotNull().WithMessage("Emitente com CNPJ nulo.");

				RuleFor(r => r.Cnpj)
					.NotEmpty().WithMessage("Emitente com CNPJ não informado.")
					.MaximumLength(14).WithMessage("Emitente com CNPJ maior que 14 caracteres")
					.Must(CNPJValidator.IsValid).WithMessage("Emitente com CNPJ inválido.");

				RuleFor(r => r.StateRegistration)
					.NotEmpty().WithMessage("Emitente com inscrição estadual não informada.")
					.MaximumLength(15).WithMessage("Emitente com inscrição estadual maior que 15 caracteres.");

				RuleFor(r => r.MunicipalRegistration)
					.NotEmpty().WithMessage("Emitente com inscrição municipal não informada.")
					.MaximumLength(15).WithMessage("Emitente com inscrição municipal maior que 15 caracteres.");

				RuleFor(r => r.Address)
					.NotNull().WithMessage("Emitente com endereço nulo");

			}
		}
	}
}