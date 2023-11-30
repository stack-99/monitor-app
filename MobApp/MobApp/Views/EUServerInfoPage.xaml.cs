using Newtonsoft.Json;
using MobApp.Models;
using MobApp.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobApp.Views
{
    /// <summary>
    /// Loads memory/space resources
    /// </summary>
    public partial class EUServerInfoPage : ContentPage
    {
        EUServerInfoViewModel _viewModel;

        public EUServerInfoPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new EUServerInfoViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           _viewModel.OnAppearing();
        }

        private async void refreshEUHardwareInfo(object sender, EventArgs e)
        {
            await _viewModel.ExecuteLoadItemsCommand();
        }
    }
}