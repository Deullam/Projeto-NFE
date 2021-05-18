namespace TheSolutionBrothers.NFe.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TheSolutionsBrothers_Nfe_Receiver : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBAddress",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        City = c.String(nullable: false, maxLength: 80, unicode: false),
                        Country = c.String(nullable: false, maxLength: 50, unicode: false),
                        Neighborhood = c.String(nullable: false, maxLength: 60, unicode: false),
                        State = c.String(nullable: false, maxLength: 2, unicode: false),
                        StreetName = c.String(nullable: false, maxLength: 100, unicode: false),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBReceiver",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60, unicode: false),
                        CompanyName = c.String(maxLength: 60, unicode: false),
                        StateRegistration = c.String(maxLength: 15, unicode: false),
                        Cpf_Value = c.String(maxLength: 11, unicode: false),
                        Cnpj_Value = c.String(maxLength: 14, unicode: false),
                        Type = c.Int(nullable: false),
                        AddressId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBAddress", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBReceiver", "AddressId", "dbo.TBAddress");
            DropIndex("dbo.TBReceiver", new[] { "AddressId" });
            DropTable("dbo.TBReceiver");
            DropTable("dbo.TBAddress");
        }
    }
}
