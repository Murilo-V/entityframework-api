using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIMySql.Data;
using APIMySql.Models;

namespace APIMySql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly APIDbContext _context;

        public EstadoController(APIDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstado()
        {
          if (_context.Estado == null)
          {
              return NotFound();
          }
            return await _context.Estado.ToListAsync();
        }

        [HttpGet("{uf}")]
        public async Task<ActionResult<Estado>> GetEstado(string uf)
        {
          if (_context.Estado == null)
          {
              return NotFound();
          }
            var estado = await _context.Estado.FindAsync(uf);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        [HttpPut("{uf}")]
        public async Task<IActionResult> PutEstado(string uf, Estado estado)
        {
            if (uf != estado.UF)
            {
                return BadRequest();
            }

            _context.Entry(estado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoExists(uf))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Estado>> PostEstado(Estado estado)
        {
          if (_context.Estado == null)
          {
              return Problem("Entity set 'APIDbContext.Estado'  is null.");
          }
            _context.Estado.Add(estado);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EstadoExists(estado.UF))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEstado", new { id = estado.UF }, estado);
        }

        [HttpDelete("{uf}")]
        public async Task<IActionResult> DeleteEstado(string uf)
        {
            if (_context.Estado == null)
            {
                return NotFound();
            }
            var estado = await _context.Estado.FindAsync(uf);
            if (estado == null)
            {
                return NotFound();
            }

            _context.Estado.Remove(estado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadoExists(string uf)
        {
            return (_context.Estado?.Any(e => e.UF == uf)).GetValueOrDefault();
        }
    }
}
