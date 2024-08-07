using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiSqlite.Mobile.DTOs;
using MauiSqlite.Mobile.Models;
using Newtonsoft.Json;
using MauiSqlite.Mobile.Repositories;
using MauiSqlite.Mobile.Views;

namespace MauiSqlite.Mobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IRepository _repository;

        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? password;

        [ObservableProperty]
        private bool loadingEsVisible = false;

        public LoginViewModel(IRepository repository)
        {
            _repository = repository;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrEmpty(Email))
            {
                await Shell.Current.DisplayAlert("Error", "Ingrese un Usuario", "Ok");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Shell.Current.DisplayAlert("Error", "Ingrese una Contraseña", "Ok");
                return;
            }

            LoadingEsVisible = true;

            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                LoadingEsVisible = false;
                await Shell.Current.DisplayAlert("Error", "Verifique la conexion a Internet", "Ok");
                return;
            }

            string url = "https://umapedis-001-site1.ftempurl.com/";

            LoginDTO loginDTO = new LoginDTO
            {
                Ciperso = Email,
                Codcarnetdisca = Password
            };

            var httpResponse = await _repository.GetPersoN<ResponsePCD>(url, "api/pagobonos/Login", loginDTO);

            if (httpResponse.Error)
            {
                LoadingEsVisible = false;
                var message = await httpResponse.GetErrorMessageAsync();
                await Shell.Current.DisplayAlert("Error", message, "Ok");
                return;
            }

            await SecureStorage.Default.SetAsync(SettingsConst.Logi, "si");
            ResponsePCD responsePCD = httpResponse.Response!;
            string userDetail = JsonConvert.SerializeObject(responsePCD);
            await SecureStorage.Default.SetAsync(SettingsConst.Userl, userDetail);

            LoadingEsVisible = false;

            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
            await Shell.Current.GoToAsync($"//{nameof(InicioView)}");


            //if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            //{
            //    LoadingEsVisible = true;
            //    string url = "https://umapedis-001-site1.ftempurl.com/";

            //    LoginDTO loginDTO = new LoginDTO
            //    {
            //        Ciperso = Email,
            //        Codcarnetdisca = Password
            //    };

            //    var httpResponse = await _repository.GetPersoN<ResponsePCD>(url, "api/pagobonos/Login", loginDTO);

            //    if (httpResponse.Error)
            //    {
            //        LoadingEsVisible = false;
            //        var message = await httpResponse.GetErrorMessageAsync();
            //        await Shell.Current.DisplayAlert("Error", message, "Ok");
            //        return;
            //    }

            //    await SecureStorage.Default.SetAsync(SettingsConst.Logi, "si");
            //    ResponsePCD responsePCD = httpResponse.Response!;
            //    string userDetail = JsonConvert.SerializeObject(responsePCD);
            //    await SecureStorage.Default.SetAsync(SettingsConst.Userl, userDetail);

            //    LoadingEsVisible = false;

            //    AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
            //    await Shell.Current.GoToAsync($"//{nameof(InicioView)}");
            //}
            //else
            //{
            //    await Shell.Current.DisplayAlert("Error", "Ingrese Usua y Clave", "Ok");
            //    return;
            //}
        }
    }
}
