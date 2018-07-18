namespace GDPRiS.Data.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedenumsforcompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "MarkOfCar", c => c.Int(nullable: false));
            DropColumn("dbo.Cars", "typeOfCar");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cars", "typeOfCar", c => c.String());
            DropColumn("dbo.Cars", "MarkOfCar");
        }
    }
}
