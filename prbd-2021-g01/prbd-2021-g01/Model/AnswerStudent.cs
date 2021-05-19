using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;

namespace prbd_2021_g01.Model {
    public class AnswerStudent : EntityBase<EcoleContext>
    {
        [Key]
        public int Id { get; set; }
        public virtual QuizQuestion QuizQuestion { get; set; }
        public virtual Student Student { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();

        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
