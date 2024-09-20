using Razorblue.IPFiltering.Models;
using Type = Razorblue.IPFiltering.Models.Type;

namespace Razorblue.IPFiltering.Repositories;

public class LocalIpRepository: IIpRepository
{
    public IEnumerable<IpDescriptor> GetIpDescriptors()
    {
        return new[]
        {
            // Single IPs
            new IpDescriptor
            {
                Type = Type.Single,
                StartIp = "1.1.1.1"
            },
            new IpDescriptor
            {
                Type = Type.Single,
                StartIp = "2.2.2.2"
            },
            // IP Ranges
            new IpDescriptor
            {
                Type = Type.Range,
                StartIp = "3.0.0.0",
                EndIp = "3.0.1.0"
            },
            new IpDescriptor
            {
                Type = Type.Range,
                StartIp = "4.0.0.0",
                EndIp = "4.0.1.0"
            },
            // CIDRs
            new IpDescriptor
            {
                Type = Type.Cidr,
                StartIp = "9.0.0.0",
                Cidr = "9.0.0.0/24"
            },
            new IpDescriptor
            {
                Type = Type.Cidr,
                StartIp = "11.0.0.0",
                Cidr = "11.0.0.0/24"
            }
        };
    }
}
