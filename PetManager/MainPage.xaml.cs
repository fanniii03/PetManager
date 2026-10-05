using CommunityToolkit.Mvvm.Messaging;

namespace PetManager
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel vm;

        public MainPage()
        {
            InitializeComponent();

            vm = new MainPageViewModel();
            BindingContext = vm;

            WeakReferenceMessenger.Default
                .Register<string>(this, async (recipient, message) =>
                {
                    await DisplayAlert("Warning", message, "OK");
                });
        }
    }
}
