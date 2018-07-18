namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedrelationcaremployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cars", "EmployeeId");
            AddForeignKey("dbo.Cars", "EmployeeId", "dbo.Employees", "idEmployee");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Cars", new[] { "EmployeeId" });
            DropColumn("dbo.Cars", "EmployeeId");
        }
    }
}
