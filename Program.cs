using API_Buenas_practicas.Data;
using API_Buenas_practicas.Repositories;
using API_Buenas_practicas.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connectionStrings = builder.Configuration.GetConnectionString("ConnDBString");
builder.Services.AddDbContext<MyAppDbContext>(options=>options.UseSqlServer(connectionStrings));
builder.Services.AddScoped<ITareaRepository, TareaRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    var archivoXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var rutaArchivo = Path.Combine(AppContext.BaseDirectory, archivoXML);
    config.IncludeXmlComments(rutaArchivo);
});

var app = builder.Build();

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
