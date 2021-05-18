using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Domain.Features.Products;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Products
{
    class ProductMapper : EntityTypeConfiguration<Product>
    {
        public ProductMapper()
        {
            ToTable("TBProduct");

            HasKey(x => x.Id);

            Property(x => x.Code).HasColumnType("VARCHAR").HasMaxLength(14).IsRequired();
            Property(x => x.Description).HasColumnType("VARCHAR").HasMaxLength(60).IsRequired();
            Property(x => x.CurrentValue).IsRequired();
            Ignore(x => x.TaxProduct);
        }

    }
}
