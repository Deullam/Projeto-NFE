using System.Data.Entity.ModelConfiguration;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Receivers
{
    public class ReceiverMapper : EntityTypeConfiguration<Receiver>
    {
        public ReceiverMapper()
        {
            ToTable("TBReceiver");

            HasKey(x => x.Id);

            HasRequired(x => x.Address);

            Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(60).IsRequired();
            Property(x => x.CompanyName).HasColumnType("VARCHAR").HasMaxLength(60).IsOptional();
            Property(x => x.StateRegistration).HasColumnType("VARCHAR").HasMaxLength(15).IsOptional();
            Property(x => x.Cpf.Value).HasColumnName("CPF").HasColumnType("VARCHAR").HasMaxLength(11).IsOptional();
            Property(x => x.Cnpj.Value).HasColumnName("CNPJ").HasColumnType("VARCHAR").HasMaxLength(14).IsOptional();
            Property(x => x.Type).IsRequired();
        }

    }
}
