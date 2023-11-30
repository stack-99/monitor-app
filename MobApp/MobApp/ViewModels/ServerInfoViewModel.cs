using MobApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MobApp.ViewModels
{
    public class ServerInfoViewModel
    {
        public ObservableCollection<ServerInfo> ServersInfo { get; set; }

        public ServerInfoViewModel()
        {
            ServersInfo = new ObservableCollection<ServerInfo>();

            ServersInfo.Add(new ServerInfo() { Name = "EU Server", Count = -1 });
        }

        internal void Refresh(List<ServerInfo> serversInfo)
        {
            ServersInfo.Clear();

            foreach(var server in serversInfo)
            {
                ServersInfo.Add(server);
            }
        }

        internal void ClearList()
        {
            ServersInfo.Clear();
        }

        internal void Add(ServerInfo server)
        {
            ServersInfo.Add(server);
        }
    }
}
