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
        
        private bool isCheckedForQuestion = false;
        public bool IsCheckedForQuestion
        {
            get => isCheckedForQuestion;
            set => isCheckedForQuestion = value;
        }


        public Category(Course course, string title)
        {
            Course = course;
            Title = title;
        }

        public Category() { }

        public static IQueryable<Category> GetCategories(Course course)
        {

            var category = from c in Context.Categories
                           where c.Course.Id == course.Id
                           select c;

            return category;
        }


        public static IQueryable<Category> GetCategories(Course course, Question qt)
        {
            var categories = GetCategories(course);

            foreach (Category cat in categories)
            {
                if (qt!= null && cat.Questions.Any(q => q.Id == qt.Id))
                {
                    cat.IsCheckedForQuestion = true;
                }
                else
                {
                    cat.IsCheckedForQuestion = false;
                }
            }

            return categories;
        }

        public void addQuestion(Question q)
        {
            q.Categories.Add(this);
            this.Questions.Add(q);
            //Context.SaveChanges();
        }

        public void deleteQuestion(Question q)
        {
            if(q.Categories.Any(c => c.Id == this.Id))
            {
                q.Categories.Remove(this);
            }
            if (this.Questions.Any(qt => qt.Id == q.Id))
            {
                this.Questions.Remove(q);
            }
            
            //Context.SaveChanges();
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

        public void unSelect()
        {
            this.IsChecked = false;
        }

        public void select()
        {
            this.IsChecked = true;
        }

        public static IQueryable<Category> GetCategoriesForQuestion(Course co, Question q)
        {

            var category = from c in Context.Categories
                           where c.Course.Id == co.Id
                           select c;

            foreach(Category ca in category)
            {
                if(ca.Questions.Any(qt => qt.Id == q.Id))
                {
                    ca.isCheckedForQuestion = true;
                }
                else
                {
                    ca.isCheckedForQuestion = false;
                }
            }
            //changer checkedfoquest
            return category;
        }


    }
}
