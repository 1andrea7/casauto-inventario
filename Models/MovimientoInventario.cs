namespace CasautoAPI.Models
{
    public class MovimientoInventario
    {
        public int IdMovimiento { get; set; }
        public string TipoMovimiento { get; set; } = string.Empty;
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal? PrecioUnitarioMovimiento { get; set; }
        public string? Motivo { get; set; }
        public string? ReferenciaDocumento { get; set; }
        public string? Observaciones { get; set; }
        public int StockAntes { get; set; }
        public int StockDespues { get; set; }
        public int IdUsuario { get; set; }
        public DateTime? FechaMovimiento { get; set; }
        public Producto? Producto { get; set; }
    }
}