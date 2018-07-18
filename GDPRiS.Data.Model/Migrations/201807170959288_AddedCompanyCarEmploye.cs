namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCompanyCarEmploye : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        idCar = c.Int(nullable: false, identity: true),
                        typeOfCar = c.String(),
                    })
                .PrimaryKey(t => t.idCar);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        idCompany = c.Int(nullable: false, identity: true),
                        nameOfCompany = c.String(),
                    })
                .PrimaryKey(t => t.idCompany);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        idEmployee = c.Int(nullable: false, identity: true),
                        NameOfEmployee = c.String(),
                    })
                .PrimaryKey(t => t.idEmployee);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
            DropTable("dbo.Companies");
            DropTable("dbo.Cars");
        }
    }
}
