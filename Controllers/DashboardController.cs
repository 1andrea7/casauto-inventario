using CasautoAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasautoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly CasautoContext _context;

        public DashboardController(CasautoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var hoy = DateTime.Today;

            var totalProductos = await _context.Productos
                .Where(p => p.Estado == "activo").CountAsync();

            var entradasHoy = await _context.MovimientosInventario
                .Where(m => m.TipoMovimiento == "entrada" && m.FechaMovimiento >= hoy)
                .SumAsync(m => (int?)m.Cantidad) ?? 0;

            var stockCritico = await _context.Productos
                .Where(p => p.Estado == "activo" && p.StockActual < p.StockMinimo)
                .CountAsync();

            var valorInventario = await _context.Productos
                .Where(p => p.Estado == "activo")
                .SumAsync(p => (decimal?)p.PrecioUnitario * p.StockActual) ?? 0;

            var ultimosMovimientos = await _context.MovimientosInventario
                .Include(m => m.Producto)
                .OrderByDescending(m => m.FechaMovimiento)
                .Take(5)
                .ToListAsync();

            var alertas = await _context.Productos
                .Where(p => p.Estado == "activo" && p.StockActual < p.StockMinimo)
                .OrderBy(p => p.StockActual)
                .Take(5)
                .ToListAsync();

            return Ok(new
            {
                totalProductos,
                entradasHoy,
                stockCritico,
                valorInventario,
                ultimosMovimientos = ultimosMovimientos.Select(m => new {
                    m.IdMovimiento,
                    m.TipoMovimiento,
                    m.Cantidad,
                    m.Motivo,
                    m.FechaMovimiento,
                    m.StockAntes,
                    m.StockDespues,
                    producto = m.Producto == null ? null : new
                    {
                        m.Producto.IdProducto,
                        m.Producto.Nombre
                    }
                }),
                alertas = alertas.Select(p => new {
                    p.IdProducto,
                    p.Nombre,
                    p.StockActual,
                    p.StockMinimo
                })
            });
        }

        [HttpGet("alertas")]
        public async Task<IActionResult> GetAlertas()
        {
            var criticos = await _context.Productos
                .Where(p => p.Estado == "activo" && p.StockActual == 0)
                .OrderBy(p => p.Nombre)
                .ToListAsync();

            var advertencias = await _context.Productos
                .Where(p => p.Estado == "activo" && p.StockActual > 0 && p.StockActual < p.StockMinimo)
                .OrderBy(p => p.StockActual)
                .ToListAsync();

            return Ok(new { criticos, advertencias });
        }
    }
}