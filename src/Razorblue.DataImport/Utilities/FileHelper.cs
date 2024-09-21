using System.Globalization;
using CsvHelper.Configuration;
using Razorblue.DataImport.Models;

namespace Razorblue.DataImport.Utilities;

public class FileHelper : IFileHelper
{
    public IEnumerable<Vehicle> GetRecords(string file)
    {
        using var reader = new StreamReader(file);
        using var csv = new CsvHelper.CsvReader(reader, Configuration());
        csv.Context.RegisterClassMap<ReaderMap>();

        // Stream records
        foreach (var record in csv.GetRecords<Vehicle>())
            yield return record;
    }

    public void WriteRecords(string file, IEnumerable<Vehicle> records)
    {
        using var writer = new StreamWriter(file);
        using var csv = new CsvHelper.CsvWriter(writer, Configuration());
        csv.Context.RegisterClassMap<WriterMap>();
        csv.WriteRecords(records);
    }

    // Read & Write configuration
    // Can be split if needed
    private static CsvConfiguration Configuration()
    {
        return new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, Delimiter = "," };
    }
}

public sealed class ReaderMap : ClassMap<Vehicle>
{
    public ReaderMap()
    {
        // Map columns of csv, if header name is different
        Map(m => m.Registration).Name("Car Registration");
        Map(m => m.Make);
        Map(m => m.Model);
        Map(m => m.Colour);
        Map(m => m.Fuel);
    }
}

public sealed class WriterMap : ClassMap<Vehicle>
{
    public WriterMap()
    {
        // Map columns of csv, if header name is different
        // Remove fuel as it is implicit
        Map(m => m.Registration).Name("Car Registration");
        Map(m => m.Make);
        Map(m => m.Model);
        Map(m => m.Colour);
    }
}
