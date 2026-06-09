using Challenge_PM.Data;
using Challenge_PM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManutencaoController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public ManutencaoController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var manutencoes = await dbContext.Manutencoes
                .Include(m => m.Funcionario)
                .Include(m => m.Alerta)
                .ThenInclude(a => a.TipoAlerta)
                .ToListAsync();
            return Ok(manutencoes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var manutencao = await dbContext.Manutencoes
                .Include(m => m.Funcionario)
                .Include(m => m.Alerta)
                .ThenInclude(a => a.TipoAlerta)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manutencao == null)
                return NotFound();

            return Ok(manutencao);
        }

        [HttpGet("funcionario/{funcionarioId}")]
        public async Task<IActionResult> GetByFuncionario(int funcionarioId)
        {
            var manutencoes = await dbContext.Manutencoes
                .Include(m => m.Funcionario)
                .Include(m => m.Alerta)
                .ThenInclude(a => a.TipoAlerta)
                .Where(m => m.Funcionario_Id == funcionarioId)
                .ToListAsync();

            if (manutencoes == null)
                return NotFound();

            return Ok(manutencoes);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var manutencoes = await dbContext.Manutencoes
                .Include(m => m.Funcionario)
                .Include(m => m.Alerta)
                .ThenInclude(a => a.TipoAlerta)
                .Where(m => m.StatusManutencao == status)
                .ToListAsync();

            if (manutencoes == null)
                return NotFound();

            return Ok(manutencoes);
        }

        [HttpGet("tipo/{tipo}")]
        public async Task<IActionResult> GetByTipo(string tipo)
        {
            var manutencoes = await dbContext.Manutencoes
                .Include(m => m.Funcionario)
                .Include(m => m.Alerta)
                .ThenInclude(a => a.TipoAlerta)
                .Where(m => m.TipoManutencao == tipo)
                .ToListAsync();

            if (manutencoes == null)
                return NotFound();

            return Ok(manutencoes);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Manutencao manutencaoToSave)
        {
            var manutencao = new Manutencao(
                manutencaoToSave.DataManutencao,
                manutencaoToSave.TipoManutencao,
                manutencaoToSave.StatusManutencao,
                manutencaoToSave.Funcionario_Id,
                manutencaoToSave.Alerta_Id
            );
            dbContext.Manutencoes.Add(manutencao);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetById),
                new { id = manutencao.Id },
                manutencao
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Manutencao novaManutencao)
        {
            var manutencao = await dbContext.Manutencoes.FindAsync(id);
            if (manutencao == null)
                return NotFound();

            manutencao.Update(
                novaManutencao.DataManutencao,
                novaManutencao.TipoManutencao,
                novaManutencao.StatusManutencao,
                novaManutencao.Funcionario_Id,
                novaManutencao.Alerta_Id
            );
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var manutencao = await dbContext.Manutencoes.FindAsync(id);
            if (manutencao == null)
                return NotFound();

            dbContext.Manutencoes.Remove(manutencao);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
