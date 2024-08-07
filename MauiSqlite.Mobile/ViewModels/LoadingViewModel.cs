using MauiSqlite.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSqlite.Mobile.ViewModels
{
    public class LoadingViewModel
    {
        public LoadingViewModel()
        {
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            var sesi = await SecureStorage.Default.GetAsync(SettingsConst.Logi);
            if (string.IsNullOrEmpty(sesi))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginView)}");
            }
            else
            {
                AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
                await Shell.Current.GoToAsync($"//{nameof(InicioView)}");
            }
        }
    }
}
