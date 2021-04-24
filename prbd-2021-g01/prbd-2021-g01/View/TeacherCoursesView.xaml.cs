using PRBD_Framework;

namespace prbd_2021_g01.View {
    /// <summary>
    /// Logique d'interaction pour CoursesView.xaml
    /// </summary>
    public partial class TeacherCoursesView : UserControlBase {
        public TeacherCoursesView() {
            InitializeComponent();
        }
        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            vm.DisplayCourseDetails.Execute(listView.SelectedItem);
        }
    }
}
