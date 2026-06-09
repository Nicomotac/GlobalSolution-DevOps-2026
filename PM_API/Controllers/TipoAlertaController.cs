using Challenge_PM.Data;
using Challenge_PM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoAlertaController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public TipoAlertaController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tiposAlerta = await dbContext.TipoAlertas.ToListAsync();
            return Ok(tiposAlerta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tipoAlerta = await dbContext.TipoAlertas.FindAsync(id);

            if (tipoAlerta == null)
                return NotFound();

            return Ok(tipoAlerta);
        }

        [HttpGet("nivel/{nivel}")]
        public async Task<IActionResult> GetByNivel(string nivel)
        {
            var tiposAlerta = await dbContext.TipoAlertas
                .Where(t => t.Nivel_alerta == nivel)
                .ToListAsync();

            if (tiposAlerta == null)
                return NotFound();

            return Ok(tiposAlerta);
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<IActionResult> GetByTipo(string tipo)
        {
            var tiposAlerta = await dbContext.TipoAlertas
                .Where(t => t.Tipo == tipo)
                .ToListAsync();

            if (tiposAlerta == null)
                return NotFound();

            return Ok(tiposAlerta);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TipoAlerta tipoAlertaToSave)
        {
            var tipoAlerta = new TipoAlerta(
                tipoAlertaToSave.Tipo,
                tipoAlertaToSave.Nivel_alerta,
                tipoAlertaToSave.Descricao
            );
            dbContext.TipoAlertas.Add(tipoAlerta);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetById),
                new { id = tipoAlerta.Id },
                tipoAlerta
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TipoAlerta novoTipoAlerta)
        {
            var tipoAlerta = await dbContext.TipoAlertas.FindAsync(id);
            if (tipoAlerta == null)
                return NotFound();

            tipoAlerta.Update(
                novoTipoAlerta.Tipo,
                novoTipoAlerta.Nivel_alerta,
                novoTipoAlerta.Descricao
            );
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tipoAlerta = await dbContext.TipoAlertas.FindAsync(id);
            if (tipoAlerta == null)
                return NotFound();

            dbContext.TipoAlertas.Remove(tipoAlerta);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
