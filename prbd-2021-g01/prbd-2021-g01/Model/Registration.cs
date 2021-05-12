using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;

namespace prbd_2021_g01.Model {
    public class Registration : EntityBase<EcoleContext>
    {
        [Key]
        public int Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }

        public virtual RegistrationState State { get; set; }

        public Registration() { }

        public Registration(Student s, Course c, RegistrationState rs )
        {
            Student = s;
            s?.registrations.Add(this);
            Course = c;
            c?.registrations.Add(this);
            State = rs;
        }


        public static RegistrationState getRegistrationState(Student student, Course course)
        {
            var registrations = from r in Context.Registrations
                                where r.Student.Id == student.Id && r.Course.Id == course.Id
                                select r;
            return registrations.FirstOrDefault() == null ? RegistrationState.Inactive: registrations.FirstOrDefault().State;

        }

        public void changeStatus(RegistrationState newState)
        {
            this.State = newState;
        }

    }

    public enum RegistrationState
    {
        Active,
        Pending,
        Inactive

    }
}
