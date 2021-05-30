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

        public Student(string firstname, string lastname, string email, string password) : base(firstname, lastname, email, password) {

        }

        public Student() { }

        public void add() {
            Context.Students.Add(this);
            Context.SaveChanges();
        }

        public void changeRegistrationStatus(Course course)
        {
            //var filtered = from m in Context.Members
            //               where m.Pseudo.Contains(Filter) || m.Profile.Contains(Filter)
            //               orderby m.Pseudo
            //               select m;

            var reg = registrations.FirstOrDefault(r => r.Course.Id == course.Id && r.Student.Id == this.Id);
            if (reg == null)
            {
                reg = new Registration(this, course, RegistrationState.Pending);
                this.registrations.Add(reg);
                course.registrations.Add(reg);
                Context.Registrations.Add(reg);
            }
            else if(reg.State == RegistrationState.Pending)
            {

                this.registrations.Remove(reg);
                course.registrations.Remove(reg);
                Context.Registrations.Remove(reg);
            }
            else
            {
                reg.changeStatus(RegistrationState.Active);
            }
                Context.SaveChanges();
        }

        /*public RegistrationState Status {
            get => getRegisteredStatus(Course c);  // not ok
        }*/

        /*public RegistrationState getRegisteredStatus(Course c) {
            return Registration.getRegistrationState(this, c);
        }*/

    }
}
