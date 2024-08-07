using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiSqlite.Mobile.DTOs
{
    public partial class EAfiliadoDTO : ObservableObject
    {
        [ObservableProperty]
        public int idAfiliado;
        [ObservableProperty]
        public string? nroCI;
        [ObservableProperty]
        public string? nombres;
        [ObservableProperty]
        public string? apellidos;
        [ObservableProperty]
        public string? direccion;
        [ObservableProperty]
        public string? celular;
        [ObservableProperty]
        public bool estado;
        [ObservableProperty]
        public int eGestionId;
        [ObservableProperty]
        private string? eGestionDescripcion;
    }
}
