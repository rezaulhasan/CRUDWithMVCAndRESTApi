namespace RentMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedCustomerWithMembershipType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "MembershipTypeId", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "MembershipTypeId");
        }
    }
}
