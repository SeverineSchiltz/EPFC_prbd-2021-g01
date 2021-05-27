using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;
using prbd_2021_g01;
using prbd_2021_g01.ViewModel;

namespace prbd_2021_g01.Model
{
    public class Course : EntityBase<EcoleContext> {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual Teacher Teacher { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxStudent { get; set; }
        [Timestamp]
        public byte[] Timestamp { get; set; }

        public RegistrationState OneRegistration { get => getRegisteredStatus((Student)App.CurrentUser); }

        public int NumberOfActiveStudents { get => getNumberOfActiveStudentsByCourse(); }
        public int NumberOfPendingStudents { get => getNumberOfPendingStudentsByCourse(); }
        public int NumberOfInactiveStudents { get => getNumberOfInactiveStudentsByCourse(); }

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


        public Student getInactiveStudentsByCourse() {
            return (Student)Registration.GetInactiveStudentsByCourse(this);
        }

        public String InactiveStudentsNameByCourse { 
            get => getInactiveStudentsByCourse().ToString(); }

        public void Delete() {
            Teacher.Courses.Remove(this);
            Context.Courses.Remove(this);
            Context.SaveChanges();
        }



    }

}
