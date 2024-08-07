using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiSqlite.Mobile.DTOs
{
    public partial class EGestionDTO : ObservableObject
    {
        [ObservableProperty]
        public int idges;

        [ObservableProperty]
        public string? descripcion;

        [ObservableProperty]
        public bool estado;
    }
}
