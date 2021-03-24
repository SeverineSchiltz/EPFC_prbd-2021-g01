using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PRBD_Framework;

namespace prbd_2021_g01.Model
{
    public class EcoleContext : DbContextBase {

        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => {
            builder.AddConsole();
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ecole")
                .EnableSensitiveDataLogging()
                //.UseLoggerFactory(_loggerFactory)
                .UseLazyLoadingProxies(true)
                ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
                
            //// l'entité Member participe à une relation one-to-many ...
            //modelBuilder.Entity<Member>()
            //    // avec, du côté many, la propriété MessagesSent ...
            //    .HasMany(member => member.MessagesSent)
            //    // avec, du côté one, la propriété Author ...
            //    .WithOne(msg => msg.Author)
            //    // et pour laquelle on désactive le delete en cascade
            //    .OnDelete(DeleteBehavior.Restrict);

            //// l'entité Member participe à une relation one-to-many ...
            //modelBuilder.Entity<Member>()
            //    // avec, du côté many, la propriété MessagesReceived ...
            //    .HasMany(member => member.MessagesReceived)
            //    // avec, du côté one, la propriété Recipient ...
            //    .WithOne(msg => msg.Recipient)
            //    // et pour laquelle on désactive le delete en cascade
            //    .OnDelete(DeleteBehavior.Restrict);
        }

        public void SeedData() {
            Database.BeginTransaction();

            //add data

            var testTeacher1 = new Teacher("testTeacher1", "test2021");
            //Teachers.AddRange(new[] { testTeacher }); // when we need to create many objects

            var testTeacher2 = new Teacher("testTeacher2", "test2021");

            var testStudent = new Student("testStudent", "test2021");

            Users.AddRange(testTeacher1, testTeacher2, testStudent);   

            SaveChanges();

            Database.CommitTransaction();
        }

       
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Quiz> Quizz { get; set; }

    }
}