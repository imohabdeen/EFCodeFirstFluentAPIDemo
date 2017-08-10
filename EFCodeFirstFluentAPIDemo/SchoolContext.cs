using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCodeFirstFluentAPIDemo
{
    class SchoolContext : DbContext
    {
        public SchoolContext(): base() 
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Standard> Standards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("Admin");

            //Map entity to table
            modelBuilder.Entity<Student>().ToTable("StudentInfo");
            modelBuilder.Entity<Standard>().ToTable("StandardInfo", "dbo");

            //Confgiure Primary key
            modelBuilder.Entity<Student>().HasKey<int>(s => s.StudentID);
            modelBuilder.Entity<Standard>().HasKey<int>(s => s.StandardId);

            //Configure column 
            modelBuilder.Entity<Student>()
                   .Property(p => p.DateOfBirth)
                   .HasColumnName("DoB")
                   .HasColumnOrder(3)
                   .HasColumnType("datetime2");

            //Configure Null Column
            modelBuilder.Entity<Student>()
                    .Property(p => p.Height)
                    .IsOptional();

            //Configure NotNull Column
            modelBuilder.Entity<Student>()
                .Property(p => p.Weight)
                .IsRequired();

            //Set StudentName column size to 50
            modelBuilder.Entity<Student>()
                    .Property(p => p.StudentName)
                    .HasMaxLength(50);

            //Set StudentName column size to 50 and change datatype to nchar 
            //IsFixedLength() change datatype from nvarchar to nchar
            modelBuilder.Entity<Student>()
                    .Property(p => p.StudentName)
                    .HasMaxLength(50).IsFixedLength();

            //Set size decimal(2,2)
            modelBuilder.Entity<Student>()
                .Property(p => p.Height)
                .HasPrecision(2, 2);



            //Map Entity to Multiple Table
            //************************************************************************
            //modelBuilder.Entity<Student>().Map(m =>
            //{
            //    m.Properties(p => new { p.StudentId, p.StudentName });
            //    m.ToTable("StudentInfo");

            //}).Map(m => {
            //    m.Properties(p => new { p.StudentId, p.Height, p.Weight, p.Photo, p.DateOfBirth });
            //    m.ToTable("StudentInfoDetail");

            //});
            //modelBuilder.Entity<Standard>().ToTable("StandardInfo");
            //************************************************************************

            //Map Entity to Multiple Table - Another way 
            //************************************************************************
            //modelBuilder.Entity<Student>().Map(delegate (EntityMappingConfiguration<Student> studentConfig)
            //{
            //    studentConfig.Properties(p => new { p.StudentId, p.StudentName });
            //    studentConfig.ToTable("StudentInfo");
            //});

            //Action<EntityMappingConfiguration<Student>> studentMapping = m =>
            //{
            //    m.Properties(p => new { p.StudentId, p.Height, p.Weight, p.Photo, p.DateOfBirth });
            //    m.ToTable("StudentInfoDetail");
            //};
            //modelBuilder.Entity<Student>().Map(studentMapping);

            //modelBuilder.Entity<Standard>().ToTable("StandardInfo");
            //************************************************************************

            //Configure Relationships

            //Configure One-to-Zero-or-One Relationship
            //************************************************************************
            //Entity1 can be associated with zero or only one instance of Entity2.
            //One-to-zero-or-one relationship happens when the primary key of one table becomes PK & FK in 
            //another table in relational database such as SQL Server.
            //Entity 1 = Student 
            //Entity 2 = StudentAddress
            //So, we have to configure StudentId in Student table as PK and StudentAddressId column in 
            //StudentAddress table as PK and FK both.
            // Configure Student & StudentAddress entity
            modelBuilder.Entity<Student>()
                        .HasOptional(s => s.Address) // Mark Address property optional in Student entity
                        .WithRequired(ad => ad.Student); // mark Student property as required in StudentAddress entity. Cannot save StudentAddress without Student

            // Configure StudentId as FK for StudentAddress - one to one relationship
            //modelBuilder.Entity<Student>()
            //            .HasRequired(s => s.Address)
            //            .WithRequiredPrincipal(ad => ad.Student);


            //Configure One-to-Many Relationship
            //************************************************************************
            //one Standard can include many Students. So the relation between Student and Standard entities would be one-to-many.
            modelBuilder.Entity<Student>()
                        .HasRequired<Standard>(s => s.Standard) // Student entity requires Standard 
                        .WithMany(s => s.Students); // Standard entity includes many Students entities

            //one-to-many 
            //If forign key is nullable
            //modelBuilder.Entity<Student>()
            //            .HasOptional<Standard>(s => s.Standard)
            //            .WithMany(s => s.Students);

            //We can start with standard class
            //modelBuilder.Entity<Standard>()
            //            .HasMany<Student>(s => s.Students) //Standard has many Students
            //            .WithRequired(s => s.Standard);  //Student require one Standard


            //Configure Many-to-Many relationship
            //************************************************************************
            // Student can join multiple courses and multiple students can join one course
            modelBuilder.Entity<Student>()
               .HasMany<Course>(s => s.Courses)
               .WithMany(c => c.Students)
               .Map(cs =>
               {
                   cs.MapLeftKey("StudentRefId");
                   cs.MapRightKey("CourseRefId");
                   cs.ToTable("StudentCourse");
               });

        }
    }
}
