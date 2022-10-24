namespace MvcOnlineTicariOtomasyon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newMig : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Carilers", newName: "Caris");
            RenameTable(name: "dbo.Faturalars", newName: "Faturas");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Faturas", newName: "Faturalars");
            RenameTable(name: "dbo.Caris", newName: "Carilers");
        }
    }
}
