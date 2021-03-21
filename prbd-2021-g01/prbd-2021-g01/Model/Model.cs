using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PRBD_Framework;

namespace prbd_2021_g01
{
    public class Model : DbContextBase {

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

            SaveChanges();

            Database.CommitTransaction();
        }

       
        public DbSet<Course> Course2 { get; set; }

    }
}