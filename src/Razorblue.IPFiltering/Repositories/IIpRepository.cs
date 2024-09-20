using Razorblue.IPFiltering.Models;

namespace Razorblue.IPFiltering.Repositories;

public interface IIpRepository
{
    IEnumerable<IpDescriptor> GetIpDescriptors();
}
