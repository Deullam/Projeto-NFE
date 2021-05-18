using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Invoices
{
	public class InvoiceMapper : EntityTypeConfiguration<Invoice>
	{
		public InvoiceMapper()
		{
			ToTable("TBInvoice");

			HasKey(x => x.Id);

            HasRequired(x => x.Receiver).WithMany().HasForeignKey(x => x.ReceiverId).WillCascadeOnDelete(false);
            HasRequired(x => x.Carrier).WithMany().HasForeignKey(x => x.CarrierId).WillCascadeOnDelete(false);
            HasRequired(x => x.Sender).WithMany().HasForeignKey(x => x.SenderId).WillCascadeOnDelete(false);

            Property(x => x.NatureOperation).HasColumnType("VARCHAR").HasMaxLength(70).IsRequired();
			Property(x => x.KeyAccess.Value).HasColumnType("CHAR").HasMaxLength(44).IsFixedLength().HasColumnName("KeyAccess").IsOptional();
			Property(x => x.Number).IsRequired();
			Property(x => x.Status).IsRequired();
			Property(x => x.EntryDate).HasColumnType("DATETIME2").IsRequired();
			Property(x => x.IssueDate).HasColumnType("DATETIME2").IsOptional();

			Ignore(x => x.InvoiceReceiver);
			Ignore(x => x.InvoiceCarrier);
			Ignore(x => x.InvoiceSender);
			Ignore(x => x.InvoiceTax);

		}
	}
}
