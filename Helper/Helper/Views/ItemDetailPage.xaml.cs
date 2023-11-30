using Helper.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Helper.Views
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