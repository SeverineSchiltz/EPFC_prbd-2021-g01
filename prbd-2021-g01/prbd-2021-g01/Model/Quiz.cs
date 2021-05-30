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
        public virtual ICollection<QuizQuestion> Questions { get; set; } = new HashSet<QuizQuestion>();

        public int nbOfQuestions { get => Questions.Count(); }
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


        public static IQueryable<Quiz> GetQuizzStudent(Course course)
        {
            //ne peut voir que les quizz passés et présents
            var quizz = from q in Context.Quizz
                           where q.Course.Id == course.Id && q.StartDateTime < DateTime.Now
                           select q;

            return quizz;
        }

        public static IQueryable<Quiz> GetQuizzes(Course course)
        {
            //peut voir tous les quiz
            var quiz = from q in Context.Quizz
                        where q.Course.Id == course.Id
                        select q;

            return quiz;
        }

        public static void updateOrAddQuizzesInCourse(List<Quiz> listQuiz, Course course)
        {
            foreach (Quiz q in listQuiz)
            {
                if (Context.Quizz.Any(qu => qu.Id == q.Id))
                {
                    Context.Quizz.Update(q);

                }
                else
                {
                    q.Course = course;
                    Context.Quizz.AddRange(q);
                }

            }
            Context.SaveChanges();
        }

        public static void removeQuizzes(Quiz[] listQuiz)
        {
            if (listQuiz != null)
            {
                foreach (Quiz q in listQuiz)
                {
                    if (Context.Quizz.Any(qu => qu.Id == q.Id))
                    {
                        Context.Quizz.Remove(q);
                    }
                }
                Context.SaveChanges();
            }

        }
    }
}
