using prbd_2021_g01.Model;
using PRBD_Framework;
using System.Windows;

namespace prbd_2021_g01.View
{
    /// <summary>
    /// Logique d'interaction pour TeacherCourseQuestionsView.xaml
    /// </summary>
    public partial class TeacherCourseQuestionsView : UserControlBase
    {
        public TeacherCourseQuestionsView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CourseProperty =
            DependencyProperty.Register(
            nameof(Course),                 // nom de la propriété
            typeof(Course),                 // type associé à la propriété
            typeof(TeacherCourseQuestionsView),     // type "propriétaire" qui déclare la propriété
            new PropertyMetadata(null)      // métadonnées associées qui définissent la valeur par défaut (ici null)
        );


        public Course Course
        {
            get { return (Course)GetValue(CourseProperty); }
            set { SetValue(CourseProperty, value); }
        }
    }
}
