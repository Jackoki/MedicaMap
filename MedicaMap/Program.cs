using MedicaMap.Data;
using MedicaMap.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MedicaMapContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));

builder.Services.AddScoped<MunicipalityService>();
builder.Services.AddScoped<EstablishmentService>();
builder.Services.AddScoped<MedicationService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<StateService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();