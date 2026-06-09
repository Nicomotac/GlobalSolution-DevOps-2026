using Challenge_PM.Data;
using Challenge_PM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataCenterController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public DataCenterController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dataCenters = await dbContext.DataCenters.ToListAsync();
            return Ok(dataCenters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dataCenter = await dbContext.DataCenters.FindAsync(id);

            if (dataCenter == null)
                return NotFound();

            return Ok(dataCenter);
        }

        [HttpGet("setor/{setor}")]
        public async Task<IActionResult> GetBySetor(string setor)
        {
            var dataCenters = await dbContext.DataCenters
                .Where(d => d.Setor == setor)
                .ToListAsync();

            if (dataCenters == null)
                return NotFound();

            return Ok(dataCenters);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var dataCenters = await dbContext.DataCenters
                .Where(d => d.StatusDatacenter == status)
                .ToListAsync();

            if (dataCenters == null)
                return NotFound();

            return Ok(dataCenters);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DataCenter dataCenterToSave)
        {
            var dataCenter = new DataCenter(dataCenterToSave.Setor, dataCenterToSave.StatusDatacenter);
            dbContext.DataCenters.Add(dataCenter);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetById),
                new { id = dataCenter.Id },
                dataCenter
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DataCenter novoDataCenter)
        {
            var dataCenter = await dbContext.DataCenters.FindAsync(id);
            if (dataCenter == null)
                return NotFound();

            dataCenter.Update(novoDataCenter.Setor, novoDataCenter.StatusDatacenter);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dataCenter = await dbContext.DataCenters.FindAsync(id);
            if (dataCenter == null)
                return NotFound();

            dbContext.DataCenters.Remove(dataCenter);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
