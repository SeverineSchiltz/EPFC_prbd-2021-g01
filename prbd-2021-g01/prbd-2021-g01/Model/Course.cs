using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;
using prbd_2021_g01;
using prbd_2021_g01.ViewModel;
using System.Collections;

namespace prbd_2021_g01.Model
{
    public class Course : EntityBase<EcoleContext> {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual Teacher Teacher { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //public int MaxStudent { get; set; }
        private int maxStudent;
        public int MaxStudent {
            get { return maxStudent; }
            set {
                if (NumberOfActiveStudents <= value) {
                    maxStudent = value;
                }
            }
        }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public RegistrationState OneRegistration { get => getRegisteredStatus((Student)App.CurrentUser); }

        public int NumberOfActiveStudents { get => getNumberOfActiveStudentsByCourse(); }
        public int NumberOfPendingStudents { get => getNumberOfPendingStudentsByCourse(); }
        public int NumberOfInactiveStudents { get => getNumberOfInactiveStudentsByCourse(); }

        /*public int NumberOfActiveAndPendingStudents { get => getNumberOfActiveAndPendingStudentsByCourse(); }*/

        public bool HasRegistration { get => !isRegistered((Student)App.CurrentUser); }
        public virtual ICollection<Registration> registrations { get; set; } = new HashSet<Registration>();

        public Course(Teacher teacher, string title, int maxStudent, string description = "") {
            //Teacher.Add(teacher);
            Teacher = teacher;
            // method will used only if teacher is not null
            Teacher?.Courses.Add(this);
            Title = title;
            Description = description;
            MaxStudent = maxStudent;
        }

        public Course() { }

        public static Course GetByTitle(string title) {
            return Context.Courses.SingleOrDefault(m => m.Title == title);
        }
        public static Course GetById(int id) {
            return Context.Courses.SingleOrDefault(c => c.Id == id);
        }

        public bool isRegistered(Student s) {
            return Registration.getRegistrationState(s, this) == RegistrationState.Active;
        }

        public RegistrationState getRegisteredStatus(Student s) {
            return Registration.getRegistrationState(s, this);
        }


        public static IQueryable<Course> GetCoursesByTeacher(Teacher teacher) {
            var query = from c in Context.Courses
                        where teacher.Id == c.Teacher.Id
                        select c;
            return query;
        }

        public int getNumberOfActiveStudentsByCourse() {
            return Registration.getNumberOfActiveStudentsByCourse(this);
        }

        public int getNumberOfPendingStudentsByCourse() {
            return Registration.getNumberOfPendingStudentsByCourse(this);
        }

        public int getNumberOfInactiveStudentsByCourse() {
            return Registration.getNumberOfInactiveStudentsByCourse(this);
        }

        /*public int getNumberOfActiveAndPendingStudentsByCourse() {
            return Registration.getNumberOfActiveAndPendingStudentsByCourse(this);
        }*/

        public void makeActiveStudents(IList selectedStudents) {
            if (NumberOfActiveStudents + selectedStudents.Count <= MaxStudent) {
                foreach (Student s in selectedStudents) {
                    if (this.getRegisteredStatus(s) == RegistrationState.Inactive) {
                        Registration reg = Context.Registrations.FirstOrDefault(r => r.Student.Id == s.Id && r.Course.Id == this.Id);
                        if (reg != null) {
                            reg.State = RegistrationState.Active;
                            Context.Registrations.Update(reg);

                        } else {
                            reg = new Registration(s, this, RegistrationState.Active);
                            Context.Registrations.AddRange(reg);
                        }
                    }
                }
            }
            Context.SaveChanges(); // update in db
        } 


        /*public void makeInactiveStudents_oldVersion(IList selectedStudents) {
            foreach (Student s in selectedStudents) {
                if (this.getRegisteredStatus(s) == RegistrationState.Active ||
                    this.getRegisteredStatus(s) == RegistrationState.Pending) {
                    Registration reg = Context.Registrations.FirstOrDefault(r => r.Student.Id == s.Id && r.Course.Id == this.Id);
                    if (reg != null) {
                        //reg.changeStatus(RegistrationState.Inactive);
                        reg.State = RegistrationState.Inactive;
                        Context.Registrations.Update(reg);
                    }
                }
            }
            Context.SaveChanges(); // update in db
        }*/

        public void makeInactiveStudents(IList selectedStudents) { // registrations list
            IList students = new ArrayList();
            foreach (Registration r in selectedStudents) {
                students.Add(r.Student);
            }
            foreach (Student s in students) {
                if (this.getRegisteredStatus(s) == RegistrationState.Active ||
                    this.getRegisteredStatus(s) == RegistrationState.Pending) {
                    Registration reg = Context.Registrations.FirstOrDefault(r => r.Student.Id == s.Id && r.Course.Id == this.Id);
                    if (reg != null) {
                        //reg.changeStatus(RegistrationState.Inactive);
                        reg.State = RegistrationState.Inactive;
                        Context.Registrations.Update(reg);
                    }
                }
            }
            Context.SaveChanges(); // update in db
        }

        public void changeStatusToInactiveOrActive(Registration registration) { // 1 registration
            Student student = registration.Student;
            if (this.getRegisteredStatus(student) == RegistrationState.Active) {
                registration.State = RegistrationState.Inactive;
                Context.Registrations.Update(registration);
            } else if (this.getRegisteredStatus(student) == RegistrationState.Pending) {
                registration.State = RegistrationState.Active;
                Context.Registrations.Update(registration);
            }
            Context.SaveChanges(); // update in db
        }


        public void Delete() {
            Teacher.Courses.Remove(this);
            Context.Courses.Remove(this);
            Context.SaveChanges();
        }


    }

}
