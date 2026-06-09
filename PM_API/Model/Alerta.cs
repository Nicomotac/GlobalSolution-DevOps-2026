namespace Challenge_PM.Models
{
    public class Alerta
    {
        public int Id { get; private set; }

        public DateTime DataAlerta { get; private set; }

        public int Tipo_Id { get; private set; }

        public int Sensor_Id { get; private set; }

        public TipoAlerta? TipoAlerta { get; private set; }

        public Sensor? Sensor { get; private set; }

        protected Alerta() { }

        public Alerta(DateTime dataAlerta, int tipo_Id, int sensor_Id)
        {
            DataAlerta = dataAlerta;
            Tipo_Id = tipo_Id;
            Sensor_Id = sensor_Id;

        }

        public void Update(DateTime dataAlerta, int tipo_Id, int sensor_Id)
        {
            DataAlerta = dataAlerta;
            Tipo_Id = tipo_Id;
            Sensor_Id = sensor_Id;

        }
    }
}
