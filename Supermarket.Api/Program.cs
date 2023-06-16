using Supermarket.Api.Data;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

builder.Services.AddControllers();
//Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Agregar la configuraci�n al servicio de la aplicaci�n.
var configuration = builder.Configuration;

//Obtener la cadena de conexi�n de la configuraci�n.
var connectionString = configuration.GetConnectionString("MyDatabase");

//Agrega la instancia de DataBaseContext al servicio de la aplicaci�n.
var databaseContext = new DataBaseContext(connectionString);
builder.Services.AddSingleton(databaseContext);

//Registra la implementaci�n concreta de IProductsRepository
//para su inyecci�n de dependencias en el contenedor de servicios.
builder.Services.AddScoped<IProductsRepository, ProductsRepository>();

//Registra la implementaci�n concreta de IPurchasesRepository
//para su inyecci�n de dependencias en el contenedor de servicios.
builder.Services.AddScoped<IPurchasesRepository, PurchasesRepository>();

//Registra la implementaci�n concreta de IReportsRepository
//para su inyecci�n de dependencias en el contenedor de servicios.
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();

var app = builder.Build();

//Crea la tabla Productos al iniciar la aplicaci�n.
databaseContext.CreateTableProducts();

//Crea la tabla Compras al iniciar la aplicaci�n.
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
