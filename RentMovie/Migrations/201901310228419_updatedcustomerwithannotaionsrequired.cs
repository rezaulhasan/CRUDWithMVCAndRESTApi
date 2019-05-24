namespace RentMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedcustomerwithannotaionsrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Customers", "Birthdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "Birthdate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(maxLength: 255));
        }
    }
}
