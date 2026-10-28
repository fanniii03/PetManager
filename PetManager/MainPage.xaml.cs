using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Tasks;

namespace PetManager
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel vm;

        public MainPage()
        {
            InitializeComponent();
            //this.vm = vm;
            //BindingContext = vm;

            this.vm = this.BindingContext as MainPageViewModel ?? new MainPageViewModel();

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
