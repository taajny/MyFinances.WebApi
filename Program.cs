using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyFinances.WebApi.Models;
using MyFinances.WebApi.Models.Domains;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
//IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
// Add services to the container.
builder.Services.AddScoped<UnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<MyFinancesContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("MyFinancesContext")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPatch = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPatch);
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
