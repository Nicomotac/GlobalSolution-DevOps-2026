using Challenge_PM.Data;
using Challenge_PM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public AlertaController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alertas = await dbContext.Alertas
                .Include(a => a.TipoAlerta)
                .Include(a => a.Sensor)
                .ThenInclude(s => s.DataCenter)
                .ToListAsync();
            return Ok(alertas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var alerta = await dbContext.Alertas
                .Include(a => a.TipoAlerta)
                .Include(a => a.Sensor)
                .ThenInclude(s => s.DataCenter)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alerta == null)
                return NotFound();

            return Ok(alerta);
        }

        [HttpGet("sensor/{sensorId}")]
        public async Task<IActionResult> GetBySensor(int sensorId)
        {
            var alertas = await dbContext.Alertas
                .Include(a => a.TipoAlerta)
                .Include(a => a.Sensor)
                .ThenInclude(s => s.DataCenter)
                .Where(a => a.Sensor_Id == sensorId)
                .ToListAsync();

            if (alertas == null)
                return NotFound();

            return Ok(alertas);
        }

        [HttpGet("tipo/{tipoId}")]
        public async Task<IActionResult> GetByTipo(int tipoId)
        {
            var alertas = await dbContext.Alertas
                .Include(a => a.TipoAlerta)
                .Include(a => a.Sensor)
                .ThenInclude(s => s.DataCenter)
                .Where(a => a.Tipo_Id == tipoId)
                .ToListAsync();

            if (alertas == null)
                return NotFound();

            return Ok(alertas);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Alerta alertaToSave)
        {
            var alerta = new Alerta(alertaToSave.DataAlerta, alertaToSave.Tipo_Id, alertaToSave.Sensor_Id);
            dbContext.Alertas.Add(alerta);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetById),
                new { id = alerta.Id },
                alerta
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Alerta novoAlerta)
        {
            var alerta = await dbContext.Alertas.FindAsync(id);
            if (alerta == null)
                return NotFound();

            alerta.Update(novoAlerta.DataAlerta, novoAlerta.Tipo_Id, novoAlerta.Sensor_Id);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var alerta = await dbContext.Alertas.FindAsync(id);
            if (alerta == null)
                return NotFound();

            dbContext.Alertas.Remove(alerta);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
