using prbd_2021_g01.Model;
using PRBD_Framework;
using System.Windows;

namespace prbd_2021_g01.View {
    /// <summary>
    /// Logique d'interaction pour CourseDetailView.xaml
    /// </summary>
    public partial class TeacherCourseDetailView : UserControlBase {
        public TeacherCourseDetailView(Course course, bool isNew) {
            InitializeComponent();
            vm.Init(course, isNew);
        }


    }
}
