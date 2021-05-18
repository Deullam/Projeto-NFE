using TheSolutionBrothers.NFe.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Products
{
    public class ProductDeleteWithDependencesException : BusinessException
    {
        public ProductDeleteWithDependencesException() : base(ErrorCodes.Unauthorized, "Produto relacionado com um invoiceItem.")
        {
        }
    }
}
