namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRateComparisonResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RateComparisonResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(),
                        City1 = c.String(),
                        StateAbbreviation1 = c.String(),
                        Rate1 = c.Double(nullable: false),
                        Difference = c.Double(nullable: false),
                        City2 = c.String(),
                        StateAbbreviation2 = c.String(),
                        Rate2 = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RateComparisonResults");
        }
    }
}
