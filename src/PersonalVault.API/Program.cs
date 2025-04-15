using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PersonalVault.Infrastructure.Persistence;
using PersonalVault.Infrastructure.Repositories;
using PersonalVault.Application.Common.Interfaces;
using PersonalVault.Application.Notes.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using PersonalVault.Application.Mapping;
using PersonalVault.Application.Notes.Commands;
using PersonalVault.Application.Notes.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register EF Core DbContext using the connection string from configuration.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repository and application service
builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

// Register FluentValidation (automatically scans for validators)
builder.Services.AddValidatorsFromAssembly(typeof(NoteCreateValidator).Assembly);
builder.Services.AddFluentValidationAutoValidation();

// Register AutoMapper with our mapping profiles
builder.Services.AddAutoMapper(typeof(NoteMappingProfile));

// Register MediatR (this scans the assembly containing our commands and handlers)
builder.Services.AddMediatR(typeof(CreateNoteCommand).Assembly);

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware.
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "My Personal Vault API V1");
    s.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();