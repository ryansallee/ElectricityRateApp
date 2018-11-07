namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToUtilitySearcResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UtilitySearchResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(),
                        City = c.String(),
                        StateAbbreviation = c.String(),
                        UtilityName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.ProviderSearchResults");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProviderSearchResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(),
                        City = c.String(),
                        StateAbbreviation = c.String(),
                        ProviderName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.UtilitySearchResults");
        }
    }
}
