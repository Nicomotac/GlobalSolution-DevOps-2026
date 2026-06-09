using Challenge_PM.Data;
using Challenge_PM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public SensorController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sensores = await dbContext.Sensores
                .Include(s => s.DataCenter)
                .ToListAsync();
            return Ok(sensores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sensor = await dbContext.Sensores
                .Include(s => s.DataCenter)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sensor == null)
                return NotFound();

            return Ok(sensor);
        }

        [HttpGet("datacenter/{dataCenterId}")]
        public async Task<IActionResult> GetByDataCenter(int dataCenterId)
        {
            var sensores = await dbContext.Sensores
                .Include(s => s.DataCenter)
                .Where(s => s.DataCenter_Id == dataCenterId)
                .ToListAsync();

            if (sensores == null)
                return NotFound();

            return Ok(sensores);
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<IActionResult> GetByTipo(string tipo)
        {
            var sensores = await dbContext.Sensores
                .Include(s => s.DataCenter)
                .Where(s => s.TipoSensor == tipo)
                .ToListAsync();

            if (sensores == null)
                return NotFound();

            return Ok(sensores);
        }

        [HttpGet("atividade/{atividade}")]
        public async Task<IActionResult> GetByAtividade(string atividade)
        {
            var sensores = await dbContext.Sensores
                .Include(s => s.DataCenter)
                .Where(s => s.AtividadeSensor == atividade)
                .ToListAsync();

            if (sensores == null)
                return NotFound();

            return Ok(sensores);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Sensor sensorToSave)
        {
            var sensor = new Sensor(
                sensorToSave.TipoSensor,
                sensorToSave.UnidadeMedida,
                sensorToSave.AtividadeSensor,
                sensorToSave.DataCenter_Id
            );
            dbContext.Sensores.Add(sensor);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetById),
                new { id = sensor.Id },
                sensor
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Sensor novoSensor)
        {
            var sensor = await dbContext.Sensores.FindAsync(id);
            if (sensor == null)
                return NotFound();

            sensor.Update(
                novoSensor.TipoSensor,
                novoSensor.UnidadeMedida,
                novoSensor.AtividadeSensor,
                novoSensor.DataCenter_Id
            );
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sensor = await dbContext.Sensores.FindAsync(id);
            if (sensor == null)
                return NotFound();

            dbContext.Sensores.Remove(sensor);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
