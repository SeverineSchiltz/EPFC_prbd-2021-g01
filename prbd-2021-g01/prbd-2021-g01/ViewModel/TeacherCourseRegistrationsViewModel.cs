using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;
using System.Collections.ObjectModel;
using System.Windows.Input;
using prbd_2021_g01.Model;

namespace prbd_2021_g01.ViewModel {
    public class TeacherCourseRegistrationsViewModel : ViewModelCommon {

        private ObservableCollection<Registration> registrations;
        public ObservableCollection<Registration> Registrations {
            get => registrations;
            set => SetProperty<ObservableCollection<Registration>>(ref registrations, value);
        }

        private ObservableCollectionFast<Student> students;
        public ObservableCollectionFast<Student> Students {
            get { return students; }
            set {
                students = value;
                RaisePropertyChanged(nameof(Students));
            }

        }

        private Course course;
        public Course Course {
            get => course;
            set => SetProperty(ref course, value, OnRefreshData);
        }

        /*private string inactiveStudent;

        public string InactiveStudent { 
            get => inactiveStudent; 
            set => SetProperty(ref inactiveStudent, value); 
        }*/

        public TeacherCourseRegistrationsViewModel() : base() {
            Registrations = new ObservableCollectionFast<Registration>(App.Context.Registrations);
            //Students = new ObservableCollectionFast<Student>(Student.GetInactiveStudentsByCourse(course));
        }

        protected override void OnRefreshData() {
        }
    }
}
