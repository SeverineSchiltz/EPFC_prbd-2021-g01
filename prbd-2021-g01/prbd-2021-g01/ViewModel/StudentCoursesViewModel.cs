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

        private ObservableCollectionFast<Course> courses;
        public ObservableCollectionFast<Course> Courses
        {
            get => courses;
            set => SetProperty<ObservableCollectionFast<Course>>(ref courses, value);
        }

        private string filter;
        public string Filter {
            get => filter;
            set => SetProperty<string>(ref filter, value, ApplyFilterAction);
        }
        private void ApplyFilterAction() {
            var query = from c in Context.Courses where
                        c.Title.Contains(Filter) || c.Description.Contains(Filter) select c;
            Courses = new ObservableCollectionFast<Course>(query);
        }

        private Course selectedItem = new Course();
        public Course SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        public ICommand DisplayStudentCourseDetails { get; set; }

        public ICommand ClearFilter { get; set; }
        public ICommand RegiState { get; set; }


        public StudentCoursesViewModel() : base()
        {
            Courses = new ObservableCollectionFast<Course>(App.Context.Courses);

            ClearFilter = new RelayCommand(() => Filter = "");

            DisplayStudentCourseDetails = new RelayCommand<Course>(course => {
                NotifyColleagues(AppMessages.MSG_DISPLAY_STUDENT_COURSE, course);
            });

            //RegiState = new RelayCommand(ChangeRegistrationStatusAction);
            RegiState = new RelayCommand<Course>(ChangeRegistrationStatusAction, selectedItems => {
                return !Context.ChangeTracker.HasChanges();
            });
        }

        private void ChangeRegistrationStatusAction(Course course)
        {
            ((Student)CurrentUser).changeRegistrationStatus(course);
            //Pour updater l'écran:
            Courses.Reset(App.Context.Courses);
            //Notifie aux autres écrans:
            NotifyColleagues(AppMessages.MSG_REFRESH_REGISTRATION_STATE, null);

        }

        protected override void OnRefreshData()
        {

        }

    }
}
