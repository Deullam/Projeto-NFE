using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {

        public static TaxProduct GetValidTaxProduct()
        {
            return new TaxProduct();
        }

    }
}
