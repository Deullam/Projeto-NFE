using FluentValidation;
using FluentValidation.Results;

namespace TheSolutionBrothers.NFe.Application.Features.Products.Queries
{
	public class ProductGetAllQuery
	{
		public virtual int Size { get; set; }

		public ProductGetAllQuery(int _size)
		{
			Size = _size;
		}

		public ValidationResult Validate()
		{
			return new Validator().Validate(this);
		}

		class Validator : AbstractValidator<ProductGetAllQuery>
		{
			public Validator()
			{
				RuleFor(c => c.Size).GreaterThan(0).WithMessage("Quantidade inválida.");
			}
		}
	}
}