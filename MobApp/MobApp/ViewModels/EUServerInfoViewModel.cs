using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using MobApp.Models;
using MobApp.Views;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MobApp.ViewModels
{
    public class EUServerInfoViewModel : BaseViewModel
    {
        private const string Uri = "";

        public ServerHardwareInfo ServerHardwareInfo { get; set; }

        public Command LoadItemsCommand { get; }
        //public Command AddItemCommand { get; }
        public Command<ServerInfo> ItemTapped { get; }

        public EUServerInfoViewModel()
        {
            Title = "EU Hardware Info";
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ServerHardwareInfo = new ServerHardwareInfo { };
        }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var res = await RestAPICore.GetInstance().SendGetJsonAsync(Uri + "processes");

                if (res.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    DependencyService.Get<IMessage>().ShortAlert("[RefreshServerInfo] Unable to get response");
                    return;

                }

                ServerHardwareInfo = JsonConvert.DeserializeObject<ServerHardwareInfo>(await res.Content.ReadAsStringAsync());

                OnPropertyChanged("ServerHardwareInfo");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}