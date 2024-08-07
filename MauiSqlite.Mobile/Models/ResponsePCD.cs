using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSqlite.Mobile.Models
{
    public class ResponsePCD
    {
        public int Idpersodisca { get; set; }
        public int IdUsuario { get; set; }
        public int Idasoci { get; set; }
        public int Idtipodisca { get; set; }
        public string? Ciperso { get; set; }
        public string? Codcarnetdisca { get; set; }
        public string? Porsentaje { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Sexo { get; set; }
        public string? Foto { get; set; }
        public bool EstadoBono { get; set; }
        public bool Estado { get; set; }
        public string? DescripcionAso { get; set; }
        public string? DescripcionTipo { get; set; }
        public string? NombresTu { get; set; }
        public string? Ciapoderado { get; set; }
        public string? Parentesco { get; set; }
        public string? Celular { get; set; }

        public string PictureFullPath => string.IsNullOrEmpty(Foto)
            ? "https://umapedis-001-site1.ftempurl.com/Imagenes/Sinfotop.jpg"
            : $"https://umapedis-001-site1.ftempurl.com{Foto}";

        public string FullName => $"{Nombres} {Apellidos}";
    }
}
