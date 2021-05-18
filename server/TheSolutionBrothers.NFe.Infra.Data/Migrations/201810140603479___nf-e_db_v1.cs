namespace TheSolutionBrothers.NFe.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class __nfe_db_v1 : DbMigration
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
                "dbo.TBCarrier",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60, unicode: false),
                        CompanyName = c.String(maxLength: 60, unicode: false),
                        CPF = c.String(maxLength: 11, unicode: false),
                        CNPJ = c.String(maxLength: 14, unicode: false),
                        StateRegistration = c.String(maxLength: 15, unicode: false),
                        FreightResponsability = c.Int(nullable: false),
                        PersonType = c.Int(nullable: false),
                        AddressId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBAddress", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.TBInvoiceItem",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IcmsAliquot = c.Double(),
                        IpiAliquot = c.Double(),
                        Amount = c.Long(nullable: false),
                        UnitValue = c.Double(),
                        Code = c.String(maxLength: 14, unicode: false),
                        Description = c.String(maxLength: 60, unicode: false),
                        InvoiceId = c.Long(nullable: false),
                        ProductId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBInvoice", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.TBProduct", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.TBInvoice",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        NatureOperation = c.String(nullable: false, maxLength: 70, unicode: false),
                        KeyAccess = c.String(maxLength: 44, fixedLength: true, unicode: false),
                        Number = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        EntryDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IssueDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        SenderId = c.Long(nullable: false),
                        ReceiverId = c.Long(nullable: false),
                        CarrierId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBCarrier", t => t.CarrierId)
                .ForeignKey("dbo.TBReceiver", t => t.ReceiverId)
                .ForeignKey("dbo.TBSender", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.CarrierId);
            
            CreateTable(
                "dbo.TBReceiver",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60, unicode: false),
                        CompanyName = c.String(maxLength: 60, unicode: false),
                        StateRegistration = c.String(maxLength: 15, unicode: false),
                        CPF = c.String(maxLength: 11, unicode: false),
                        CNPJ = c.String(maxLength: 14, unicode: false),
                        Type = c.Int(nullable: false),
                        AddressId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBAddress", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.TBSender",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FancyName = c.String(nullable: false, maxLength: 60, unicode: false),
                        CompanyName = c.String(maxLength: 60, unicode: false),
                        Cnpj_Value = c.String(maxLength: 14, unicode: false),
                        StateRegistration = c.String(maxLength: 15, unicode: false),
                        MunicipalRegistration = c.String(maxLength: 15, unicode: false),
                        AddressId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBAddress", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.TBProduct",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 14, unicode: false),
                        Description = c.String(nullable: false, maxLength: 60, unicode: false),
                        CurrentValue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBUser",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBInvoiceItem", "ProductId", "dbo.TBProduct");
            DropForeignKey("dbo.TBInvoiceItem", "InvoiceId", "dbo.TBInvoice");
            DropForeignKey("dbo.TBInvoice", "SenderId", "dbo.TBSender");
            DropForeignKey("dbo.TBSender", "AddressId", "dbo.TBAddress");
            DropForeignKey("dbo.TBInvoice", "ReceiverId", "dbo.TBReceiver");
            DropForeignKey("dbo.TBReceiver", "AddressId", "dbo.TBAddress");
            DropForeignKey("dbo.TBInvoice", "CarrierId", "dbo.TBCarrier");
            DropForeignKey("dbo.TBCarrier", "AddressId", "dbo.TBAddress");
            DropIndex("dbo.TBSender", new[] { "AddressId" });
            DropIndex("dbo.TBReceiver", new[] { "AddressId" });
            DropIndex("dbo.TBInvoice", new[] { "CarrierId" });
            DropIndex("dbo.TBInvoice", new[] { "ReceiverId" });
            DropIndex("dbo.TBInvoice", new[] { "SenderId" });
            DropIndex("dbo.TBInvoiceItem", new[] { "ProductId" });
            DropIndex("dbo.TBInvoiceItem", new[] { "InvoiceId" });
            DropIndex("dbo.TBCarrier", new[] { "AddressId" });
            DropTable("dbo.TBUser");
            DropTable("dbo.TBProduct");
            DropTable("dbo.TBSender");
            DropTable("dbo.TBReceiver");
            DropTable("dbo.TBInvoice");
            DropTable("dbo.TBInvoiceItem");
            DropTable("dbo.TBCarrier");
            DropTable("dbo.TBAddress");
        }
    }
}
