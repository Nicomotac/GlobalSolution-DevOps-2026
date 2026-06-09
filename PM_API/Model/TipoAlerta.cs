namespace Challenge_PM.Models
{
    public class TipoAlerta
    {
        public int Id { get; private set; }

        public string Tipo { get; private set; }

        public string Nivel_alerta { get; private set; }

        public string Descricao { get; private set; }

        protected TipoAlerta() { }

        public TipoAlerta(string tipo, string nivel_alerta, string descricao)
        {
            Tipo = tipo;
            Nivel_alerta = nivel_alerta;
            Descricao = descricao;

        }

        public void Update(string tipo, string nivel_alerta, string descricao)
        {
            Tipo = tipo;
            Nivel_alerta = nivel_alerta;
            Descricao = descricao;

        }
    }
}