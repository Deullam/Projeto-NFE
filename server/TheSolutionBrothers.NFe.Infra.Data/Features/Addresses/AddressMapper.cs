using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Addresses
{
    public class AddressMapper : EntityTypeConfiguration<Address>
    {
        public AddressMapper()
        {
            ToTable("TBAddress");

            HasKey(x => x.Id);

            Property(x => x.City).HasColumnType("VARCHAR").HasMaxLength(80).IsRequired();
            Property(x => x.Country).HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();
            Property(x => x.Neighborhood).HasColumnType("VARCHAR").HasMaxLength(60).IsRequired();
            Property(x => x.State).HasColumnType("VARCHAR").HasMaxLength(2).IsRequired();
            Property(x => x.StreetName).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired();
            Property(x => x.Number).IsRequired();
        }
    }
}
