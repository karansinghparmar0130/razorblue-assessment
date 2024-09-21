using Razorblue.DataImport.Models;

namespace Razorblue.DataImport.Services;

public interface IDataProcessorService
{
    /// <summary>
    /// Generates files based criteria retrieves processed records
    /// </summary>
    /// <param name="inputFile">File to load & process</param>
    /// <param name="outputPath">Output path to generate files</param>
    /// <returns>Returns list of processed records</returns>
    IEnumerable<Vehicle> GenerateDataAndRetrieveRecords(string? inputFile, string? outputPath);
}
