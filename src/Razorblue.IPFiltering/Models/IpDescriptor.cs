namespace Razorblue.IPFiltering.Models;

public class IpDescriptor
{
    // These should always be set
    public required Type Type { get; init; }
    public required string StartIp { get; init; }
    
    // Can be optional based on context
    // EndIp will be null in case of CIDR/Single IP
    // Cidr will be null in case of IP Range/Single IP
    public string? EndIp { get; init; }
    public string? Cidr { get; init; }
}

public enum Type
{
    Cidr,
    Range,
    Single
}
