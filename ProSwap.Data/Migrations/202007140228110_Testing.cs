namespace ProSwap.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Testing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Game", "CurrencyName", c => c.String());
            AddColumn("dbo.Game", "CurrencyId", c => c.Int());
            AddColumn("dbo.Game", "UnitPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Game", "UnitQuantity", c => c.Int());
            AddColumn("dbo.Game", "UnitLowValue", c => c.Double());
            AddColumn("dbo.Game", "UnitHighValue", c => c.Double());
            AddColumn("dbo.Game", "UnitMedianValue", c => c.Double());
            AddColumn("dbo.Game", "UnitAverage", c => c.Double());
            AddColumn("dbo.Game", "GameAccountId", c => c.Int());
            AddColumn("dbo.Game", "OwnerId", c => c.Guid());
            AddColumn("dbo.Game", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Game", "Discriminator");
            DropColumn("dbo.Game", "OwnerId");
            DropColumn("dbo.Game", "GameAccountId");
            DropColumn("dbo.Game", "UnitAverage");
            DropColumn("dbo.Game", "UnitMedianValue");
            DropColumn("dbo.Game", "UnitHighValue");
            DropColumn("dbo.Game", "UnitLowValue");
            DropColumn("dbo.Game", "UnitQuantity");
            DropColumn("dbo.Game", "UnitPrice");
            DropColumn("dbo.Game", "CurrencyId");
            DropColumn("dbo.Game", "CurrencyName");
        }
    }
}
