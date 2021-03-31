using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2021_g01.Model {
    public class AnswerStudent {
        [Key]
        public int Id { get; set; }
        public virtual QuizQuestion QuizQuestion { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
    }
}
