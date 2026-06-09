using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge_PM.Models
{
    public class Manutencao
    {
        public int Id { get; private set; }

        public DateTime DataManutencao { get; private set; }

        public string TipoManutencao { get; private set; }

        public string StatusManutencao { get; private set; }

        public int Funcionario_Id { get; private set; }

        public int Alerta_Id { get; private set; }
        
        public Funcionario? Funcionario { get; private set; }

        public Alerta? Alerta { get; private set; }
        protected Manutencao() { }

        public Manutencao(DateTime dataManutencao, string tipoManutencao, string statusManutencao, int funcionario_Id, int alerta_Id)
        {
            DataManutencao = dataManutencao;
            TipoManutencao = tipoManutencao;
            StatusManutencao= statusManutencao;
            Funcionario_Id = funcionario_Id;
            Alerta_Id = alerta_Id;

        }

        public void Update(DateTime dataManutencao, string tipoManutencao, string statusManutencao, int funcionario_Id, int alerta_Id)
        {
            DataManutencao = dataManutencao;
            TipoManutencao = tipoManutencao;
            StatusManutencao = statusManutencao;
            Funcionario_Id = funcionario_Id;
            Alerta_Id = alerta_Id;

        }
    }
}

