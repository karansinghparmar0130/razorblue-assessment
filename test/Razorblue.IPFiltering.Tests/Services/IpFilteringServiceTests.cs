using Moq;
using Razorblue.IPFiltering.Models;
using Razorblue.IPFiltering.Repositories;
using Razorblue.IPFiltering.Services;
using Type = Razorblue.IPFiltering.Models.Type;

namespace Razorblue.IPFiltering.Tests.Services;

public class IpFilteringServiceTests
{
    private readonly Mock<IIpRepository> _ipRepositoryMock = new();
    private readonly IpFilteringService _sut;

    public IpFilteringServiceTests() =>
        _sut = new IpFilteringService(_ipRepositoryMock.Object);

    [Fact]
    public void Should_return_false_When_database_returned_empty()
    {
        // Arrange
        _ipRepositoryMock.Setup(x => x.GetIpDescriptors())
            .Returns([]); // return empty response

        // Act
        var actual = _sut.IsIpAvailable("1.1.1.1");

        // Assert
        Assert.False(actual);
    }

    [Theory]
    [InlineData("1-1", "IpAddress: 1-1 not valid")] // Invalid
    [InlineData("@", "IpAddress: @ not valid")]
    [InlineData("  ", "IpAddress should be provided")] // Whitespace
    [InlineData("", "IpAddress should be provided")] // Empty
    [InlineData(null, "IpAddress should be provided")] // Null
    public void Should_throw_exception_When_input_ip_is_invalid(string? inputIp, string exceptionMessage)
    {
        // Act & Assert
        var actual = Assert.Throws<ArgumentException>(() => _sut.IsIpAvailable(inputIp));
        Assert.Equal(expected: exceptionMessage, actual.Message);
    }

    [Theory]
    [InlineData("1.1.0.0", false)]
    [InlineData("1.1.1.254", true)]
    [InlineData("3.3.3.3", true)]
    [InlineData("2.2.2.5", true)]
    public void Should_return_true_or_false_for_valid_ip_When_database_returned_stored_ip_list(string inputIp,
        bool isAllowed)
    {
        // Arrange
        _ipRepositoryMock.Setup(x => x.GetIpDescriptors())
            .Returns(GetAllowedList());

        // Act
        var actual = _sut.IsIpAvailable(inputIp);

        // Assert
        Assert.Equal(isAllowed, actual);
    }

    private static IEnumerable<IpDescriptor> GetAllowedList()
    {
        return new[]
        {
            new IpDescriptor
            {
                Type = Type.Cidr,
                StartIp = "1.1.1.0",
                Cidr = "1.1.1.0/24"
            },
            new IpDescriptor
            {
                Type = Type.Range,
                StartIp = "2.2.2.2",
                EndIp = "2.2.2.10"
            },
            new IpDescriptor
            {
                Type = Type.Single,
                StartIp = "3.3.3.3"
            }
        };
    }
}
