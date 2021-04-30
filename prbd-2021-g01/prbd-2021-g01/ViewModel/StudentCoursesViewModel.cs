using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PRBD_Framework;
using System.Collections.Generic;
using prbd_2021_g01.Model;
using System.Drawing;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using Colors = System.Windows.Media.Colors;

namespace prbd_2021_g01.ViewModel
{
    public class StudentCoursesViewModel : ViewModelCommon
    {

        private ObservableCollection<Course> courses;
        public ObservableCollection<Course> Courses
        {
            get => courses;
            set => SetProperty<ObservableCollection<Course>>(ref courses, value);
        }

        private string filter;
        public string Filter {
            get => filter;
            set => SetProperty<string>(ref filter, value, ApplyFilterAction);
        }
        private void ApplyFilterAction() {
            var query = from c in Context.Courses where
                        c.Title.Contains(Filter) || c.Description.Contains(Filter) select c;
            Courses = new ObservableCollection<Course>(query);
        }


        public ICommand DisplayStudentCourseDetails { get; set; }

        //public SolidColorBrush ColorCourse
        //{
        //    get
        //    {
        //        //return Color.FromRgb(255,0,0);
        //        return new SolidColorBrush(Colors.Red);
        //    }
        //}


        public ICommand ClearFilter { get; set; }

        public StudentCoursesViewModel() : base()
        {
            Courses = new ObservableCollection<Course>(App.Context.Courses);

            ClearFilter = new RelayCommand(() => Filter = "");

            DisplayStudentCourseDetails = new RelayCommand<Course>(course => {
                NotifyColleagues(AppMessages.MSG_DISPLAY_STUDENT_COURSE, course);
            });

            //AllSelected = true;
        }

        protected override void OnRefreshData()
        {

        }

    }
}
