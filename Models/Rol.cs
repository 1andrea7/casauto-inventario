namespace CasautoAPI.Models
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Permisos { get; set; }
    }
}