using System.Windows.Input;
using PRBD_Framework;
using prbd_2021_g01.Properties;
using prbd_2021_g01.Model;
using System.Windows;

namespace prbd_2021_g01.View {
    /// <summary>
    /// Logique d'interaction pour RegisteredStudentsView.xaml
    /// </summary>
    public partial class RegisteredStudentsView : UserControlBase {
        public RegisteredStudentsView() {
            InitializeComponent();
        }

        public static readonly DependencyProperty RegistrationProperty =
            DependencyProperty.Register(
                nameof(Registration),                 // nom de la propriété
                typeof(Registration),                 // type associé à la propriété
                typeof(RegisteredStudentsView), // type "propriétaire" qui déclare la propriété
                new PropertyMetadata(null)      // métadonnées associées qui définissent la valeur par défaut (ici null)
            );

        public Registration Registration {
            get { return (Registration)GetValue(RegistrationProperty); }
            set { SetValue(RegistrationProperty, value); }
        }

    }
}
