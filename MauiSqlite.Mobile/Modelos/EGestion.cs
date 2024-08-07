using System.ComponentModel.DataAnnotations;

namespace MauiSqlite.Mobile.Modelos
{
    public class EGestion
    {
        [Key]
        public int Idges { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public ICollection<EAfiliado>? Afiliados { get; set; }
    }
}
