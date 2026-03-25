namespace CasautoAPI.Models
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}