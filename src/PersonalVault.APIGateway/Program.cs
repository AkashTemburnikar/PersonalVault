using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Tell Ocelot where to find the configuration file
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot to the DI container
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Use Ocelot middleware. This will automatically read your ocelot.json configuration.
await app.UseOcelot();

app.Run();