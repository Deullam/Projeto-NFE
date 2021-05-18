namespace TheSolutionBrothers.NFe.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TheSolutionBrothers.Nfe.Infra.Crypto;
    using TheSolutionBrothers.NFe.Domain.Features.Users;

    internal sealed class Configuration : DbMigrationsConfiguration<TheSolutionBrothers.NFe.Infra.Data.Contexts.ContextNfe>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TheSolutionBrothers.NFe.Infra.Data.Contexts.ContextNfe context)
        {
            var password = "12345";
            User user = new User();
            user.Name = "admin";
            user.Password = password.GenerateHash();

            context.Users.Add(user);

            context.SaveChanges();
        }
    }
}
