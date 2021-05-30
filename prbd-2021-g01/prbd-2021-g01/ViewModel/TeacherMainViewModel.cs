using PRBD_Framework;
using prbd_2021_g01.Model;
using System.Windows.Input;
using System;

namespace prbd_2021_g01.ViewModel
{
    public class TeacherMainViewModel : ViewModelCommon
    {
        public event Action OnLogout;

        //public event Action OnProfile;

        public event Action<Course, bool> DisplayCourse;

        public event Action<Course, string> RenameTab;

        public event Action<Course> CloseTab; // see Register

        public ICommand LogoutCommand { get; set; }
        //public ICommand ProfileCommand { get; set; }

        public TeacherMainViewModel() : base()
        {
            LogoutCommand = new RelayCommand(LogoutAction);
            //ProfileCommand = new RelayCommand(ProfileAction);
            Register(this, AppMessages.MSG_NEW_COURSE, () => {
                // crée une nouvelle instance pour un nouveau cours "vide"
                var course = new Course();
                // demande à la vue de créer dynamiquement un nouvel onglet avec le titre "<new course>"
                DisplayCourse?.Invoke(course, true);
            });
            Register<Course>(this, AppMessages.MSG_DISPLAY_COURSE, course => {
                // demande à la vue de créer dynamiquement un nouvel onglet avec le titre du course (sur lequel on double clique)
                DisplayCourse?.Invoke(course, false); // false: not a new course
            });
            Register<Course>(this, AppMessages.MSG_TITLE_CHANGED, course => {
                RenameTab?.Invoke(course, course.Title);
            });
            Register<Course>(this, AppMessages.MSG_CLOSE_TAB, course => {
                CloseTab?.Invoke(course);
            });
        }

        protected override void OnRefreshData()
        {
            // pour plus tard
        }

        private void LogoutAction() { 
            Logout(); 
            OnLogout?.Invoke(); 
        }

        //private void ProfileAction() {
        //    OnProfile?.Invoke();
        //}

        public string Title { 
            get => $"AppSchool ({CurrentUser.Firstname} {CurrentUser.Lastname} - Teacher)"; 
        }

    }

}
