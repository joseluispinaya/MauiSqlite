using MauiSqlite.Mobile.ViewModels;
using MauiSqlite.Mobile.Views;

namespace MauiSqlite.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = new AppShellViewModel();
            Routing.RegisterRoute(nameof(InicioView), typeof(InicioView));
            Routing.RegisterRoute(nameof(AddAfiliadoView), typeof(AddAfiliadoView));
            //Routing.RegisterRoute(nameof(AfiliadoView), typeof(AfiliadoView));
        }
    }
}
