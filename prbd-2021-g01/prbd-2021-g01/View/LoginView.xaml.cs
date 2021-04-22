using System;
using System.Windows;
using PRBD_Framework;

namespace prbd_2021_g01.View {
    /// <summary>
    /// Logique d'interaction pour LoginView.xaml
    /// </summary>
    public partial class LoginView : WindowBase {
        public LoginView() 
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) 
        {
            Close();
        }

        private void txtEmail_GotFocus(object sender, RoutedEventArgs e) {
            txtEmail.SelectAll();
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e) {
            txtPassword.SelectAll();
        }

        private void Vm_OnLoginSuccess() {
            App.NavigateTo<MainView>();
        }

        private void Vm_OnSignup() {
            App.NavigateTo<SignupView>();
        }

    }

}

