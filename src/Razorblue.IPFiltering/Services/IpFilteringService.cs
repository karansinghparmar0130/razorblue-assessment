using System.Net;
using Razorblue.IPFiltering.Repositories;
using Type = Razorblue.IPFiltering.Models.Type;

namespace Razorblue.IPFiltering.Services;

public class IpFilteringService(IIpRepository ipRepository) : IIpFilteringService
{
    // This assumes that data loaded from IIpRepository is valid IPs
    public bool IsIpAvailable(string? ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            throw new ArgumentException("IpAddress should be provided");

        if (!IPAddress.TryParse(ipAddress, out var validatedIp))
            throw new ArgumentException($"IpAddress: {ipAddress} not valid");

        // Get IpDescriptors and group them based on types
        var ipGroups = ipRepository
            .GetIpDescriptors()
            .GroupBy(ipDescriptor => ipDescriptor.Type)
            .ToDictionary(group => group.Key, group => group.ToList());

        // Check if present in single IPs
        if (ipGroups.TryGetValue(Type.Single, out var singleIps)
            && singleIps.Any(ipDescriptor => IsIpInSingleIp(validatedIp, ipDescriptor.StartIp)))
            return true;

        // Check if present in IP Range
        if (ipGroups.TryGetValue(Type.Range, out var ipRangeList)
            && ipRangeList.Any(ipDescriptor => IsIpInRange(validatedIp, ipDescriptor.StartIp, ipDescriptor.EndIp!)))
            return true;

        // Check if present in CIDR
        if (ipGroups.TryGetValue(Type.Cidr, out var cidrList)
            && cidrList.Any(ipDescriptor => IsIpInCidr(validatedIp, ipDescriptor.Cidr!)))
            return true;

        return false;
    }

    #region Logic from Google for Ip Identification

    private static bool IsIpInSingleIp(IPAddress ipAddress, string singleIp)
    {
        var ip = IPAddress.Parse(singleIp);
        return ipAddress.Equals(ip);
    }
    
    private static bool IsIpInCidr(IPAddress ipAddress, string cidr)
    {
        var parts = cidr.Split('/');
        var baseIp = IPAddress.Parse(parts[0]);
        var prefixLength = int.Parse(parts[1]);

        var mask = IpAddressToLongMask(prefixLength);
        return (IpAddressToLong(ipAddress) & mask) == (IpAddressToLong(baseIp) & mask);
    }

    private static bool IsIpInRange(IPAddress ipAddress, string startIp, string endIp)
    {
        var start = IpAddressToLong(IPAddress.Parse(startIp));
        var end = IpAddressToLong(IPAddress.Parse(endIp));

        var ipLong = IpAddressToLong(ipAddress);
        return ipLong >= start && ipLong <= end;
    }

    private static long IpAddressToLong(IPAddress ipAddress)
    {
        var bytes = ipAddress.GetAddressBytes();
        
        // Convert to little-endian for comparison
        Array.Reverse(bytes);
        return BitConverter.ToUInt32(bytes, 0);
    }

    private static long IpAddressToLongMask(int prefixLength)
    {
        return uint.MaxValue << (32 - prefixLength);
    }
    
    #endregion
}
