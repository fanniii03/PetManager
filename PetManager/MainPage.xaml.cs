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
        }

        private async void ButtonEdit_OnClicked(object? sender, EventArgs e)
        {
            if (vm.SelectedPet == null)
            {
                await DisplayAlert("Warning", "Select a Pet first!", "OK");
                return;
            }
            ChangeAreas();
            vm.EditSelectedPet();
        }

        private void ButtonSave_OnClicked(object? sender, EventArgs e)
        {
            vm.SavePet();
            ChangeAreas();
        }

        private void ButtonCancel_OnClicked(object? sender, EventArgs e)
        {
            vm.CancelEdit();
            ChangeAreas();
        }

        private async void ButtonDelete_OnClicked(object? sender, EventArgs e)
        {
            if (vm.SelectedPet == null)
            {
                await DisplayAlert("Warning", "Select a Pet first!", "OK");
                return;
            }
            vm.DeletePet();
        }

        private void ButtonNew_OnClicked(object? sender, EventArgs e)
        {
            ChangeAreas();
            vm.NewPet();
        }

        private void ChangeAreas()
        {
            editArea.IsEnabled = !editArea.IsEnabled;
            buttonArea.IsEnabled = !buttonArea.IsEnabled;
            petsCollectionView.IsEnabled = !petsCollectionView.IsEnabled;
            petsCollectionView.InputTransparent = !petsCollectionView.InputTransparent;
        }
    }
}
