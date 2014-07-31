using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace BinbinIpLocation
{
    public static class IpLocation
    {
        public class IpLocationResult
        {
            public IpLocationResult()
            {
            }
            public string IP { get; set; }
            public string Province { get; set; }
            public string City { get; set; }
            public string Region { get; set; }
        }

        public static IpLocationResult GetLocation(string ip, IpLocationProviders provider = IpLocationProviders.pconline)
        {
            switch (provider)
            {
                case IpLocationProviders.pconline:
                    return IpLocationPconline.GetLocation(ip);
                    break;
                default:
                    throw new NotSupportedException(provider.ToString());
            }
        }
    }
}
