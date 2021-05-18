using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.InvoiceItems
{
    public class InvoiceItem
    {
        public virtual long Id { get; set; }
        public Invoice Invoice { get; set; }
        public Product Product { get; set; }

        public double IcmsAliquot { get; set; }
        public double IpiAliquot { get; set; }
        public long Amount { get; set; }
        public double UnitValue { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

		public long InvoiceId { get; set; }
		public long ProductId { get; set; }

		public virtual double TotalValue
        {
            get
            {
                return UnitValue * Amount;
            }
        }
        public virtual double IcmsValue
        {
            get
            {
                return TotalValue * IcmsAliquot;
            }
        }
        public virtual double IpiValue
        {
            get
            {
                return TotalValue * IpiAliquot;
            }
        }

        public virtual void Validate()
        {
            if (Amount == 0)
                throw new InvoiceItemUninformedAmountException();

            if (Amount < 0)
                throw new InvoiceItemInvalidAmountException();

            if (Invoice == null)
                throw new InvoiceItemNullInvoiceException();

            if (Product == null)
                throw new InvoiceItemNullProductException();

            Product.Validate();
        }

        public virtual void Consolidate()
        {
            IcmsAliquot = Product.TaxProduct.IcmsAliquot;
            IpiAliquot = Product.TaxProduct.IpiAliquot;
            UnitValue = Product.CurrentValue;
            Code = Product.Code;
            Description = Product.Description;
        }
    }
}
