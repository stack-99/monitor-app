using Newtonsoft.Json;
using MobApp.Models;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobApp.Views
{
    public partial class AboutPage : ContentPage
    {
        private const string Uri = "";

        public AboutPage()
        {
            InitializeComponent();
        }

        private async void restartEUGSServer(object sender, EventArgs e)
        {
            var res = await RestAPICore.GetInstance().SendGetJsonAsync(Uri + "restartGS");

            if (res.StatusCode != System.Net.HttpStatusCode.OK)
            {
                DependencyService.Get<IMessage>().LongAlert("[RestartGS] Unable to get response");
                return;

            }

            var jsonRes = JsonConvert.DeserializeObject<ServerStatusResult>(await res.Content.ReadAsStringAsync());

            if (jsonRes.Success)
            {
                DependencyService.Get<IMessage>().ShortAlert("Restarted EU Game server");
                // serversListView.ItemsSource = ;
            }
            else
            {
                // show toast
                DependencyService.Get<IMessage>().LongAlert("[RestartGS] Failed to restart: " + jsonRes.Success);
            }
        }

        private async void restartLSServer(object sender, EventArgs e)
        {
            var res = await RestAPICore.GetInstance().SendGetJsonAsync(Uri + "restartLS");

            if (res.StatusCode != System.Net.HttpStatusCode.OK)
            {
                // Show toast
                DependencyService.Get<IMessage>().LongAlert("[RestartLS] Unable to get response");
                return;

            }

            var jsonRes = JsonConvert.DeserializeObject<ServerStatusResult>(await res.Content.ReadAsStringAsync());

            if (jsonRes.Success)
            {
                DependencyService.Get<IMessage>().ShortAlert("Restarted EU Login server");
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("[RestartLS] Failed to restart: " + jsonRes.Success);
            }
        }
    }
}