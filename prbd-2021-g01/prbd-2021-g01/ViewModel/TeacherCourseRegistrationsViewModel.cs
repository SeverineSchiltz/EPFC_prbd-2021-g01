using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRBD_Framework;
using System.Collections.ObjectModel;
using System.Windows.Input;
using prbd_2021_g01.Model;
using System.Collections;

namespace prbd_2021_g01.ViewModel {
    public class TeacherCourseRegistrationsViewModel : ViewModelCommon {

        private ObservableCollectionFast<Registration> registrations; // = new ObservableCollectionFast<Registration>();
        public ObservableCollectionFast<Registration> Registrations {
            get => registrations;
            set => SetProperty(ref registrations, value);
        }

        private string filter;
        public string Filter {
            get => filter;
            set => SetProperty<string>(ref filter, value, ApplyFilterAction);
        }

        private void ApplyFilterAction() {
            var query = from s in Registration.GetInactiveStudentsByCourse(course) 
                        where s.Firstname.Contains(Filter) || s.Lastname.Contains(Filter) 
                        select s;
            InactiveStudents = new ObservableCollectionFast<Student>(query);
        }

        public ICommand ClearFilter { get; set; }

        /*private Registration registration;
        public Registration Registration { 
            get => registration; 
            set => SetProperty(ref registration, value); 
        }

        public string State {
            get { 
                return Registration?.State.ToString(); }
            *//*set {
                Registration.State = (String)value;
                RaisePropertyChanged(nameof(State));
                //NotifyColleagues(AppMessages.MSG_TITLE_CHANGED, Course);
            }*//*
        }*/

        private ObservableCollectionFast<Student> inactiveStudents;
        public ObservableCollectionFast<Student> InactiveStudents {
            get { return inactiveStudents; }
            set {
                inactiveStudents = value;
                RaisePropertyChanged(nameof(InactiveStudents));
            }
        }

        private ObservableCollectionFast<Student> activeOrPendingStudents;
        public ObservableCollectionFast<Student> ActiveOrPendingStudents {
            get { return activeOrPendingStudents; }
            set {
                activeOrPendingStudents = value;
                RaisePropertyChanged(nameof(ActiveOrPendingStudents));
            }
        }


        private Course course;
        public Course Course {
            get => course;
            set => SetProperty(ref course, value, OnRefreshData);
        }

        private IList inactiveStudentSelectedItems = new ArrayList();
        public IList InactiveStudentSelectedItems {
            get => inactiveStudentSelectedItems;
            set => SetProperty(ref inactiveStudentSelectedItems, value);
        }

        private IList activeOrPendingStudentSelectedItems = new ArrayList();
        public IList ActiveOrPendingStudentSelectedItems {
            get => activeOrPendingStudentSelectedItems;
            set => SetProperty(ref activeOrPendingStudentSelectedItems, value);
        }

        public ICommand UnregAllSelect { get; set; }
        public ICommand UnregSelect { get; set; }
        public ICommand RegSelect { get; set; }
        public ICommand RegAllSelect { get; set; }


        public TeacherCourseRegistrationsViewModel() : base() {
            UnregAllSelect = new RelayCommand(MakeThemAllInactiveAction);
            UnregSelect = new RelayCommand(MakeInactiveAction);
            RegSelect = new RelayCommand(MakeActiveAction);
            RegAllSelect = new RelayCommand(MakeThemAllActiveAction);

            ClearFilter = new RelayCommand(() => Filter = "");
        }

        protected override void OnRefreshData() {
            InactiveStudents = new ObservableCollectionFast<Student>(Registration.GetInactiveStudentsByCourse(course));
            ActiveOrPendingStudents = new ObservableCollectionFast<Student>(Registration.GetActiveAndPendingStudentsByCourse(course));
        }

        public void MakeThemAllInactiveAction() {
            course.makeInactiveStudents(activeOrPendingStudents);
            ResetAndNotify();
        }

        public void MakeInactiveAction() {
            course.makeInactiveStudents(activeOrPendingStudentSelectedItems);
            ResetAndNotify();
            /*InactiveStudents.Reset(Registration.GetInactiveStudentsByCourse(course));
            ActiveOrPendingStudents.Reset(Registration.GetActiveOrPendingStudentsByCourse(course));
            RaisePropertyChanged();
            notify();*/
        }

        public void MakeActiveAction() {
            course.makeActiveStudents(inactiveStudentSelectedItems);
            //course.makeInactiveStudents(activeOrPendingStudentSelectedItems);
            ResetAndNotify();
            /*InactiveStudents.Reset(Registration.GetInactiveStudentsByCourse(course)); 
            ActiveOrPendingStudents.Reset(Registration.GetActiveOrPendingStudentsByCourse(course));
            RaisePropertyChanged();
            notify();*/
        }

        public void MakeThemAllActiveAction() {
            course.makeActiveStudents(inactiveStudents);
            ResetAndNotify();
        }

        public void ResetAndNotify() { // notify() {
            InactiveStudents.Reset(Registration.GetInactiveStudentsByCourse(course)); // check if refresh in db or cache ?
            ActiveOrPendingStudents.Reset(Registration.GetActiveAndPendingStudentsByCourse(course));
            RaisePropertyChanged();
            NotifyColleagues(AppMessages.MSG_STUDENT_CHANGED);
        }


    }
}
