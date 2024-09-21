using Razorblue.DataImport.Models;

namespace Razorblue.DataImport.Utilities;

public interface IFileHelper
{
    IEnumerable<Vehicle> GetRecords(string file);
    void WriteRecords(string file, IEnumerable<Vehicle> records);
}
