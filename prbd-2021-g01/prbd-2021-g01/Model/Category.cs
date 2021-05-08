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
    }
}
