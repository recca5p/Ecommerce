using Contract.Models;
using Domain.RepositoriyInterfaces;
using Ecommerce.API.Middlewares;
using Ecommerce.API.SecurityRules;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
var secretKey = builder.Configuration["AppSettings:PasetoSettings:SecretKey"];

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });

    // 🔹 Add JWT Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}'"
    });

    // 🔹 Apply JWT to all API requests
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
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

builder.Services.AddAuthentication("CustomScheme")
    .AddScheme<AuthenticationSchemeOptions, CustomAuthHandler>("CustomScheme", options => { });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireClaim("isAdmin", "True");
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "isAdmin" && c.Value == "True"));
    });
});


TokenExtension.Configure(secretKey);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth API v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();