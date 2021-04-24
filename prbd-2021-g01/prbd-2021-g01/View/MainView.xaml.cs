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
    public partial class MainView : WindowBase
    {
        public MainView()
        {
            InitializeComponent();
        }

        /*private void Vm_DisplayCourse(Course course, bool isNew) {
            if (course != null) {
                //var tab = tabControl.GetByTitle(course.Title);
                var tab = tabControl.FindByTag(course.Title);
                if (tab == null)
                    tabControl.Add(
                        new CourseDetailView(course, isNew),
                        isNew ? "<new course>" : course.Title, course.Title
                    );
                else
                    tabControl.SetFocus(tab);
            }
        }*/

        private void Vm_OnLogout() { 
            App.NavigateTo<LoginView>(); 
        }

        private void WindowBase_KeyDown(object sender, KeyEventArgs e) { 
            if (e.Key == Key.Escape) Close(); 
        }
        private void Language_Click(object sender, RoutedEventArgs e) { 
            var lang = (sender as MenuItem).CommandParameter?.ToString(); 
            App.ChangeCulture(lang); 
            Settings.Default.Culture = lang; 
            Settings.Default.Save(); 
        }



    }
}
