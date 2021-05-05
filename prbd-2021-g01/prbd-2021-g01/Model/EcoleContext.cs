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

            // rendre l'email unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
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

            var bruno = new Teacher("Bruno","Lacroix","br@epfc.eu", "Password1,");
            //Teachers.AddRange(new[] { testTeacher }); // when we need to create many objects

            var benoit = new Teacher("Benoît", "Penelle", "be@epfc.eu", "Password1,");

            var boris = new Teacher("Boris", "Verhaegen", "bo@epfc.eu", "Password1,");

            var etudiant = new Student("Gakusei", "Sensei", "test@epfc.eu", "Password2,");

            var anc3 = new Course(bruno, "2002 - ANC3", 5, "Projet d'analyse et conception");
            var prbd = new Course(bruno, "2007 - PRBD", 5, "Projet de développement SGBD");
            var prwb = new Course(benoit, "1927 - PRWB", 5, "Projet de développement Web");
            var tgpr = new Course(benoit, "2000 - TGPR", 5, "Techniques de gestion de projets");
            var prm2 = new Course(boris, "5635 - PRM2", 5, "Principes algorithmiques et programmation");
            var pro2 = new Course(boris, "1995 - PRO2", 5, "Programmation orientée objets");

            var firstRegistration = new Registration(etudiant, anc3, RegistrationState.Active);
            var secondRegistration = new Registration(etudiant, prbd, RegistrationState.Pending);

            //bruno.AddCourse(anc3); 
            Courses.AddRange(anc3, prbd, prwb, tgpr, prm2, pro2);

            Users.AddRange(bruno, benoit, boris, etudiant);

            Registrations.AddRange(firstRegistration, secondRegistration);

            SaveChanges();

            Database.CommitTransaction();
        }

       
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Quiz> Quizz { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerStudent> AnswerStudents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }

    }
}