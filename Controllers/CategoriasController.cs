using CasautoAPI.Data;
using CasautoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasautoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly CasautoContext _context;

        public CategoriasController(CasautoContext context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            categoria.FechaCreacion = DateTime.Now;
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.IdCategoria }, categoria);
        }

        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria) return BadRequest();
            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return NotFound();
            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}