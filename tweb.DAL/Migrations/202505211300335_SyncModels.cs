namespace tweb.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "UserId", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "UserId" });
            DropColumn("dbo.OrderItems", "TotalAmount");
            DropTable("dbo.Cards");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CardNumber = c.String(),
                        CardHolder = c.String(),
                        ExpirationDate = c.String(),
                        CVV = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.OrderItems", "TotalAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            CreateIndex("dbo.Cards", "UserId");
            AddForeignKey("dbo.Cards", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
