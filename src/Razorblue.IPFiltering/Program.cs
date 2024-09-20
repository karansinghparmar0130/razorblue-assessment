using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razorblue.IPFiltering;
using Razorblue.IPFiltering.Repositories;
using Razorblue.IPFiltering.Services;

// Create
var builder = Host.CreateApplicationBuilder(args);

// Configure
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<IpFilteringService, IpFilteringService>();

// This dependency (LocalIpRepository) can be swapped with actual Database implementation
// when integration with DB is required
// Lifetime can be changed accordingly
builder.Services.AddSingleton<IIpRepository, LocalIpRepository>();

// Build
var app = builder.Build();

// Run
app.Run();
