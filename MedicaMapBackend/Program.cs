using MedicaMap.APIServices;
using MedicaMap.Data;
using MedicaMap.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<MedicaMapContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddScoped<MunicipalityService>();
builder.Services.AddScoped<EstablishmentService>();
builder.Services.AddScoped<MedicationService>();
builder.Services.AddScoped<StockService>();
builder.Services.AddScoped<StateService>();
builder.Services.AddScoped<BnafarImportService>();


builder.Services.AddHttpClient<IBGEAPIService>(client =>
{
    client.BaseAddress = new Uri("https://servicodados.ibge.gov.br/");
});

builder.Services.AddHttpClient<CnesApiService>(client =>
{
    client.BaseAddress = new Uri("https://apidadosabertos.saude.gov.br/v1/");
});

builder.Services.AddHttpClient<BnafarApiService>(client =>
{
    client.BaseAddress = new Uri("https://apidadosabertos.saude.gov.br/");
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();