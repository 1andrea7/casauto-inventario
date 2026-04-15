namespace CasautoAPI.Models
{
    public class RegistroActividad
    {
        public int IdRegistro { get; set; }
        public string TablaAfectada { get; set; } = string.Empty;
        public int IdRegistroAfectado { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string? ValoresAnteriores { get; set; }
        public string? ValoresNuevos { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaAccion { get; set; }
        public string? IpAddress { get; set; }
        public string? DescripcionCambio { get; set; }
        public Usuario? Usuario { get; set; }
    }
}