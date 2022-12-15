using GeoPet.Data;
using GeoPet.Interfaces;
using GeoPet.Services;
using GeoPet.Authorization;
using GeoPet.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the Dependency Injection container.
{
    var services = builder.Services;

    services.AddCors();
    services.AddControllers();
    services.AddHttpContextAccessor();

    // Configure DbContext
    services.AddDbContext<GeoPetContext>();

    // Configure auto mapper with all automapper profiles
    services.AddAutoMapper(typeof(Program));

    // Configure strongly typed settings objects
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // Setup DI
    services.AddScoped<IPetService, PetService>();
    services.AddScoped<IPetCarerService, PetCarerService>();
    services.AddScoped<ISearchService, SearchService>();
    services.AddScoped<IJwtUtils, JwtUtils>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    });
}

var app = builder.Build();

// Migrate database
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<GeoPetContext>();
    context.Database.Migrate();
}


// configure request pipeline
// cors policy
app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

// error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

// authentication handler
app.UseMiddleware<JwtMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
// map routes
app.MapControllers();

app.Run();

