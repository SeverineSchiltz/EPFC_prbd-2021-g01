using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prbd_2021_g01.Model;
using PRBD_Framework;

namespace prbd_2021_g01.ViewModel {
    public class ProfileViewModel : ViewModelCommon {

        /*private User user;
        public User User { get => user; set => SetProperty(ref user, value); }*/

        //public string Firstname { get; set; }
        //public string Lastname { get; set; }
        /*public string Email { get; set; }
        public string Password { get; set; }*/

        //private string firstname;
        public string Firstname {
            get { return CurrentUser.Firstname; }
            set { // => SetProperty(ref firstname, value);
                CurrentUser.Firstname = value;
                RaisePropertyChanged(nameof(Firstname));
            }
        }

        //private string lastname;
        public string Lastname {
            get { return CurrentUser.Lastname; }
            set { // => SetProperty(ref lastname, value);
                CurrentUser.Lastname = value;
                RaisePropertyChanged(nameof(Lastname));
            }
        }

        public string Email {
            get { return CurrentUser.Email; }
            set { // => SetProperty(ref email, value);
                CurrentUser.Email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        public string Password {
            get { return CurrentUser.Password; }
            set { // => SetProperty(ref password, value);
                CurrentUser.Password = value;
                RaisePropertyChanged(nameof(Password));
            }
        }

        protected override void OnRefreshData() {
           
        }
    }

}
