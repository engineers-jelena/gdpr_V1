namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrelationemployeecompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cars", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Cars", "DateDeleted", c => c.DateTime());
            AddColumn("dbo.Companies", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Companies", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Companies", "DateDeleted", c => c.DateTime());
            AddColumn("dbo.Employees", "CompanyId", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "DateModified", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "DateDeleted", c => c.DateTime());
            CreateIndex("dbo.Employees", "CompanyId");
            AddForeignKey("dbo.Employees", "CompanyId", "dbo.Companies", "idCompany");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropIndex("dbo.Employees", new[] { "CompanyId" });
            DropColumn("dbo.Employees", "DateDeleted");
            DropColumn("dbo.Employees", "DateModified");
            DropColumn("dbo.Employees", "DateCreated");
            DropColumn("dbo.Employees", "CompanyId");
            DropColumn("dbo.Companies", "DateDeleted");
            DropColumn("dbo.Companies", "DateModified");
            DropColumn("dbo.Companies", "DateCreated");
            DropColumn("dbo.Cars", "DateDeleted");
            DropColumn("dbo.Cars", "DateModified");
            DropColumn("dbo.Cars", "DateCreated");
        }
    }
}
