using System.ComponentModel.DataAnnotations;

namespace MauiSqlite.Mobile.Modelos
{
    public class EAfiliado
    {
        [Key]
        public int IdAfiliado { get; set; }
        public string? NroCI { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Celular { get; set; }
        public bool Estado { get; set; }
        public int EGestionId { get; set; }
        public EGestion? EGestion { get; set; }
    }
}
