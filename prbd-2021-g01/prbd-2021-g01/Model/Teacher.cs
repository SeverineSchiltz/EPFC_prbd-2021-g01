using System;
using System.Collections.Generic;
using PRBD_Framework;

namespace prbd_2021_g01.Model {
    public class Teacher : User {

        //[Key]
        //public int TeacherId { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();

        public void AddCourse(Course course)
        {
            Courses.Add(course);
            Context.SaveChanges();
        }

        public Teacher(string firstname, string lastname, string email, string password) : base(firstname, lastname, email, password) {

        }

        public Teacher() { }

        public override string ToString() {
            return $"{Firstname} {Lastname}";
        }

    }
}
