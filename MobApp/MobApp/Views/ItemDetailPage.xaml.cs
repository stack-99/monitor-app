using MobApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MobApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}