using Challenge_PM.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// A connection string vem da variavel de ambiente ConnectionStrings__DefaultConnection
// (definida no docker-compose). Cai para o appsettings.json caso nao exista.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cria o schema do banco (todas as tabelas) automaticamente ao subir o container.
// Usa EnsureCreated para nao depender de migrations no ambiente Docker.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Swagger habilitado em qualquer ambiente para facilitar a demonstracao.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
