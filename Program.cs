using GeoPet.Data;
using GeoPet.Interfaces;
using GeoPet.Services;
using GeoPet.Authorization;
using GeoPet.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

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
    services.AddSwaggerGen();
}

var app = builder.Build();

// Migrate database
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<GeoPetContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// configure request pipeline
{
    // cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // authentication handler
    app.UseMiddleware<JwtMiddleware>();

    // redirect to https
    app.UseHttpsRedirection();

    // map routes
    app.MapControllers();
}

app.Run();

