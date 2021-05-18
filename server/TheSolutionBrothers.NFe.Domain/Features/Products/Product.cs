using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Products
{
    public class Product
    {  
        public virtual long Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual double CurrentValue { get; set; }
        public virtual TaxProduct TaxProduct { get; set; }

        public Product()
        {
            TaxProduct = new TaxProduct();
        }

        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(Code))
            {
                throw new ProductUninformedCodeException();
            }

            if (Code.Length > 14)
            {
                throw new ProductCodeLengthOverflowException();
            }

            if (string.IsNullOrEmpty(Description))
            {
                throw new ProductUninformedDescriptionException();
            }

            if (Description.Length > 60)
            {
                throw new ProductDescriptionLengthOverflowException();
            }

            if (CurrentValue <= 0)
            {
                throw new ProductCurrentValueLowerOrEqualThanZeroException();
            }

            if (TaxProduct == null)
            {
                throw new ProductNullTaxProductException();
            }

        }

    }
}
