namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRelation : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Phones");
            AddColumn("dbo.Phones", "PhoneId", c => c.Int(nullable: false));
            AlterColumn("dbo.Phones", "id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Phones", "id");
            CreateIndex("dbo.Phones", "id");
            AddForeignKey("dbo.Phones", "id", "dbo.Users", "Id");
            DropColumn("dbo.Phones", "ClientId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Phones", "ClientId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Phones", "id", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "id" });
            DropPrimaryKey("dbo.Phones");
            AlterColumn("dbo.Phones", "id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Phones", "PhoneId");
            AddPrimaryKey("dbo.Phones", "Id");
        }
    }
}
