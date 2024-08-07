using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiSqlite.Mobile.Models;
using Newtonsoft.Json;

namespace MauiSqlite.Mobile.ViewModels
{
    public partial class FlyoutHeaderControlModel : ObservableObject
    {
        [ObservableProperty]
        private string? email;

        [ObservableProperty]
        private string? nombre;

        [ObservableProperty]
        private string? foto;

        public FlyoutHeaderControlModel()
        {
            LoadPersonalAsync();
        }
        private async void LoadPersonalAsync()
        {
            await InicioAsync();
        }
        private async Task InicioAsync()
        {
            var use = await SecureStorage.Default.GetAsync(SettingsConst.Userl);
            if (!string.IsNullOrEmpty(use))
            {
                ResponsePCD? responsePCD = JsonConvert.DeserializeObject<ResponsePCD>(use);
                Nombre = responsePCD?.Nombres;
                Foto = responsePCD?.PictureFullPath;
                Email = responsePCD?.Apellidos;
            }
        }
    }
}
