using PRBD_Framework;
using prbd_2021_g01.Model;
using System.Windows.Input;
using System;

namespace prbd_2021_g01.ViewModel
{
    public class TeacherMainViewModel : ViewModelCommon
    {
        public event Action OnLogout;

        public event Action<Course, bool> DisplayCourse;

        public event Action<Course, string> RenameTab;

        public ICommand LogoutCommand { get; set; }

        public TeacherMainViewModel() : base()
        {
            LogoutCommand = new RelayCommand(LogoutAction);
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
        }

        protected override void OnRefreshData()
        {
            // pour plus tard
        }

        private void LogoutAction() { 
            Logout(); 
            OnLogout?.Invoke(); 
        }
        public string Title { 
            get => $"AppSchool ({CurrentUser.Firstname} {CurrentUser.Lastname} - Teacher)"; 
        }

    }

}
