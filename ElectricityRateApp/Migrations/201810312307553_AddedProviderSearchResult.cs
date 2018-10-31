namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProviderSearchResult : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProviderSearchResults");
        }
    }
}
