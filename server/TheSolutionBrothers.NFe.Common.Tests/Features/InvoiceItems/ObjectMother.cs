using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Invoices.Commands;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {
        public static InvoiceItem GetNewInvoiceItemOk(Invoice invoice, Product product)
        {
            return new InvoiceItem
            {
                Amount = 1,
                Invoice = invoice,
                Product = product
            };
        }

        public static InvoiceItemRegisterCommand GetValidInvoiceItemRegisterCommand()
        {
            return new InvoiceItemRegisterCommand
            {
                Amount = 1,
                ProductId = 1
            };
        }

        public static InvoiceItemRegisterCommand GetInvalidInvoiceItemRegisterCommandWithUninformedProduct()
        {
            return new InvoiceItemRegisterCommand
            {
                Amount = 1
            };
        }

        public static InvoiceItemUpdateCommand GetValidInvoiceItemUpdateCommand()
        {
            return new InvoiceItemUpdateCommand
            {
                Id = 1,
                Amount = 1,
                ProductId = 1
            };
        }

        public static InvoiceItemUpdateCommand GetInvalidInvoiceItemUpdateCommandWithUninformedId()
        {
            return new InvoiceItemUpdateCommand
            {
                Amount = 1,
                ProductId = 1
            };
        }

        public static InvoiceItem GetExistentInvoinceItemOk(Invoice invoice, Product product)
        {
            return new InvoiceItem
            {
                Id = 1,
                Amount = 1,
                Invoice = invoice,
                Product = product
            };
        }

        public static InvoiceItem GetExistentInvoinceItemOk(Product product,Invoice invoice = null)
        {
            return new InvoiceItem
            {
                Id = 1,
                Amount = 1,
                Invoice = invoice,
                Product = product
            };
        }

        public static InvoiceItem GetInvalidInvoiceItemWithUninformedAmount(Invoice invoice, Product product)
        {
            return new InvoiceItem
            {
                Amount = 0,
                Invoice = invoice,
                Product = product
            };
        }

        public static InvoiceItem GetInvalidInvoiceItemWithInvalidAmount(Invoice invoice, Product product)
        {
            return new InvoiceItem
            {
                Amount = -1,
                Invoice = invoice,
                Product = product
            };
        }

        public static InvoiceItem GetInvalidInvoiceItemWithInvoiceNull(Product product)
        {
            return new InvoiceItem
            {
                Amount = 1,
                Invoice = null,
                Product = product
            };
        }

        public static InvoiceItem GetInvalidInvoiceItemWithProductNull(Invoice invoice)
        {
            return new InvoiceItem
            {
                Amount = 1,
                Invoice = invoice,
                Product = null
            };
        }

        public static InvoiceItem GetInvalidInvoiceItemWithInvalidProduct(Invoice invoice, Product product)
        {
            return new InvoiceItem
            {
                Amount = 1,
                Invoice = invoice,
                Product = product
            };
        }

        public static InvoiceItem GetNewConsolidatedInvoiceItem(Invoice invoice, Product product)
        {
            return new InvoiceItem
            {
                Id = 1,
                Amount = 2,
                Invoice = invoice,
                Product = product,
                IcmsAliquot = product.TaxProduct.IcmsAliquot,
                IpiAliquot = product.TaxProduct.IpiAliquot,
                Code = product.Code,
                Description = product.Description,
                UnitValue = product.CurrentValue
            };
        }

        public static InvoiceItem GetExistentConsolidatedInvoiceItem(Invoice invoice, Product product)
        {
            return new InvoiceItem
            {
                Id = 1,
                Amount = 1,
                Invoice = invoice,
                Product = product,
                IcmsAliquot = product.TaxProduct.IcmsAliquot,
                IpiAliquot = product.TaxProduct.IpiAliquot,
                Code = product.Code,
                Description = product.Description,
                UnitValue = product.CurrentValue
            };
        }

        public static InvoiceItem GetInvalidConsolidatedInvoiceItemWithUninformedAmount(Invoice invoice, Product product)
        {
            return new InvoiceItem
            {
                Amount = 0,
                Invoice = invoice,
                Product = product,
                IcmsAliquot = product.TaxProduct.IcmsAliquot,
                IpiAliquot = product.TaxProduct.IpiAliquot,
                Code = product.Code,
                Description = product.Description,
                UnitValue = product.CurrentValue
            };
        }

        public static InvoiceItem GetNewValidInvoiceItem(Invoice invoice, Product product)
        {
            return new InvoiceItem()
            {
                Amount = 5,
                Invoice = invoice,
                Product = product
            };
        }

        public static InvoiceItem GetExistentValidInvoiceItem(Invoice invoice, Product product)
        {
            return new InvoiceItem()
            {
                Id = 1,
                Amount = 5,
                Invoice = invoice,
                Product = product
            };
        }

        public static InvoiceItemDeleteCommand GetValidInvoiceItemDeleteCommand()
        {
            return new InvoiceItemDeleteCommand()
            {
                InvoiceItemIds = new long[] { 1 }
            };
        }


        public static InvoiceItemDeleteCommand GetInvalidInvoiceItemDeleteCommandWithEmptyList()
        {
            return new InvoiceItemDeleteCommand()
            {
                InvoiceItemIds = new long[] { }
            };
        }


    }
}
