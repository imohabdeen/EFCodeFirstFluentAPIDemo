namespace EFCodeFirstFluentAPIDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StandardInfo",
                c => new
                    {
                        StandardId = c.Int(nullable: false, identity: true),
                        StandardName = c.String(),
                    })
                .PrimaryKey(t => t.StandardId);
            
            CreateTable(
                "Admin.StudentInfo",
                c => new
                    {
                        DoB = c.DateTime(precision: 7, storeType: "datetime2"),
                        StudentID = c.Int(nullable: false, identity: true),
                        StudentName = c.String(maxLength: 50, fixedLength: true),
                        Photo = c.Binary(),
                        Height = c.Decimal(precision: 2, scale: 2),
                        Weight = c.Single(nullable: false),
                        Standard_StandardId = c.Int(),
                    })
                .PrimaryKey(t => t.StudentID)
                .ForeignKey("dbo.StandardInfo", t => t.Standard_StandardId)
                .Index(t => t.Standard_StandardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Admin.StudentInfo", "Standard_StandardId", "dbo.StandardInfo");
            DropIndex("Admin.StudentInfo", new[] { "Standard_StandardId" });
            DropTable("Admin.StudentInfo");
            DropTable("dbo.StandardInfo");
        }
    }
}
