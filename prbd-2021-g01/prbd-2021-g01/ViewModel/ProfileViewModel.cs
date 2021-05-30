using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using prbd_2021_g01.Model;
using prbd_2021_g01.Properties;
using PRBD_Framework;

namespace prbd_2021_g01.ViewModel {
    public class ProfileViewModel : ViewModelCommon {

        public User User { get => CurrentUser; }

        private string firstname;
        public string Firstname { get => firstname; set => SetProperty<string>(ref firstname, value); }

        private string lastname;
        public string Lastname { get => lastname; set => SetProperty<string>(ref lastname, value); }

        private string email;

        public string Email { get => email; set => SetProperty(ref email, value, () => Validate()); }

        private string password;

        public string Password { get => password; set => SetProperty<string>(ref password, value, () => Validate()); }
        
        private string passwordConfirm;

        public string PasswordConfirm { get => passwordConfirm; set => SetProperty<string>(ref passwordConfirm, value, () => Validate()); }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ProfileViewModel() : base() {
            firstname = User.Firstname;
            lastname = User.Lastname;
            email = User.Email;
            password = User.Password;
            SaveCommand = new RelayCommand(
                SaveAction,
                () => { return email != null && password != null && passwordConfirm != null && !HasErrors; });
            CancelCommand = new RelayCommand(CancelAction);

        }
        // add a comment for password confirm
        public override bool Validate() {
            ClearErrors();

            if (string.IsNullOrEmpty(Email))
                AddError(nameof(Email), Resources.Error_Required);
            else if (Email.Length < 3)
                AddError(nameof(Email), Resources.Error_LengthGreaterEqual3);
            else {
                if (string.IsNullOrEmpty(Password))
                    AddError(nameof(Password), Resources.Error_Required);
                else if (Password.Length < 3)
                    AddError(nameof(Password), Resources.Error_LengthGreaterEqual3);
                else {
                    if (string.IsNullOrEmpty(PasswordConfirm))
                        AddError(nameof(PasswordConfirm), Resources.Error_Required);
                    else if (Password.Length < 3)
                        AddError(nameof(PasswordConfirm), Resources.Error_LengthGreaterEqual3);
                }
            }
            
            RaiseErrors();
            return !HasErrors;
        }

        private void SaveAction() {
            if (Validate()) {
                User.Email = Email;
                User.Password = Password;
                Context.SaveChanges();
            }
            //NotifyColleagues(AppMessages.MSG_PROFILE_UPDATED, User);
        }

        private void CancelAction() {
            Email = User.Email;
            Password = User.Password;
            PasswordConfirm = User.Password;
            //Context.Reload(CurrentUser); // not working - reload with object to load data from db
            RaisePropertyChanged(); // notify the view that all bound properties need to be refreshed 
        }

        protected override void OnRefreshData() {
           
        }
    }

}
