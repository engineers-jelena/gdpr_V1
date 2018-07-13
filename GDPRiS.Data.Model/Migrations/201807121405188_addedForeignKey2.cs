namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedForeignKey2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phones", "id", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "id" });
            DropPrimaryKey("dbo.Phones");
            AddColumn("dbo.Phones", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Phones", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Phones", "Id");
            CreateIndex("dbo.Phones", "UserId");
            AddForeignKey("dbo.Phones", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.Phones", "PhoneId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Phones", "PhoneId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Phones", "UserId", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "UserId" });
            DropPrimaryKey("dbo.Phones");
            AlterColumn("dbo.Phones", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Phones", "UserId");
            AddPrimaryKey("dbo.Phones", "id");
            CreateIndex("dbo.Phones", "id");
            AddForeignKey("dbo.Phones", "id", "dbo.Users", "Id");
        }
    }
}
