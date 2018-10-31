namespace ElectricityRateApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedResidentialChargResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResidentialChargeResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(),
                        City = c.String(),
                        StateAbbreviation = c.String(),
                        Rate = c.Double(nullable: false),
                        Charge = c.Double(nullable: false),
                        Usage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ResidentialChargeResults");
        }
    }
}
