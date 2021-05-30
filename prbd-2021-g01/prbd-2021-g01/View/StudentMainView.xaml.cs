using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PRBD_Framework;
using prbd_2021_g01.Properties;
using prbd_2021_g01.Model;

namespace prbd_2021_g01.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StudentMainView : WindowBase
    {
        public StudentMainView()
        {
            InitializeComponent();
        }

        private void Vm_OnLogout()
        {
            App.NavigateTo<LoginView>();
        }

        private void WindowBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) Close();
        }
        private void Language_Click(object sender, RoutedEventArgs e)
        {
            var lang = (sender as MenuItem).CommandParameter?.ToString();
            App.ChangeCulture(lang);
            Settings.Default.Culture = lang;
            Settings.Default.Save();
        }

        private void Vm_DisplayStudentCourseDetails(Course course, Student student)
        {
            if (course != null && course.isRegistered(student))
            {
                var tag = "course-" + course.Id.ToString(); //clé unique de l'onglet!!
                var tab = tabControl.FindByTag(tag);
                if (tab == null)
                {
                    tabControl.Add(
                        new StudentCourseDetailsView(course),
                        course.Title,
                        tag
                    );
                }
                else
                {
                    tabControl.SetFocus(tab);
                }
            }
        }

        private void Menu_Profile_Click(object sender, RoutedEventArgs e) {
            var tag = "Profile";
            var tab = tabControl.FindByTag(tag);
            if (tab == null)
                tabControl.Add(new ProfileView(), Properties.Resources.Menu_Profile, tag);
            else
                tabControl.SetFocus(tab);
        }


        private void Vm_DisplayStudentQuiz(Quiz quiz)
        {
            if (quiz != null)
            {
                var tag = "quiz-" + quiz.Id.ToString(); 
                var tab = tabControl.FindByTag(tag);
                if (tab == null)
                {
                    tabControl.Add(
                        new StudentQuizView(quiz),
                        quiz.Title,
                        tag
                    );
                }
                else
                {
                    tabControl.SetFocus(tab);
                }
            }
        }

    }
}
