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

        private Course course;
        public Course Course {
            get => course;
            set => SetProperty(ref course, value, OnRefreshData);
        }

        public TeacherCourseRegistrationsViewModel() : base() {
            Registrations = new ObservableCollection<Registration>(App.Context.Registrations);
        }

        protected override void OnRefreshData() {
        }
    }
}
