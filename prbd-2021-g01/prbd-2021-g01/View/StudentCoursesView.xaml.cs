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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prbd_2021_g01.View
{
    /// <summary>
    /// Logique d'interaction pour StudentCoursesView.xaml
    /// </summary>
    public partial class StudentCoursesView : UserControlBase
    {
        public StudentCoursesView()
        {
            InitializeComponent();
        }
        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            vm.DisplayStudentCourseDetails.Execute(listView.SelectedItem);
        }


    }
}
