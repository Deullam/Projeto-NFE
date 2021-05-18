using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceItems
{
	public class InvoiceItemMapper : EntityTypeConfiguration<InvoiceItem>
	{
		public InvoiceItemMapper()
		{
			ToTable("TBInvoiceItem");

			HasKey(x => x.Id);

			HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);

			HasRequired(x => x.Invoice)
			   .WithMany(y => y.InvoiceItems)
			   .HasForeignKey(fk => fk.InvoiceId)
			   .WillCascadeOnDelete(true);

			Property(x => x.Amount).IsRequired();
			Property(x => x.IcmsAliquot).IsOptional();
			Property(x => x.IpiAliquot).IsOptional();
			Property(x => x.UnitValue).IsOptional();
			Property(x => x.Code).HasColumnType("VARCHAR").HasMaxLength(14).IsOptional();
			Property(x => x.Description).HasColumnType("VARCHAR").HasMaxLength(60).IsOptional();
		}
	}
}
