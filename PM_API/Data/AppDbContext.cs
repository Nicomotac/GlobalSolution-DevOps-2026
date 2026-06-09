using Challenge_PM.Models;
using Microsoft.EntityFrameworkCore;

namespace Challenge_PM.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Alerta> Alertas { get; set; }

        public DbSet<DataCenter> DataCenters { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }

        public DbSet<Manutencao> Manutencoes { get; set; }

        public DbSet<Sensor> Sensores { get; set; }

        public DbSet<TipoAlerta> TipoAlertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tabelas
            modelBuilder.Entity<DataCenter>().ToTable("datacenter");
            modelBuilder.Entity<Sensor>().ToTable("sensor");
            modelBuilder.Entity<TipoAlerta>().ToTable("tipo_alerta");
            modelBuilder.Entity<Alerta>().ToTable("alerta");
            modelBuilder.Entity<Funcionario>().ToTable("funcionario");
            modelBuilder.Entity<Manutencao>().ToTable("manutencao");

            // Relacionamentos (1:N)
            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.DataCenter)
                .WithMany()
                .HasForeignKey(s => s.DataCenter_Id);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.TipoAlerta)
                .WithMany()
                .HasForeignKey(a => a.Tipo_Id);

            modelBuilder.Entity<Alerta>()
                .HasOne(a => a.Sensor)
                .WithMany()
                .HasForeignKey(a => a.Sensor_Id);

            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.Funcionario)
                .WithMany()
                .HasForeignKey(m => m.Funcionario_Id);

            modelBuilder.Entity<Manutencao>()
                .HasOne(m => m.Alerta)
                .WithMany()
                .HasForeignKey(m => m.Alerta_Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
