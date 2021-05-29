using PRBD_Framework;
using prbd_2021_g01.Model;
using System.Windows.Input;
using System;

namespace prbd_2021_g01.ViewModel
{
    public class StudentMainViewModel : ViewModelCommon
    {
        public event Action OnLogout;
        public event Action<Course, Student> DisplayStudentCourseDetails;
        public event Action<Quiz> OnDisplayStudentQuiz;

        public ICommand LogoutCommand { get; set; }

        public StudentMainViewModel() : base()
        {
            LogoutCommand = new RelayCommand(LogoutAction);

            Register<Course>(this, AppMessages.MSG_DISPLAY_STUDENT_COURSE, course => {
                DisplayStudentCourseDetails?.Invoke(course, (Student) CurrentUser);
            });

            Register<Quiz>(this, AppMessages.MSG_DISPLAY_STUDENT_QUIZ, quiz => {
                // demande à la vue de créer dynamiquement un nouvel onglet avec le titre du course (sur lequel on double clique)
                OnDisplayStudentQuiz?.Invoke(quiz); // false: not a new course
            });


        }

        protected override void OnRefreshData()
        {
            // pour plus tard
        }

        private void LogoutAction()
        {
            Logout();
            OnLogout?.Invoke();
        }
        public string Title
        {
            get => $"AppSchool ({CurrentUser.Firstname} {CurrentUser.Lastname} - Student)";
        }

    }

}
