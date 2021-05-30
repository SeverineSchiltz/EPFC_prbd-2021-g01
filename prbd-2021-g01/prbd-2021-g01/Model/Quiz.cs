using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;

namespace prbd_2021_g01.Model {
    public class Quiz : EntityBase<EcoleContext>
    {
        [Key]
        public int Id { get; set; }
        public virtual Course Course { get; set; }
        public string Title { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public string Status { get => EndDateTime >= DateTime.Now ? "Active" : "Past"; }

        public Quiz(Course course, string title, DateTime startDateTime, DateTime endDateTime)
        {
            Course = course;
            Title = title;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
        }

        public Quiz() { }
        //public virtual ICollection<QuizQuestion> quizQuestions { get; set; } = new HashSet<QuizQuestion>();


        public static IQueryable<Quiz> GetQuizz(Course course) //Pour les students
        {
            //ne peut voir que les quizz passés et présents
            var quizz = from q in Context.Quizz
                           where q.Course.Id == course.Id && q.StartDateTime < DateTime.Now
                           select q;

            return quizz;
        }

        public static IQueryable<Quiz> GetQuizzTeacher(Course course)
        {
            var quizz = from q in Context.Quizz
                        where q.Course.Id == course.Id
                        select q;

            return quizz;
        }

    }
}
