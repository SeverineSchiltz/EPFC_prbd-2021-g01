using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;

namespace prbd_2021_g01.Model {
    public class Category : EntityBase<EcoleContext>
    {

        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual Course Course { get; set; } 
        public virtual ICollection<Question> Questions { get; set; } = new HashSet<Question>();
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public int nbOfQuestions{ get => Questions.Count(); }

        private bool isChecked = true;
        public bool IsChecked
        {
            get => isChecked;
            set => isChecked = value;
        }

        public Category(Course course, string title)
        {
            Course = course;
            Title = title;
        }

        public Category() { }

        public static IQueryable<Category> GetCategories(User observer, Course course)
        {

            var category = from c in Context.Categories
                           where c.Course.Id == course.Id
                           select c;

            return category;
        }

        public void addQuestion(Question q)
        {
            q.Categories.Add(this);
            this.Questions.Add(q);
            Context.SaveChanges();
        }

        public static void updateOrAddCategoriesInCourse(List<Category> listCat, Course course)
        {
            foreach(Category c in listCat)
            {
                if (Context.Categories.Any(ca => ca.Id == c.Id))
                {
                    Context.Categories.Update(c);
                    
                }
                else
                {
                    c.Course = course;
                    Context.Categories.AddRange(c);
                }
                
            }
            Context.SaveChanges();
        }

        public static void removeCategories(Category[] listCat)
        {
            if (listCat != null )
            {
                foreach (Category c in listCat)
                {
                    if (Context.Categories.Any(ca => ca.Id == c.Id))
                    {
                        Context.Categories.Remove(c);
                    }
                }
                Context.SaveChanges();
            }

        }


    }
}
