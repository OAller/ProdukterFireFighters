using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection; 
//måske skal slettes 
var builder = WebApplication.CreateBuilder(args);

// Hent connection string fra appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Tilføj services til containeren
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.SchemaName = "dbo";
    options.TableName = "SessionCache";
});

// Session cookies
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15); // Sessionen udløber efter 30 min
    options.Cookie.HttpOnly = true; // Gør cookien utilgængelig for JavaScript
    options.Cookie.IsEssential = true; // Sørg for, at cookien altid er aktiv
});

// Registrer ProdukterRepo med connection string som parameter
builder.Services.AddSingleton(new ProdukterRepo());

// Konfigurer CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Konfigurer middleware-pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseSession(); // Tilføj session middleware

app.UseAuthorization();

app.MapControllers();

app.Run();
