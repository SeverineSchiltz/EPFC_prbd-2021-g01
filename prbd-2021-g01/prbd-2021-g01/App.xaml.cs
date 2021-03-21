using System;
using System.Linq;
using System.Windows;
using PRBD_Framework;

namespace prbd_2021_g01
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : ApplicationBase
    {
        public static Model Context { get => Context<Model>(); }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
            Context.SeedData();

        }

        protected override void OnRefreshData()
        {
            // pour plus tard
        }

    }
}
