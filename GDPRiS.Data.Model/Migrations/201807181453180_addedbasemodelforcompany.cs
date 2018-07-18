namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedbasemodelforcompany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cars", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropPrimaryKey("dbo.Cars");
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Companies");
            AddColumn("dbo.Cars", "id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Employees", "id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Companies", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Cars", "id");
            AddPrimaryKey("dbo.Employees", "id");
            AddPrimaryKey("dbo.Companies", "id");
            AddForeignKey("dbo.Cars", "EmployeeId", "dbo.Employees", "id");
            AddForeignKey("dbo.Employees", "CompanyId", "dbo.Companies", "id");
            DropColumn("dbo.Cars", "idCar");
            DropColumn("dbo.Employees", "idEmployee");
            DropColumn("dbo.Companies", "idCompany");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Companies", "idCompany", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Employees", "idEmployee", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Cars", "idCar", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Employees", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Cars", "EmployeeId", "dbo.Employees");
            DropPrimaryKey("dbo.Companies");
            DropPrimaryKey("dbo.Employees");
            DropPrimaryKey("dbo.Cars");
            DropColumn("dbo.Companies", "id");
            DropColumn("dbo.Employees", "id");
            DropColumn("dbo.Cars", "id");
            AddPrimaryKey("dbo.Companies", "idCompany");
            AddPrimaryKey("dbo.Employees", "idEmployee");
            AddPrimaryKey("dbo.Cars", "idCar");
            AddForeignKey("dbo.Employees", "CompanyId", "dbo.Companies", "idCompany");
            AddForeignKey("dbo.Cars", "EmployeeId", "dbo.Employees", "idEmployee");
        }
    }
}
