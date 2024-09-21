using Microsoft.Extensions.Hosting;
using Razorblue.IPFiltering.Services;

namespace Razorblue.IPFiltering;

public class Worker(IpFilteringService ipFilteringService) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Task 2 - IP Filtering");

        while (true)
        {
            Console.WriteLine("------------------------\n");
            Console.Write("Please provide input: ");
            var input = Console.ReadLine();

            try
            {
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
    }

    public async Task StopAsync(CancellationToken cancellationToken) =>
        await Task.CompletedTask;
}
