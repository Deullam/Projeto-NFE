using System.Data.Entity.ModelConfiguration;
using TheSolutionBrothers.NFe.Domain.Features.Senders;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Senders
{
	public class SenderMapper : EntityTypeConfiguration<Sender>
	{
		public SenderMapper()
		{
			ToTable("TBSender");

			HasKey(x => x.Id);

			HasRequired(x => x.Address);

			Property(x => x.FancyName).HasColumnType("VARCHAR").HasMaxLength(60).IsRequired();
			Property(x => x.CompanyName).HasColumnType("VARCHAR").HasMaxLength(60).IsOptional();
			Property(x => x.StateRegistration).HasColumnType("VARCHAR").HasMaxLength(15).IsOptional();
			Property(x => x.MunicipalRegistration).HasColumnType("VARCHAR").HasMaxLength(15).IsOptional();
			Property(x => x.Cnpj.Value).HasColumnType("VARCHAR").HasMaxLength(14).IsOptional();
		}

	}
}
