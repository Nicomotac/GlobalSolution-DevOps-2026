using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge_PM.Models
{
    public class Sensor
    {
        public int Id { get; private set; }

        public string TipoSensor { get; private set; }

        public string UnidadeMedida { get; private set; }

        public string AtividadeSensor { get; private set; }

        public int DataCenter_Id { get; private set; }

        public DataCenter? DataCenter { get; private set; }

        protected Sensor() { }

        public Sensor(string tipoSensor, string unidadeMedida, string atividadeSensor, int dataCenter_Id)
        {
            TipoSensor = tipoSensor;
            UnidadeMedida = unidadeMedida;
            AtividadeSensor = atividadeSensor;
            DataCenter_Id = dataCenter_Id;

        }

        public void Update(string tipoSensor, string unidadeMedida, string atividadeSensor, int dataCenter_Id)
        {
            TipoSensor = tipoSensor;
            UnidadeMedida = unidadeMedida;
            AtividadeSensor = atividadeSensor;
            DataCenter_Id = dataCenter_Id;

        }
    }
}

