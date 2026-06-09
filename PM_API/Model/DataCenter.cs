namespace Challenge_PM.Models
{
    public class DataCenter
    {
        public int Id { get; private set; }

        public string Setor { get; private set; }

        public string StatusDatacenter { get; private set; }

        protected DataCenter() { }

        public DataCenter(string setor, string statusDatacenter)
        {
            Setor = setor;
            StatusDatacenter = statusDatacenter;
        }

        public void Update(string setor, string statusDatacenter)
        {
            Setor = setor;
            StatusDatacenter = statusDatacenter;
        }
    }
}
