using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Domain.Features.Users;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Users
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("TBUser");

            HasKey(x => x.Id);

            Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(50).IsRequired();
            Property(x => x.Password).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired();

        }
    }
}
