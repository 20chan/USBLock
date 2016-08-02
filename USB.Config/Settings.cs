using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USB.Config
{
    public class Settings
    {
        public bool IsStartUp { get; set; }
        public double FormOpacity { get; set; }
        public List<USBDeviceInfo> AcceptedSerials { get; set; }
    }
}
