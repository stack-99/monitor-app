using System;
using System.Collections.Generic;
using System.Text;

namespace MobApp.Models
{
    public class ServerInfo
    {
        public string AssignedID { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public int Count { get; set; }
        public bool Online { get; set; }

        public string Description
        {
            get
            {
                return string.Format("Name: {0} - Online: {1}", Name, Online);
            }
        }
    }

    public class ServerStatusResult
    {
        public List<ServerInfo> ServersInfo { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ServerHardwareInfo 
    {
        public bool SSAlive { get; set; }
        public bool LSAlive { get; set; }
        public bool GSAlive { get; set; }

        public ulong SSMemory { get; set; }
        public ulong LSMemory { get; set; }
        public ulong GSMemory { get; set; }

        public ulong TotalUsed { get; set; }
        public ulong Free { get; set; }

        public string Region { get; set; }

    }
}
