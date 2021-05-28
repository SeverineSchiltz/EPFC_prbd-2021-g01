using prbd_2021_g01.Model;
using PRBD_Framework;


namespace prbd_2021_g01.View
{
    /// <summary>
    /// Logique d'interaction pour StudentQuizView.xaml
    /// </summary>
    public partial class StudentQuizView : UserControlBase
    {
        public StudentQuizView()
        {
            InitializeComponent();
        }

        public StudentQuizView(Quiz quiz)
        {
            InitializeComponent();
            vm.Init(quiz);
        }

    }
}
