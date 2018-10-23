namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitalMigratiom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PowerRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZipCode = c.String(),
                        UtilityName = c.String(),
                        CommercialRate = c.Double(nullable: false),
                        ResidentialRate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PowerRates");
        }
    }
}
