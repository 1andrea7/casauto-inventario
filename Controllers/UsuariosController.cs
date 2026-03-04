using CasautoAPI.Data;
using CasautoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasautoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly CasautoContext _context;

        public UsuariosController(CasautoContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
            if (usuario == null) return NotFound();
            return usuario;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            usuario.FechaCreacion = DateTime.Now;
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario) return BadRequest();
            usuario.FechaUltimaModificacion = DateTime.Now;
            _context.Entry(usuario).State = EntityState.Modified;
            _context.Entry(usuario).Property(u => u.FechaCreacion).IsModified = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound();
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Usuarios/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Email == request.Email
                                       && u.ContrasenaHash == request.Contrasena);

            if (usuario == null)
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });

            return Ok(new
            {
                idUsuario = usuario.IdUsuario,
                nombre = usuario.Nombre,
                apellido = usuario.Apellido,
                email = usuario.Email,
                rol = usuario.Rol?.NombreRol,
                estado = usuario.Estado
            });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}