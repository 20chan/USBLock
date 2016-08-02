using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USB.Config
{
    [Serializable]
    public class USBDeviceInfo
    {
        public string SerialNumber { get; set; }
        public string Name { get; set; }
        public DateTime LastUsedDate { get; set; }
    }
}
