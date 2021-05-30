using System;
using System.Linq;
using System.Windows;
using PRBD_Framework;
using prbd_2021_g01.Model;
using prbd_2021_g01.Properties;
using System.Threading;
using System.Globalization;

namespace prbd_2021_g01
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        public static EcoleContext Context { get => Context<EcoleContext>(); }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Définit l'intervalle de temps (en secondes) pour le rafraîchissement des données
            //RefreshDelay = Settings.Default.RefreshDelay;

            //Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            //Context.SeedData();

        }

        protected override void OnRefreshData()
        {
            if (CurrentUser?.Email != null)
                CurrentUser = User.GetByEmail(CurrentUser.Email);
        }

        public static User CurrentUser { get; private set; }

        public static void Login(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        public static bool IsLoggedIn { get => CurrentUser != null; }


        public App()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Culture);
        }

    }
}
