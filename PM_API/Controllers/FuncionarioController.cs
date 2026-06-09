using Challenge_PM.Data;
using Challenge_PM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge_PM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public FuncionarioController(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var funcionarios = await dbContext.Funcionarios.ToListAsync();
            return Ok(funcionarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var funcionario = await dbContext.Funcionarios.FindAsync(id);

            if (funcionario == null)
                return NotFound();

            return Ok(funcionario);
        }

        [HttpGet("cargo/{cargo}")]
        public async Task<IActionResult> GetByCargo(string cargo)
        {
            var funcionarios = await dbContext.Funcionarios
                .Where(f => f.CargoFuncionario == cargo)
                .ToListAsync();

            if (funcionarios == null)
                return NotFound();

            return Ok(funcionarios);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var funcionario = await dbContext.Funcionarios
                .FirstOrDefaultAsync(f => f.Email == email);

            if (funcionario == null)
                return NotFound();

            return Ok(funcionario);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Funcionario funcionarioToSave)
        {
            var funcionario = new Funcionario(
                funcionarioToSave.Nome,
                funcionarioToSave.Email,
                funcionarioToSave.Telefone,
                funcionarioToSave.CargoFuncionario
            );
            dbContext.Funcionarios.Add(funcionario);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetById),
                new { id = funcionario.Id },
                funcionario
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Funcionario novoFuncionario)
        {
            var funcionario = await dbContext.Funcionarios.FindAsync(id);
            if (funcionario == null)
                return NotFound();

            funcionario.Update(
                novoFuncionario.Nome,
                novoFuncionario.Email,
                novoFuncionario.Telefone,
                novoFuncionario.CargoFuncionario
            );
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var funcionario = await dbContext.Funcionarios.FindAsync(id);
            if (funcionario == null)
                return NotFound();

            dbContext.Funcionarios.Remove(funcionario);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
