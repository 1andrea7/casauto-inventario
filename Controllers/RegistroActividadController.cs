using CasautoAPI.Data;
using CasautoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasautoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroActividadController : ControllerBase
    {
        private readonly CasautoContext _context;

        public RegistroActividadController(CasautoContext context)
        {
            _context = context;
        }

        // GET: api/RegistroActividad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroActividad>>> GetRegistros()
        {
            return await _context.RegistrosActividad
                .Include(r => r.Usuario)
                .OrderByDescending(r => r.FechaAccion)
                .Take(200)
                .ToListAsync();
        }

        // POST: api/RegistroActividad
        [HttpPost]
        public async Task<ActionResult<RegistroActividad>> PostRegistro(RegistroActividad registro)
        {
            registro.FechaAccion = DateTime.Now;
            _context.RegistrosActividad.Add(registro);
            await _context.SaveChangesAsync();
            return Ok(registro);
        }
    }
}