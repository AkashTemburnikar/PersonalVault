using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonalVault.Infrastructure.Persistence;
using PersonalVault.Infrastructure.Repositories;
using PersonalVault.Application.Common.Interfaces;
using PersonalVault.Application.Notes.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using PersonalVault.API.Middleware;
using PersonalVault.Application.Mapping;
using PersonalVault.Application.Notes.Commands;
using PersonalVault.Application.Notes.Validators;
using Serilog;
using Microsoft.ApplicationInsights;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 🔹 Logging Configuration with Application Insights
// ==========================================

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

builder.Host.UseSerilog((context, services, configuration) =>
{
    var aiConnectionString = context.Configuration["ApplicationInsights:ConnectionString"];

    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.ApplicationInsights(
            services.GetRequiredService<TelemetryClient>(),
            TelemetryConverter.Traces);
});

// ==========================
// 🔧 Configure URLs
// ==========================
builder.WebHost.UseUrls("http://0.0.0.0:8080");

// ==========================
// 📡 Application Insights
// ==========================
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<TelemetryClient>();

// ==========================
// 🔹 Register Services
// ==========================
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddValidatorsFromAssembly(typeof(NoteCreateValidator).Assembly);
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddAutoMapper(typeof(NoteMappingProfile));
builder.Services.AddMediatR(typeof(CreateNoteCommand).Assembly);

// ==========================
// 🔐 Auth0 Configuration
// ==========================
const string domain = "https://dev-test-0510.us.auth0.com";
const string audience = "https://personalvault-api";

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = domain;
        options.Audience = audience;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidAudience = audience,
            ValidIssuer = domain
        };
    });

builder.Services.AddAuthorization();

// ==========================
// 📘 Swagger Setup with Auth
// ==========================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: **Bearer {your Auth0 access token}**"
    });

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
            Array.Empty<string>()
        }
    });
});

// ==========================
// 🧠 Rate Limiting
// ==========================
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// ==========================
// 🛠 Build the App
// ==========================
var app = builder.Build();

// ==========================
// 🔄 Middleware Pipeline
// ==========================
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "My Personal Vault API V1");
    s.RoutePrefix = string.Empty;
});

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();