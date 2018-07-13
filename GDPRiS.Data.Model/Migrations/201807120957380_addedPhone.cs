namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPhone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Phones");
        }
    }
}
