namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedStateAbbreviation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PowerRates", "StateAbbreviation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PowerRates", "StateAbbreviation", c => c.String());
        }
    }
}
