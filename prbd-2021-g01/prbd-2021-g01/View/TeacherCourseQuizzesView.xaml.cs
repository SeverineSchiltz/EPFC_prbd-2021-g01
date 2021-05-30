using prbd_2021_g01.Model;
using PRBD_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace prbd_2021_g01.View
{
    /// <summary>
    /// Logique d'interaction pour TeacherCourseQuizzesView.xaml
    /// </summary>
    public partial class TeacherCourseQuizzesView : UserControlBase
    {

        public static readonly DependencyProperty CourseProperty =
          DependencyProperty.Register(
          nameof(Course),                 // nom de la propriété
          typeof(Course),                 // type associé à la propriété
          typeof(TeacherCourseQuizzesView),     // type "propriétaire" qui déclare la propriété
          new PropertyMetadata(null)      // métadonnées associées qui définissent la valeur par défaut (ici null)
      );


        public Course Course
        {
            get { return (Course)GetValue(CourseProperty); }
            set { SetValue(CourseProperty, value); }
        }

        public TeacherCourseQuizzesView()
        {
            InitializeComponent();
        }
    }
}
