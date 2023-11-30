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
    public class ItemsViewModel : BaseViewModel
    {
        private ServerInfo _selectedItem;

        public ObservableCollection<ServerInfo> ServersInfo { get; set; }

        public Command LoadItemsCommand { get; }
        //public Command AddItemCommand { get; }
        public Command<ServerInfo> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Server Status";
            ServersInfo = new ObservableCollection<ServerInfo>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<ServerInfo>(OnItemSelected);

           // AddItemCommand = new Command(OnAddItem);
        }

        public async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                ServersInfo.Clear();
                var items = await ServersDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    ServersInfo.Add(item);
                }
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

        internal void Refresh(List<ServerInfo> serversInfo)
        {
            ServersInfo.Clear();
            foreach (var server in serversInfo)
            {
                ServersInfo.Add(server);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public ServerInfo SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(ServerInfo serverInfo)
        {
            if (serverInfo == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={serverInfo.Count}");
        }
    }
}