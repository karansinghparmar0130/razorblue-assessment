using Moq;
using Razorblue.DataImport.Models;
using Razorblue.DataImport.Services;
using Razorblue.DataImport.Utilities;

namespace Razorblue.DataImport.Tests.Services;

public class DataProcessorServiceTests
{
    private readonly Mock<IFileHelper> _fileHelperMock = new();
    private readonly DataProcessorService _sut;

    public DataProcessorServiceTests() =>
        _sut = new DataProcessorService(_fileHelperMock.Object);

    [Theory]
    [InlineData("  ")] // Whitespace
    [InlineData("")] // Empty
    [InlineData(null)] // Null
    public void Should_throw_exception_When_input_file_is_invalid(string? inputFile)
    {
        // Act & Assert
        var actual = Assert.Throws<ArgumentException>(() =>
            _sut.GenerateDataAndRetrieveRecords(inputFile, "outputPath"));
        
        Assert.Equal(expected: "Input file should be provided", actual.Message);
        
        // In case of invalid inputs, FileHelper should not be accessed
        _fileHelperMock.Verify(x =>
                x.GetRecords(It.IsAny<string>()),
            Times.Never);
        _fileHelperMock.Verify(x =>
                x.WriteRecords(It.IsAny<string>(), It.IsAny<IEnumerable<Vehicle>>()),
            Times.Never);
    }

    [Theory]
    [InlineData("  ")] // Whitespace
    [InlineData("")] // Empty
    [InlineData(null)] // Null
    public void Should_throw_exception_When_output_path_is_invalid(string? outputPath)
    {
        // Act & Assert
        var actual = Assert.Throws<ArgumentException>(() =>
            _sut.GenerateDataAndRetrieveRecords("inputFile", outputPath));

        Assert.Equal(expected: "Output path should be provided", actual.Message);
        
        // In case of invalid inputs, FileHelper should not be accessed
        _fileHelperMock.Verify(x =>
                x.GetRecords(It.IsAny<string>()),
            Times.Never); 
        _fileHelperMock.Verify(x =>
                x.WriteRecords(It.IsAny<string>(), It.IsAny<IEnumerable<Vehicle>>()),
            Times.Never);
    }

    [Fact]
    public void Should_return_empty_list_When_file_helper_returned_empty()
    {
        // Arrange
        const string inputFile = "test-file";
        const string outputPath = "csv/something";
        
        _fileHelperMock.Setup(x => x.GetRecords(It.IsAny<string>()))
            .Returns([]); // return empty response

        // Act
        var actual = _sut.GenerateDataAndRetrieveRecords(inputFile, outputPath);

        // Assert
        Assert.Empty(actual);
        _fileHelperMock.Verify(x =>
                x.GetRecords(inputFile),
            Times.Once); // FileHelper GetRecords should be accessed once
        _fileHelperMock.Verify(x =>
                x.WriteRecords(It.IsAny<string>(), It.IsAny<IEnumerable<Vehicle>>()),
            Times.Never); // FileHelper WriteRecords should not be accessed
    }

    [Fact]
    public void Should_return_processed_list_When_file_helper_returned_records()
    {
        // Arrange
        const string inputFile = "test-file";
        const string outputPath = "csv/something";
        
        _fileHelperMock.Setup(x => x.GetRecords(It.IsAny<string>()))
            .Returns(Records()); // return response from file helper
        _fileHelperMock.Setup(x => x.WriteRecords(It.IsAny<string>(), It.IsAny<IEnumerable<Vehicle>>()));

        // Act
        var actual = _sut.GenerateDataAndRetrieveRecords(inputFile, outputPath);

        // Assert
        // Returns deduplicated (by registration) records
        Assert.Equivalent(ProcessedRecords(), actual, strict: true);
        _fileHelperMock.Verify(x =>
                x.GetRecords(inputFile),
            Times.Once); // FileHelper GetRecords should be accessed once
        _fileHelperMock.Verify(x =>
                x.WriteRecords(It.IsAny<string>(), It.IsAny<IEnumerable<Vehicle>>()),
            Times.Exactly(2)); // FileHelper WriteRecords should be accessed twice (once for each fuel group)
    }

    // Setup records with
    // 1 Duplicate record
    // 2 fuel groups
    private static IEnumerable<Vehicle> Records() =>
        new[]
        {
            DuplicateFuel1Record1,
            DuplicateFuel1Record1,
            Fuel1Record2,
            Fuel2Record1,
            Fuel2Record2
        };

    // Processed records
    // without duplicate record
    private static IEnumerable<Vehicle> ProcessedRecords() =>
        new[]
        {
            DuplicateFuel1Record1,
            Fuel1Record2,
            Fuel2Record1,
            Fuel2Record2
        };

    // Group 1
    private static IEnumerable<Vehicle> Fuel1Records() =>
        new[]
        {
            DuplicateFuel1Record1,
            Fuel1Record2
        };
    
    // Group 2
    private static IEnumerable<Vehicle> Fuel2Records() =>
        new[]
        {
            Fuel2Record1,
            Fuel2Record2
        };
    
    private static Vehicle DuplicateFuel1Record1 =>
        new()
        {
            Registration = "RG01 ABC",
            Make = "Make1",
            Model = "Model1",
            Colour = "Colour1",
            Fuel = "Fuel1"
        };
    
    private static Vehicle Fuel1Record2 =>
        new()
        {
            Registration = "RG02 ABC",
            Make = "Make1",
            Model = "Model1",
            Colour = "Colour1",
            Fuel = "Fuel1"
        };
    
    private static Vehicle Fuel2Record1 =>
        new()
        {
            Registration = "KS01 ABC",
            Make = "Make1",
            Model = "Model1",
            Colour = "Colour1",
            Fuel = "Fuel2"
        };
    
    private static Vehicle Fuel2Record2 =>
        new()
        {
            Registration = "KS02 ABC",
            Make = "Make1",
            Model = "Model1",
            Colour = "Colour1",
            Fuel = "Fuel2"
        };
}
