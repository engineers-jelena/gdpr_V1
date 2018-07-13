namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initail_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 300),
                        Email = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Role = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
