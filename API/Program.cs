using API.Controllers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Microsoft.EntityFrameworkCore;

// Create a new web application builder using the given command-line arguments.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add controllers to the dependency injection container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI services to the dependency injection container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(ProductsController<>));
//builder.Services.AddScoped(typeof(ProductsController<>), typeof(IGenericRepository<>));

// Build the web application.
var app = builder.Build();

// Configure the HTTP request pipeline.
// If the application is in development mode, enable Swagger and Swagger UI.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

// Use HTTPS redirection.
app.UseHttpsRedirection();

// Use authentication/authorization middleware.
app.UseAuthorization();

// Map the controllers to the appropriate endpoints.
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
}
catch (Exception ex)
{
    logger.LogError(ex, "Migration error!");
}

// Start the web application.
app.Run();
