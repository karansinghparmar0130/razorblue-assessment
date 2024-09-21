using System.Text.RegularExpressions;

namespace Razorblue.DataImport.Models;

public partial class Vehicle
{
    public required string Registration { get; init; }
    public required string Make { get; init; }
    public required string Model { get; init; }
    public required string Colour { get; init; }
    public required string Fuel { get; init; }
    
    // Set based on allowed registration style 
    public bool IsValid =>
        AllowedRegistrations()
            .IsMatch(Registration);

    // Allowed Pattern
    // Two letters followed by two numbers, followed by a space and then three letters
    [GeneratedRegex("^[A-Za-z]{2}[0-9]{2} [A-Za-z]{3}$")]
    private static partial Regex AllowedRegistrations();
}
