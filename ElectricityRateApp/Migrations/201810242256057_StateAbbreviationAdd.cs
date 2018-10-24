namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StateAbbreviationAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PowerRates", "StateAbbreviation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PowerRates", "StateAbbreviation");
        }
    }
}
