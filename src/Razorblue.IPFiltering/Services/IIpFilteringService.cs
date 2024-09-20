namespace Razorblue.IPFiltering.Services;

public interface IIpFilteringService
{
    bool IsIpAvailable(string? ipAddress);
}
