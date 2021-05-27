using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using prbd_2021_g01.Model;
using PRBD_Framework;

namespace prbd_2021_g01.ViewModel {
    public class TeacherCourseDetailViewModel : ViewModelCommon {

        private Course course;
        public Course Course { get => course; set => SetProperty(ref course, value); }
        //public Course Course {
        //    get { return Course; }
        //    set {
        //        course = value;
        //        RaisePropertyChanged(nameof(Course));
        //    }
        //}


        private bool isNew;
        public bool IsNew {
            get { return isNew; }
            set {
                isNew = value;
                RaisePropertyChanged(nameof(IsNew), nameof(IsExisting));
            }
        }

        public TeacherCourseCategoriesViewModel CourseCategories { get; private set; } = new TeacherCourseCategoriesViewModel();


        public TeacherCourseRegistrationsViewModel CourseRegistrations { get; private set; } = new TeacherCourseRegistrationsViewModel();

        public TeacherCourseQuestionsViewModel CourseQuestions { get; private set; } = new TeacherCourseQuestionsViewModel();


        public bool IsExisting { get => !isNew; }

        public string Title {
            get { return Course?.Title; }
            set {
                Course.Title = value;
                RaisePropertyChanged(nameof(Title));
                NotifyColleagues(AppMessages.MSG_TITLE_CHANGED, Course);
            }
        }

        //private string description;
        public string Description {
            get { return Course?.Description; }
            set  { // => SetProperty(ref description, value);
                Course.Description = value;
                RaisePropertyChanged(nameof(Description));
                }
        }

        public int MaxStudent {
            get {
                if (Course?.MaxStudent == null)
                    return 0;
                else
                    return Course.MaxStudent; }
            set {
                Course.MaxStudent = value;
                RaisePropertyChanged(nameof(MaxStudent));
            }
        }

        private string teacher;
        public string Teacher {
            get { return CurrentUser.ToString(); }
            set => SetProperty(ref teacher, value);
        }
        //public string Teacher { get { return CurrentUser.ToString(); }  }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public TeacherCourseDetailViewModel() : base() {
            SaveCommand = new RelayCommand(SaveAction, CanSaveAction); // CanSaveOrCancelAction);
            CancelCommand = new RelayCommand(CancelAction, CanCancelAction);
            DeleteCommand = new RelayCommand(DeleteAction, () => !IsNew); // () => !IsNew bc btn shouldn't be active if we are on a new course

        }

        public void Init(Course course, bool isNew) {
            // Bind properties of child ViewModel
            this.BindOneWay(nameof(Course), CourseCategories, nameof(CourseCategories.Course));

            this.BindOneWay(nameof(Course), CourseRegistrations, nameof(CourseRegistrations.Course));

            this.BindOneWay(nameof(Course), CourseQuestions, nameof(CourseQuestions.Course));

            // Il faut recharger ce cours dans le contexte courant pour pouvoir le modifier
            Course = isNew ? course : Course.GetByTitle(course.Title);
            IsNew = isNew;

            RaisePropertyChanged();
        }

        protected override void OnRefreshData() {
        }

        private void SaveAction() {
            if (IsNew) {
                Teacher CurrentTeacher = (Teacher) CurrentUser;
                Course course = new Course(CurrentTeacher, Title, MaxStudent, Description);
                CurrentTeacher.AddCourse(course);
                IsNew = false;
            }
            Context.SaveChanges();
            NotifyColleagues(AppMessages.MSG_COURSE_CHANGED, Course);
        }

        private bool CanSaveAction() {
            if (IsNew)
                return !string.IsNullOrEmpty(Title);
            return Course != null && (Context?.Entry(Course)?.State == EntityState.Modified);
        }

        private void CancelAction() {
            if (IsNew) { // close tab if we need to cancel while editing a new course
                NotifyColleagues(AppMessages.MSG_CLOSE_TAB, Course);
                // NotifyColleagues because no access to the tab control
            } else {    // course exists =>
                Context.Reload(Course); // reload with object to load data from db
                RaisePropertyChanged(); // notify the view that all bound properties need to be refreshed 
                // and Context?.Entry(Course)?.State will be "unchanged" so btn will be deactivated
            }
        }

        private bool CanCancelAction() {
            // course not null && isnew (new course: btn active) or
            // course not null && there are changes 
            // EF is used to know more about the state: enum to know if object is modified
            // asking to change tracker what is the state, if modified: true
            return Course != null && (IsNew || Context?.Entry(Course)?.State == EntityState.Modified);
        }

        private void DeleteAction() {
            CancelAction();
            Course.Delete();
            NotifyColleagues(AppMessages.MSG_COURSE_CHANGED, Course);
            NotifyColleagues(AppMessages.MSG_CLOSE_TAB, Course);
        }

        private ObservableCollection<Course> courses;
        public ObservableCollection<Course> Courses {
            get => courses;
            set => SetProperty<ObservableCollection<Course>>(ref courses, value);
        }

    }

}
