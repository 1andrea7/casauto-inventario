namespace CasautoAPI.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string CodigoProducto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public int IdCategoria { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal? PrecioCompra { get; set; }
        public int StockActual { get; set; }
        public int? StockMinimo { get; set; }
        public string? UnidadMedida { get; set; }
        public string? UbicacionBodega { get; set; }
        public string? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaUltimaModificacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
    }
}