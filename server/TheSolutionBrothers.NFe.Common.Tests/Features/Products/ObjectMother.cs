using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Products.Commands;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;
using TheSolutionBrothers.NFe.Application.Features.Products.Queries;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {

        public static Product GetNewValidProduct(TaxProduct taxProduct)
        {
            return new Product()
            {
                Code = "122424",
                Description = "Samsung Galaxy S9",
                CurrentValue = 4399.99,
                TaxProduct = taxProduct
            };
        }

        public static ProductRegisterCommand GetValidProductRegisterCommand()
        {
            return new ProductRegisterCommand()
            {
                Code = "122424",
                Description = "Samsung Galaxy S9",
                CurrentValue = 4399.99
            };
        }

        public static Product GetExistentValidProduct(TaxProduct taxProduct)
        {
            return new Product()
            {
                Id = 1,
                Code = "122424",
                Description = "Samsung Galaxy S9",
                CurrentValue = 4399.99,
                TaxProduct = taxProduct
            };
        }

        public static ProductViewModel GetExistentValidProductViewModel()
        {
            return new ProductViewModel()
            {
                Id = 1,
                Code = "122424",
                Description = "Samsung Galaxy S9",
                CurrentValue = 4399.99,
                IcmsAliquot = 3.0,
                IpiAliquot = 3.0
            };
        }

        public static ProductGetAllQuery GetValidProductGetAllQuery()
        {
            var size = 2;
            return new ProductGetAllQuery(size)
            {
            };
        }

        public static ProductUpdateCommand GetExistentValidProductUpdateCommand()
        {
            return new ProductUpdateCommand()
            {
                Id = 1,
                Code = "122424",
                Description = "Samsung Galaxy S9",
                CurrentValue = 4399.99
            };
        }


        public static Product GetExistentValidProductWithoutDependency(TaxProduct taxProduct)
        {
            return new Product()
            {
                Id = 2,
                Code = "122425",
                Description = "Samsung Galaxy S8",
                CurrentValue = 4399.99,
                TaxProduct = taxProduct
            };
        }

        public static ProductDeleteCommand GetExistentValidProductWithoutDependencyDeleteCommand()
        {
            return new ProductDeleteCommand()
            {
                ProductIds = new long[]{1}
            };
        }
        public static ProductDeleteCommand GetExistentValidProductWithoutIdDeleteCommand()
        {
            return new ProductDeleteCommand()
            {
                ProductIds = new long[] {}
            };
        }

        public static Product GetInvalidProductWithUniformedCode(TaxProduct taxProduct)
        {
            return new Product()
            {
                Code = null,
                Description = "Samsung Galaxy S9",
                CurrentValue = 4399.99,
                TaxProduct = taxProduct
            };
        }

        public static Product GetInvalidProductWithCodeLengthOverflow(TaxProduct taxProduct)
        {
            return new Product()
            {
                Code = "123456789012345",
                Description = "Samsung Galaxy S9",
                CurrentValue = 4399.99,
                TaxProduct = taxProduct
            };
        }

        public static ProductRegisterCommand GetInvalidProductWithCodeLengthOverflowRegisterCommand()
        {
            return new ProductRegisterCommand()
            {
                Code = "123456789012345",
                Description = "Samsung Galaxy S9",
                CurrentValue = 4399.99,
            };
        }

        public static Product GetInvalidProductWithUninformedDescription(TaxProduct taxProduct)
        {
            return new Product()
            {
                Code = "122424",
                Description = null,
                CurrentValue = 4399.99,
                TaxProduct = taxProduct
            };
        }

        public static Product GetInvalidProductWithDescriptionLengthOverflow(TaxProduct taxProduct)
        {
            return new Product()
            {
                Code = "122424",
                Description = "asdfgasdfgasdfgasdfgasdfgasdfgasdfgasdfgasdfgasdfgasdfgasdfgv",
                CurrentValue = 4399.99,
                TaxProduct = taxProduct
            };
        }

        public static Product GetInvalidProductWithCurrentValueLowerThanZero(TaxProduct taxProduct)
        {
            return new Product()
            {
                Code = "122424",
                Description = "Samsung Galaxy S9",
                CurrentValue = -1,
                TaxProduct = taxProduct
            };
        }

        public static Product GetInvalidProductWithCurrentValueEqualThanZero(TaxProduct taxProduct)
        {
            return new Product()
            {
                Code = "122424",
                Description = "Samsung Galaxy S9",
                CurrentValue = 0,
                TaxProduct = taxProduct
            };
        }

        public static ProductUpdateCommand GetInvalidProductWithCurrentValueEqualThanZeroUpdateCommand()
        {
            return new ProductUpdateCommand()
            {
                Code = "122424",
                Description = "Samsung Galaxy S9",
                CurrentValue = 0
            };
        }

        public static Product GetInvalidProductWithNullTaxProduct()
        {
            return new Product()
            {
                Code = "122424",
                Description = "Samsung Galaxy S9",
                CurrentValue = 4399.99,
                TaxProduct = null
            };
        }

    }
}
