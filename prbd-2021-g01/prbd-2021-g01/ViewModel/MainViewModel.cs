using PRBD_Framework;
using prbd_2021_g01.Model;
using System.Windows.Input;
using System;

namespace prbd_2021_g01.ViewModel
{
    public class MainViewModel : ViewModelCommon
    {
        public event Action OnLogout; 
        public ICommand LogoutCommand { get; set; }

        public MainViewModel() : base()
        {
            LogoutCommand = new RelayCommand(LogoutAction);
        }

        protected override void OnRefreshData()
        {
            // pour plus tard
        }

        private void LogoutAction() { 
            Logout(); 
            OnLogout?.Invoke(); 
        }
        public string Title { 
            get => $"AppSchool ({CurrentUser.Firstname} {CurrentUser.Lastname})"; 
        }
    }
}
