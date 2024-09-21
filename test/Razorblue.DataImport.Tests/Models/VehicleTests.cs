using Razorblue.DataImport.Models;

namespace Razorblue.DataImport.Tests.Models;

public class VehicleTests
{
    [Theory] 
    [InlineData("OO13 KSP", true)]
    [InlineData("0013 KSP", false)]
    [InlineData("oo13KSP", false)]
    [InlineData("@", false)]
    public void Should_set_vehicle_is_valid_When_registration_is_provided(string registration, bool isValid)
    {
        // Arrange & Act
        var expected = new Vehicle
        {
            Registration = registration,
            Make = "some make",
            Model = "some model",
            Colour = "some colour",
            Fuel = "some fuel"
        };
        
        // Assert
        Assert.Equal(isValid, expected.IsValid);
    }
}
