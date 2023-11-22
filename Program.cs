using Microsoft.EntityFrameworkCore;
using RepharmTaskBackend;
using FluentValidation.AspNetCore;
using RepharmTaskBackend.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePatientValidator>());

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BackendContext>(options =>
    options.UseInMemoryDatabase("DoctorAppDB"));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000") 
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<BackendContext>();
        context.Database.EnsureCreated(); // This will create the database and apply seeding
    }
    catch (Exception ex)
    {
        // Log or handle the exception if something goes wrong
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
