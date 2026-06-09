using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge_PM.Models
{
    public class Funcionario
    {
        public int Id { get; private set; }

        public string Nome { get; private set; }

        public string Email { get; private set; }

        public string Telefone { get; private set; }

        public string CargoFuncionario { get; private set; }

        protected Funcionario() { }

        public Funcionario(string nome, string email, string telefone, string cargoFuncionario)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            CargoFuncionario = cargoFuncionario;
        }

        public void Update(string nome, string email, string telefone, string cargoFuncionario)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            CargoFuncionario = cargoFuncionario;

        }
    }
}
