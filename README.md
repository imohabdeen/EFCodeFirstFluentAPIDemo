# EFCodeFirstFluentAPIDemo
Entity Framework code first fluent API &amp; implement relations between classes demo

## Install Entity Framework package
install-package EntityFramework

## Enable Migrations in project
Enable-Migrations

## Add Migration
Add-Migration InitialCreation

## Update database
Update-Database

## Database Creation
database will be created on the following instance (LocalDb)\MSSQLLocalDB

## Configure One-to-Zero-or-One Relationship
            //Entity1 can be associated with zero or only one instance of Entity2.
            //One-to-zero-or-one relationship happens when the primary key of one table becomes PK & FK in 
            //another table in relational database such as SQL Server.
            //Entity 1 = Student 
            //Entity 2 = StudentAddress
            //So, we have to configure StudentId in Student table as PK and StudentAddressId column in 
            //StudentAddress table as PK and FK both.
            
## Configure One-to-Many Relationship
            //one Standard can include many Students. So the relation between Student and Standard entities would be one-to-many.
            
## Configure Many-to-Many relationship            
            // Student can join multiple courses and multiple students can join one course
