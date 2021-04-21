using PRBD_Framework;
using prbd_2021_g01.Model;

namespace prbd_2021_g01.ViewModel
{
    public abstract class ViewModelCommon : ViewModelBase<EcoleContext> {

        public ViewModelCommon() : base() {
        }

        //public static bool IsTeacher {
        //    get => App.IsLoggedIn && App.CurrentUser.DiscriminatorValue == "Teacher";
        //}

        public static User CurrentUser {
            get => App.CurrentUser;
        }

        public static void Login(User user) {
            App.Login(user);
        }

        public static void Logout() {
            App.Logout();
        }

        public static bool IsLoggedIn { get => App.IsLoggedIn; }
    }
}
