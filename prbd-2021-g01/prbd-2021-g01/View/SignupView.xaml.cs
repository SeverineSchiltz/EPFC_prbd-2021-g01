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

namespace prbd_2021_g01.View {
    /// <summary>
    /// Logique d'interaction pour SignupView.xaml
    /// </summary>
    public partial class SignupView : WindowBase {
        public SignupView() {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void txtFirstname_GotFocus(object sender, RoutedEventArgs e) {
            txtFirstname.SelectAll();
        }

        private void txtLastname_GotFocus(object sender, RoutedEventArgs e) {
            txtLastname.SelectAll();
        }
        private void txtEmail_GotFocus(object sender, RoutedEventArgs e) {
            txtEmail.SelectAll();
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e) {
            txtPassword.SelectAll();
        }

        private void txtPasswordConfirm_GotFocus(object sender, RoutedEventArgs e) {
            txtPasswordConfirm.SelectAll();
        }

        private void Vm_OnSignupSuccess() {
            App.NavigateTo<StudentMainView>();
        }

        private void Vm_OnLoginReturn() {
            App.NavigateTo<LoginView>();
        }

    }
}
