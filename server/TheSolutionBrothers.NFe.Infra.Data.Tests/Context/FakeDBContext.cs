using System.Data.Common;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;

namespace TheSolutionBrothers.NFe.Infra.Data.Tests.Context
{
    public class FakeDbContext : ContextNfe
    {
        public FakeDbContext(DbConnection connection) : base(connection)
        {
        }
    }
}
