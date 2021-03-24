using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2021_g01.Model {
    public class Teacher : User {

        //[Key]
        //public int TeacherId { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();

        public Teacher(string email, string password) : base( email, password) {

        }

        public Teacher() { }
    }
}
