using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PRBD_Framework;
using System.Collections.Generic;
using prbd_2021_g01.Model;

namespace prbd_2021_g01.ViewModel {
    public class TeacherCoursesViewModel : ViewModelCommon {

        private ObservableCollectionFast<Course> courses;
        public ObservableCollectionFast<Course> Courses {
            get => courses;
            set => SetProperty<ObservableCollectionFast<Course>>(ref courses, value);
        }

        private string title;

        public string Title { get => title; set => SetProperty(ref title, value); }

        private string description;

        public string Description { get => description; set => SetProperty(ref description, value); }

        private string teacher; // TODO: ask question about comment (what about readonly?)

        public string Teacher { get { return teacher.ToString(); } set => SetProperty(ref teacher, value); }


        private string filter;
        public string Filter {
            get => filter;
            set => SetProperty<string>(ref filter, value, ApplyFilterAction);
        }
        private void ApplyFilterAction() {
            var query = from c in Course.GetCoursesByTeacher((Teacher)CurrentUser) //from c in Context.Courses where
                        where c.Title.Contains(Filter) || c.Description.Contains(Filter) select c;
            Courses = new ObservableCollectionFast<Course>(query);
        }

      

        public ICommand ClearFilter { get; set; }
        public ICommand NewCourse { get; set; }
        public ICommand DisplayCourseDetails { get; set; }

        public TeacherCoursesViewModel() : base() {
            //Courses = new ObservableCollection<Course>(App.Context.Courses);

            //CoursesByTeacher = new ObservableCollection<Course>(CoursesByTeacherAction());


            Courses = new ObservableCollectionFast<Course>(Course.GetCoursesByTeacher((Teacher)CurrentUser));

            ClearFilter = new RelayCommand(() => Filter = "");

            NewCourse = new RelayCommand(() => { NotifyColleagues(AppMessages.MSG_NEW_COURSE); });

            Register<Course>(this, AppMessages.MSG_COURSE_CHANGED, course => {
                ApplyFilterAction();  //OnRefreshData();
            });

            DisplayCourseDetails = new RelayCommand<Course>(course => {
                NotifyColleagues(AppMessages.MSG_DISPLAY_COURSE, course);
            });

            Register(this, AppMessages.MSG_STUDENT_CHANGED, () => {
                OnRefreshData();
            });

            //AllSelected = true;
        }

        protected override void OnRefreshData() {
            //reset properties
            Courses.Reset(Course.GetCoursesByTeacher((Teacher)CurrentUser));
            RaisePropertyChanged();
        }
    }
}
