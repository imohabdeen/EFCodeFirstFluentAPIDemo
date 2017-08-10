namespace EFCodeFirstFluentAPIDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Admin.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "Admin.StudentCourse",
                c => new
                    {
                        StudentRefId = c.Int(nullable: false),
                        CourseRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentRefId, t.CourseRefId })
                .ForeignKey("Admin.StudentInfo", t => t.StudentRefId, cascadeDelete: true)
                .ForeignKey("Admin.Courses", t => t.CourseRefId, cascadeDelete: true)
                .Index(t => t.StudentRefId)
                .Index(t => t.CourseRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Admin.StudentCourse", "CourseRefId", "Admin.Courses");
            DropForeignKey("Admin.StudentCourse", "StudentRefId", "Admin.StudentInfo");
            DropIndex("Admin.StudentCourse", new[] { "CourseRefId" });
            DropIndex("Admin.StudentCourse", new[] { "StudentRefId" });
            DropTable("Admin.StudentCourse");
            DropTable("Admin.Courses");
        }
    }
}
