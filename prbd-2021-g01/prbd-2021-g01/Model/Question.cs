using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;

namespace prbd_2021_g01.Model {
    public class Question : EntityBase<EcoleContext>
    {
        [Key]
        public int Id { get; set; }
        public virtual Course Course { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new HashSet<QuizQuestion>();
        public virtual ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public Question(Course course, string title, string content)
        {
            Course = course;
            Title = title;
            Content = content;
        }

        public Question() { }

        public static IQueryable<Question> GetQuestions(Course course)
        {
            //faut d'abord updater en BDD sinon, la requete linq ne fonctionne pas car le IsChecked n'est pas à jour!
            Context.SaveChanges();
            var questions = from q in Context.Questions
                            where q.Course.Id == course.Id && q.Categories.Any(c => c.IsChecked && c.Course.Id == course.Id)
                            select q;
            ////Pour tester
            //List<Question> test = questions.Cast<Question>().ToList();

            return questions;
        }

        public string GetAnswersAsString()
        {
            string answers = "";
            foreach (Answer answer in Answers)
            {
                answers += (answer.IsCorrect ? "*" : "") + answer.Content + "\n";
            }

            return answers;
        }

        //public static List<Question> GetQuestions(Course course, List<Category> listCat)
        //{
        //    var question = new List<Question>();
        //    foreach (var c in listCat)
        //    {
        //        var qt = from q in Context.Questions
        //                        where q.Course.Id == course.Id && q.Categories.Contains(c)
        //                        select q;
        //        foreach (var m in qt)
        //        {
        //            question.Add(m);
        //        }

        //    }
        //    return question;
        //}



    }
}
