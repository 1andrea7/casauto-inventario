using CasautoAPI.Data;
using CasautoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasautoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly CasautoContext _context;

        public MovimientosController(CasautoContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>> GetMovimientos()
        {
            return await _context.MovimientosInventario
                .Include(m => m.Producto)
                .OrderByDescending(m => m.FechaMovimiento)
                .ToListAsync();
        }

        // POST: api/Movimientos
        [HttpPost]
        public async Task<ActionResult<MovimientoInventario>> PostMovimiento(MovimientoInventario movimiento)
        {
            // Obtener producto actual
            var producto = await _context.Productos.FindAsync(movimiento.IdProducto);
            if (producto == null) return NotFound("Producto no encontrado");

            // Calcular stock antes y después
            movimiento.StockAntes = producto.StockActual;

            if (movimiento.TipoMovimiento == "entrada")
                producto.StockActual += movimiento.Cantidad;
            else
                producto.StockActual -= movimiento.Cantidad;

            movimiento.StockDespues = producto.StockActual;
            movimiento.FechaMovimiento = DateTime.Now;

            // Guardar movimiento y actualizar producto
            _context.MovimientosInventario.Add(movimiento);
            producto.FechaUltimaModificacion = DateTime.Now;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovimientos), new { id = movimiento.IdMovimiento }, movimiento);
        }
    }
}