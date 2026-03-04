namespace CasautoAPI.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContrasenaHash { get; set; } = string.Empty;
        public string? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaUltimaModificacion { get; set; }
        public int IdRol { get; set; }
        public Rol? Rol { get; set; }
    }
}