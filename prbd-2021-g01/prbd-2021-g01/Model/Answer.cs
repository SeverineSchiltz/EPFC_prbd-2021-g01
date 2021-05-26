using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;

namespace prbd_2021_g01.Model {
    public class Answer : EntityBase<EcoleContext>
    {
        [Key]
        public int Id { get; set; }
        public virtual Question Question { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }


        public Answer(Question question, string content, bool isCorrect)
        {
            Question = question;
            Question.Answers.Add(this);
            Content = content;
            IsCorrect = isCorrect;
        }

        public Answer() { }

    }
}
