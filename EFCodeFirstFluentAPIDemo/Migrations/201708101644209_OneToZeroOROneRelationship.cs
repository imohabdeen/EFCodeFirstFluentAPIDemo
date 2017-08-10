namespace EFCodeFirstFluentAPIDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToZeroOROneRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Admin.StudentAddresses",
                c => new
                    {
                        StudentAddressId = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        City = c.String(),
                        Zipcode = c.Int(nullable: false),
                        State = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.StudentAddressId)
                .ForeignKey("Admin.StudentInfo", t => t.StudentAddressId)
                .Index(t => t.StudentAddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Admin.StudentAddresses", "StudentAddressId", "Admin.StudentInfo");
            DropIndex("Admin.StudentAddresses", new[] { "StudentAddressId" });
            DropTable("Admin.StudentAddresses");
        }
    }
}
