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


    }
}
