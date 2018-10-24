namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCommercialRate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PowerRates", "CommercialRate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PowerRates", "CommercialRate", c => c.Double(nullable: false));
        }
    }
}
