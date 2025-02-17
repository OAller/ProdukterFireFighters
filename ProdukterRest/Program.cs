var builder = WebApplication.CreateBuilder(args);

// Hent connection string fra appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Tilføj services til containeren
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
