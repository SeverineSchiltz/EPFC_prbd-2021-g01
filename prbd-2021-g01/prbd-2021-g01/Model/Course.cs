using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prbd_2021_g01.Model
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual Teacher Teacher { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxStudent { get; set; }
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

    }
}
