using System;
using System.Windows.Input;
using prbd_2021_g01.Model;
using prbd_2021_g01.Properties;
using PRBD_Framework;

namespace prbd_2021_g01.ViewModel {
    class SignupViewModel : ViewModelCommon {

        public event Action OnLoginReturn;
        public event Action OnSignupSuccess;

        public ICommand SignupCommand { get; set; } 
        public ICommand LoginReturnCommand { get; set; }

        private string firstname;

        public string Firstname { get => firstname; set => SetProperty(ref firstname, value, () => Validate()); }

        private string lastname;

        public string Lastname { get => lastname; set => SetProperty(ref lastname, value, () => Validate()); }

        private string email;

        public string Email { get => email; set => SetProperty(ref email, value, () => Validate()); }

        private string password;
        public string Password { get => password; set => SetProperty<string>(ref password, value, () => Validate()); }

        private string passwordConfirm;
        public string PasswordConfirm { get => passwordConfirm; set => SetProperty<string>(ref passwordConfirm, value, () => Validate()); }

        public SignupViewModel() : base() {
            SignupCommand = new RelayCommand(
                SignupAction,
                () => { return firstname != null && lastname != null && email != null && password != null && !HasErrors; }
            );
            LoginReturnCommand = new RelayCommand(
                LoginReturnAction
            );
        }

        private void SignupAction() {
            if (Validate()) {
                Student student = new Student(firstname, lastname, Email, password);
                // TODO: question: what's the best place (model?)
                Context.Students.Add(student);
                Context.SaveChanges();
                var user = (User)student;
                Login(user);
                OnSignupSuccess?.Invoke();
            }
        }

        private void LoginReturnAction() {
            OnLoginReturn?.Invoke();
        }

        public override bool Validate() {
            ClearErrors();

            var user = User.GetByEmail(Email);
            //var user = Context.Users.Find(Email);

            if (string.IsNullOrEmpty(Email))
                AddError(nameof(Email), Resources.Error_Required);
            else if (Email.Length < 3)
                AddError(nameof(Email), Resources.Error_LengthGreaterEqual3);
            else if (user != null)
                AddError(nameof(Email), Resources.Error_EmailAlreadyInUse);
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
