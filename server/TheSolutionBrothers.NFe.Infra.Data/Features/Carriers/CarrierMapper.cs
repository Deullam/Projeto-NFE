using System.Data.Entity.ModelConfiguration;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Carriers
{
    public class CarrierMapper : EntityTypeConfiguration<Carrier>
    {
        public CarrierMapper()
        {
            ToTable("TBCarrier");

            HasKey(x => x.Id);

            HasRequired(x => x.Address);

            Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(60).IsRequired();
            Property(x => x.CompanyName).HasColumnType("VARCHAR").HasMaxLength(60).IsOptional();
            Property(x => x.StateRegistration).HasColumnType("VARCHAR").HasMaxLength(15).IsOptional();
            Property(x => x.CPF.Value).HasColumnName("CPF").HasColumnType("VARCHAR").HasMaxLength(11).IsOptional();
            Property(x => x.CNPJ.Value).HasColumnName("CNPJ").HasColumnType("VARCHAR").HasMaxLength(14).IsOptional();
            Property(x => x.PersonType).IsRequired();
            Property(x => x.FreightResponsability).IsRequired();
        }

    }
}
