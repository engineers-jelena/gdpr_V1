namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateModified_fix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "DateDeleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "DateDeleted", c => c.DateTime(nullable: false));
        }
    }
}
