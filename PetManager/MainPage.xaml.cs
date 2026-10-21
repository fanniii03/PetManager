using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Tasks;

namespace PetManager
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel vm;

        public MainPage(MainPageViewModel vm)
        {
            InitializeComponent();
            this.vm = vm;
            BindingContext = vm;

            WeakReferenceMessenger.Default
                .Register<string>(this, async (recipient, message) =>
                {
                    await DisplayAlert("Warning", message, "OK");
                });
        }

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            await vm.InitializeAsync();
        }
    }
}
