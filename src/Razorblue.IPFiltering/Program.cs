using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razorblue.IPFiltering.Repositories;
using Razorblue.IPFiltering.Services;

// Create
var builder = Host.CreateApplicationBuilder(args);

// Configure
builder.Services.AddSingleton<IpFilteringService, IpFilteringService>();

// This dependency (LocalIpRepository) can be swapped with actual Database implementation
// when integration with DB is required
// Lifetime can be changed accordingly
builder.Services.AddSingleton<IIpRepository, LocalIpRepository>();

// Build
var app = builder.Build();

// Run
Console.WriteLine("Task 2 - IP Filtering");

while (true)
{
    Console.WriteLine("------------------------\n");
    Console.Write("Please provide input: ");
    var input = Console.ReadLine();

    try
    {
        var ipFilteringService = app.Services.GetRequiredService<IpFilteringService>();
        var isIpAvailable = ipFilteringService.IsIpAvailable(input);
        Console.WriteLine($"Is IP available in list? {isIpAvailable}\n");
    }
    catch (Exception ex)
    {
        // This should ideally be abstracted to prevent internal info leakage
        // Done only in this context
        Console.WriteLine($"Error: {ex.Message}\n");
    }
    finally
    {
        Console.WriteLine("Press Enter to continue or Ctrl-C to exit\n");
        Console.ReadKey(intercept: true);
    }
}
