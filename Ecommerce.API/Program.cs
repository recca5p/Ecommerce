using Domain.RepositoriyInterfaces;
using Ecommerce.API.Middlewares;
using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repositories;
using Services;
using Services.Abstraction;
using Services.Profiles;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

String envInUsed = configuration.GetSection("ASPNETCORE_ENVIRONMENT").Value 
                   ?? throw new InvalidOperationException("Can not get ASPNETCORE_ENVIRONMENT value");

WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    EnvironmentName = envInUsed
});

// Add services to the container.

IConfigurationSection appConfigSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appConfigSection);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetSection("AppSettings:DatabaseConnectionString").Value 
                          ?? throw new InvalidOperationException("Can not get DatabaseConnectionString");

builder.Services.AddScoped<IRepositoryDbContext>(x =>
    new RepositoryDbContext(
        new DbContextOptionsBuilder<RepositoryDbContext>()
            .UseNpgsql(connectionString)
            .Options));

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddAutoMapper(typeof(ModelProfile));
builder.Services.AddScoped<IServiceManager, ServiceManager>();
    
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();