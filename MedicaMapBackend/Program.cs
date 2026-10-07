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


builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue;
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
});


var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("Angular");
app.MapControllers();
app.Run();