using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Razorblue.DataImport.Services;
using Razorblue.DataImport.Utilities;

// Create
var builder = Host.CreateApplicationBuilder(args);

// Configure
builder.Services.AddTransient<IFileHelper, FileHelper>();
builder.Services.AddTransient<IDataProcessorService, DataProcessorService>();

// Build
var app = builder.Build();

// Run
Console.WriteLine("Task 3 - Data Import");

while (true)
{
    Console.WriteLine("------------------------\n");
    Console.Write("Please provide input file: ");
    var input = Console.ReadLine();

    Console.Write("Please provide output path: ");
    var outputPath = Console.ReadLine();

    try
    {
        var dataProcessorService = app.Services.GetRequiredService<IDataProcessorService>();
        var processedRecords = dataProcessorService
            .GenerateDataAndRetrieveRecords(input, outputPath)
            .ToList();

        if (processedRecords.Count is 0)
            Console.WriteLine("No records processed");
        else
        {
            // Header
            Console.WriteLine("Car Registration,Make,Model,Colour,Fuel");
            var invalidCount = 0;
            foreach (var record in processedRecords)
            {
                if (record.IsValid)
                    Console.WriteLine(
                        $"{record.Registration},{record.Make}{record.Model},{record.Colour},{record.Fuel}");
                else
                    invalidCount++;
            }

            // Print count of invalid registrations
            Console.WriteLine($"Number of records with Invalid Registration: {invalidCount}\n");
        }
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
