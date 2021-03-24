using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2021_g01.Model {
    public class Student : User {
        // ! Error if we keep StudentId
        //[Key]
        //public int StudentId { get; set; }
        public virtual ICollection<Registration> registrations { get; set; } = new HashSet<Registration>();

        // To check
        //public virtual ICollection<ResponseStudent> responseStudents { get; set; } = new HashSet<ResponseStudent>();

        public Student(string email, string password) : base(email, password) {

        }

        public Student() { }
    }
}
