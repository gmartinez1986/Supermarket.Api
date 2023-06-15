using Supermarket.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agregar la configuraci�n al servicio de la aplicaci�n
var configuration = builder.Configuration;

// Obtener la cadena de conexi�n de la configuraci�n
var connectionString = configuration.GetConnectionString("MyDatabase");

// Agrega la instancia de DataContext al servicio de la aplicaci�n
var databaseContext = new DataBaseContext(connectionString);
builder.Services.AddSingleton(databaseContext);

builder.Services.AddScoped<IRepository, Repository>();

var app = builder.Build();

// Crea la tabla al iniciar la aplicaci�n
databaseContext.CreateTableProducts();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
