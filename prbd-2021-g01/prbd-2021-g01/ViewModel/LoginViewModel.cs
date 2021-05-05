using System;
using System.Windows.Input;
using prbd_2021_g01.Model;
using prbd_2021_g01.Properties;
using PRBD_Framework;

namespace prbd_2021_g01.ViewModel {
    public class LoginViewModel : ViewModelCommon {

        public event Action OnTeacherLoginSuccess;
        public event Action OnStudentLoginSuccess;
        public event Action OnSignup;

        public ICommand LoginCommand { get; set; }
        public ICommand SignUp { get; set; }

        private string email = "test@epfc.eu";

        public string Email { get => email; set => SetProperty(ref email, value, () => Validate()); }

        private string password = "Password2,";
        public string Password { get => password; set => SetProperty<string>(ref password, value, () => Validate()); }

        public LoginViewModel() : base() {
            LoginCommand = new RelayCommand(
                LoginAction,
                () => { return email != null && password != null && !HasErrors; }
            );
            SignUp = new RelayCommand(
                SignUpAction
            );
        }

        private void LoginAction() {
            if (Validate()) {
                var user = User.GetByEmail(Email);
                //var user = Context.Users.Find(Email);
                Login(user);
                if (user.GetType() == typeof(Teacher))
                {
                    OnTeacherLoginSuccess?.Invoke();
                }
                else
                {
                    OnStudentLoginSuccess?.Invoke();
                }
            }
        }

        private void SignUpAction() {
            OnSignup?.Invoke();
        }

        public override bool Validate() {
            ClearErrors();

            var user = User.GetByEmail(Email);
            //var user = Context.Users.Find(Email);

            if (string.IsNullOrEmpty(Email))
                AddError(nameof(Email), Resources.Error_Required);
            else if (Email.Length < 3)
                AddError(nameof(Email), Resources.Error_LengthGreaterEqual3);
            else if (user == null)
                AddError(nameof(Email), Resources.Error_DoesNotExist);
            else {
                if (string.IsNullOrEmpty(Password))
                    AddError(nameof(Password), Resources.Error_Required);
                else if (Password.Length < 3)
                    AddError(nameof(Password), Resources.Error_LengthGreaterEqual3);
                else if (user != null && user.Password != Password)
                    AddError(nameof(Password), Resources.Error_WrongPassword);
            }

            RaiseErrors();
            return !HasErrors;
        }

        
        protected override void OnRefreshData() {
        }
    }
}
