using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PRBD_Framework;
using System.Collections.Generic;
using prbd_2021_g01.Model;

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

        private string title;

        public string Title { get => title; set => SetProperty(ref title, value); }

        private string description;

        public string Description { get => description; set => SetProperty(ref description, value); }

        public ICommand NewCourse { get; set; }
        public ICommand DisplayCourseDetails { get; set; }

        public StudentCoursesViewModel() : base()
        {
            Courses = new ObservableCollection<Course>(App.Context.Courses);

            NewCourse = new RelayCommand(() => { NotifyColleagues(AppMessages.MSG_NEW_COURSE); });

            Register<Course>(this, AppMessages.MSG_COURSE_CHANGED, Course => {
                OnRefreshData();
            });

            DisplayCourseDetails = new RelayCommand<Course>(Course => {
                NotifyColleagues(AppMessages.MSG_DISPLAY_COURSE, Course);
            });

            //AllSelected = true;
        }

        protected override void OnRefreshData()
        {

        }
    }
}
