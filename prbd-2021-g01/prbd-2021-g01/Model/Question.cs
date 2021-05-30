using System;
using System.Collections;
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
        //public string Content { get; set; }
        public virtual TypeOfQuest Type { get => GetTypeOfQuestion(); }

        //public virtual ICollection<Category> GetCategoriesIsChecked { get => GetCategories(); }

        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new HashSet<QuizQuestion>();
        public virtual ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public Question(Course course, string title)
        {
            Course = course;
            Title = title;
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

        public static IEnumerable<Question> GetQuestionsExcept(IList questions, Course course)
        {
            return GetQuestionsCourse(course).Where(question => !questions.Contains(question));
        }

        public static IEnumerable<Question> GetQuestionsCourse(Course course)
        {
            return Context.Questions.Where(q => q.Course.Id == course.Id);
        }

        public static IQueryable<Question> GetAllQuestions()
        {
            //faut d'abord updater en BDD sinon, la requete linq ne fonctionne pas car le IsChecked n'est pas à jour!
            Context.SaveChanges();
            var questions = from q in Context.Questions
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
                answers += (answer.IsCorrect ? "*" : "") + answer.Content + "\r\n";
            }

            return answers;
        }

        public void SetAnswersAsString(string answers)
        {
            foreach (Answer a in Answers)
            {
                Answers.Remove(a);
                Context.Answers.Remove(a);
            }
            
            string[] asw = answers.Split("\r\n");
            foreach(string str in asw)
            {
                
                if (str != "")
                {
                    Answer a;
                    if (str.Substring(0, 1) == "*")
                    {
                        a = new Answer(this, str.Substring(1, str.Length - 1), true);
                    }
                    else
                    {
                        a = new Answer(this, str, false);
                    }

                    Answers.Add(a);
                    Context.Answers.Add(a);
                }

            }
            Context.SaveChanges();


        }

        public bool save()
        {
            bool hasCategory = false;
            foreach (Category ca in Category.GetCategories(this.Course))
            {
                if (ca.IsCheckedForQuestion)
                {
                    hasCategory = true;
                }
            }
            if (hasCategory)
            {
                foreach (Category ca in Category.GetCategories(this.Course))
                {
                    ca.deleteQuestion(this);
                    if (ca.IsCheckedForQuestion)
                    {
                        ca.addQuestion(this);
                    }
                }
                if (Context.Questions.Any(q => q.Id == this.Id))
                {
                    Context.Questions.Update(this);
                }
                else
                {
                    Context.Questions.Add(this);
                    
                }
                Context.SaveChanges();
            }

            return hasCategory;
        }

        public void delete()
        {
            if (Context.Questions.Any(q => q.Id == this.Id))
            {
                Context.Questions.Remove(this);
            }

            Context.SaveChanges();
        }

        public TypeOfQuest GetTypeOfQuestion()
        {
            var ans = from a in Context.Answers
                      where a.Question.Id == this.Id && a.IsCorrect
                      select a;

            return ans.Count() > 1? TypeOfQuest.Multi: TypeOfQuest.One;
        }

        //public ICollection<Category> GetCategories()
        //{

        //    var categories = from c in Context.Categories
        //                    where c.Course.Id == this.Course.Id
        //                    select c;
        //    foreach(Category cat in categories)
        //    {
        //        if(cat.Questions.Any(q => q.Id == this.Id))
        //        {
        //            cat.IsCheckedForQuestion = true;
        //        }
        //        else
        //        {
        //            cat.IsCheckedForQuestion = false;
        //        }
        //    }

        //    return categories.Cast<Category>().ToList();
        //}

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

    public enum TypeOfQuest
    {
        One,
        Multi

    }
}
