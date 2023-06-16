using Supermarket.Api.Data;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddControllers();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregar la configuración al servicio de la aplicación.
var configuration = builder.Configuration;

//Obtener la cadena de conexión de la configuración.
var connectionString = configuration.GetConnectionString("MyDatabase");

//Agrega la instancia de DataBaseContext al servicio de la aplicación.
var databaseContext = new DataBaseContext(connectionString);
builder.Services.AddSingleton(databaseContext);

//Registra la implementación concreta de IProductsRepository
//para su inyección de dependencias en el contenedor de servicios.
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

//Registra la implementación concreta de IPurchasesRepository
//para su inyección de dependencias en el contenedor de servicios.
builder.Services.AddScoped<IPurchasesRepository, PurchasesRepository>();

//Registra la implementación concreta de IReportsRepository
//para su inyección de dependencias en el contenedor de servicios.
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();

var app = builder.Build();

//Crea la tabla Productos al iniciar la aplicación.
databaseContext.CreateTableProducts();

//Crea la tabla Compras al iniciar la aplicación.
databaseContext.CreateTablePurchases();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
