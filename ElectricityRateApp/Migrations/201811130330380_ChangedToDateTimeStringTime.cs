namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedToDateTimeStringTime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UtilitySearchResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        City = c.String(),
                        StateAbbreviation = c.String(),
                        UtilityName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.RateComparisonResults", "Time", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ResidentialChargeResults", "Time", c => c.DateTime(nullable: false));
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
            
            AlterColumn("dbo.ResidentialChargeResults", "Time", c => c.String());
            AlterColumn("dbo.RateComparisonResults", "Time", c => c.String());
            DropTable("dbo.UtilitySearchResults");
        }
    }
}
