using Razorblue.DataImport.Models;
using Razorblue.DataImport.Utilities;

namespace Razorblue.DataImport.Services;

public class DataProcessorService(IFileHelper fileHelper) : IDataProcessorService
{
    public IEnumerable<Vehicle> GenerateDataAndRetrieveRecords(
        string? inputFile, string? outputPath)
    {
        if (string.IsNullOrWhiteSpace(inputFile))
            throw new ArgumentException("Input file should be provided");
        
        if (string.IsNullOrWhiteSpace(outputPath))
            throw new ArgumentException("Output path should be provided");

        // Load all records
        // Removing any duplicates using the vehicle
        // registration as the primary key
        var records = fileHelper
            .GetRecords(inputFile)
            .DistinctBy(vehicle => vehicle.Registration)
            .ToList();
        
        // By filtering on the fuel type
        var recordGroups = records
            .GroupBy(vehicle => vehicle.Fuel);

        // Create exported CSV(s), one for each fuel type of the vehicles
        var fileIdentifier = $"_{DateTime.UtcNow:s}.csv";
        foreach (var fuelGroup in recordGroups)
        {
            var fileName = $"{fuelGroup.Key}{fileIdentifier}";
            fileHelper.WriteRecords(Path.Combine(outputPath, fileName), fuelGroup);
        }

        // Return records
        // for any display requirements
        return records;
    }
}
