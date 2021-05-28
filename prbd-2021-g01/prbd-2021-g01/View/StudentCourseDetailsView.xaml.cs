using prbd_2021_g01.Model;
using PRBD_Framework;


namespace prbd_2021_g01.View
{
    /// <summary>
    /// Logique d'interaction pour StudentCourseDetailsView.xaml
    /// </summary>
    public partial class StudentCourseDetailsView : UserControlBase
    {
        public StudentCourseDetailsView()
        {
            InitializeComponent();
        }

        public StudentCourseDetailsView(Course course)
        {
            InitializeComponent();
            vm.Init(course);
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            vm.DisplayQuizCourseDetails.Execute(grid.SelectedItem);
        }

    }
}
