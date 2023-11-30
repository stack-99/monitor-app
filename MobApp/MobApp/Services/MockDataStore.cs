using Newtonsoft.Json;
using MobApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobApp.Services
{
    public class MockDataStore : IDataStore<ServerInfo>
    {
        private const string Uri = "";

        readonly List<ServerInfo> items;

        public MockDataStore()
        {
            items = new List<ServerInfo>()
            {
                new ServerInfo { AssignedID = "EUHost/-1", Name = "Mock Data", Count = -1}
                //new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
            };
        }

        public async Task<bool> AddItemAsync(ServerInfo item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ServerInfo item)
        {
            var oldItem = items.Where((ServerInfo arg) => arg.AssignedID == item.AssignedID).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Not handling deletes for servers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            return await Task.FromResult(true);
        }

        public async Task<ServerInfo> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.AssignedID == id));
        }

        public async Task<IEnumerable<ServerInfo>> GetItemsAsync(bool forceRefresh = false)
        {
            if(forceRefresh)
            {
                var res = await RestAPICore.GetInstance().SendGetJsonAsync(Uri + "serversInfo");

                if (res.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    DependencyService.Get<IMessage>().ShortAlert("[RestartGS] Unable to get response");
                    return items;

                }

                var jsonRes = JsonConvert.DeserializeObject<ServerStatusResult>(await res.Content.ReadAsStringAsync());

                if (jsonRes.Success)
                {
                    items.Clear();
                    foreach (var item in jsonRes.ServersInfo)
                    {
                        await AddItemAsync(item);
                    }
                }
                else
                {
                    // show toast

                    DependencyService.Get<IMessage>().LongAlert("[ServerInfo] Unable to get servers status");
                }
            }

            return await Task.FromResult(items);
        }
    }
}