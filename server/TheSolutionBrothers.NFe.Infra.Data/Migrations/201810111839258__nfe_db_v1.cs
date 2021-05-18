namespace TheSolutionBrothers.NFe.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _nfe_db_v1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.TBInvoice", name: "KeyAccess_Value", newName: "KeyAccess");
            AlterColumn("dbo.TBInvoice", "KeyAccess", c => c.String(maxLength: 44, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBInvoice", "KeyAccess", c => c.String(maxLength: 44, unicode: false));
            RenameColumn(table: "dbo.TBInvoice", name: "KeyAccess", newName: "KeyAccess_Value");
        }
    }
}
