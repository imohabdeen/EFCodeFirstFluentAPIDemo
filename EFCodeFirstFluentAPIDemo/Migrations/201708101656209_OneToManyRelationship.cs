namespace EFCodeFirstFluentAPIDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToManyRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Admin.StudentInfo", "Standard_StandardId", "dbo.StandardInfo");
            DropIndex("Admin.StudentInfo", new[] { "Standard_StandardId" });
            RenameColumn(table: "Admin.StudentInfo", name: "Standard_StandardId", newName: "StandardId");
            AlterColumn("Admin.StudentInfo", "StandardId", c => c.Int(nullable: false));
            CreateIndex("Admin.StudentInfo", "StandardId");
            AddForeignKey("Admin.StudentInfo", "StandardId", "dbo.StandardInfo", "StandardId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Admin.StudentInfo", "StandardId", "dbo.StandardInfo");
            DropIndex("Admin.StudentInfo", new[] { "StandardId" });
            AlterColumn("Admin.StudentInfo", "StandardId", c => c.Int());
            RenameColumn(table: "Admin.StudentInfo", name: "StandardId", newName: "Standard_StandardId");
            CreateIndex("Admin.StudentInfo", "Standard_StandardId");
            AddForeignKey("Admin.StudentInfo", "Standard_StandardId", "dbo.StandardInfo", "StandardId");
        }
    }
}
