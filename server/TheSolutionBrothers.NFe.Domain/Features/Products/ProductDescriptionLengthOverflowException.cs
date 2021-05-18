using TheSolutionBrothers.NFe.Domain.Exceptions;

namespace TheSolutionBrothers.NFe.Domain.Features.Products
{

    public class ProductDescriptionLengthOverflowException : BusinessException
    {

        public ProductDescriptionLengthOverflowException() : base(ErrorCodes.Unauthorized, "Comprimento da descrição maior que 60 caracteres.")
        {
        }

    }
}
