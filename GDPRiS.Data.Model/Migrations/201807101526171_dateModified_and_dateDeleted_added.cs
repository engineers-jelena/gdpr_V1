namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateModified_and_dateDeleted_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "DateDeleted", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DateDeleted");
            DropColumn("dbo.Users", "DateModified");
        }
    }
}
