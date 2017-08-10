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




        }
    }
}
