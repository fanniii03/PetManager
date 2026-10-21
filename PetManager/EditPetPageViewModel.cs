using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

//using Kotlin.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManager
{
    [QueryProperty(nameof(OriginalPet),"Pet")]
    public partial class EditPetPageViewModel : ObservableObject
    {
        [ObservableProperty]
        Pet originalPet;
        [ObservableProperty]
        Pet editedPet;

        public void Init()
        {
            EditedPet = OriginalPet.GetCopy();
        }

        [RelayCommand]
        public async Task SavePetAsync()
        {
            var parameter = new ShellNavigationQueryParameters
            {
                {"EditedPet",EditedPet }
            };
            await Shell.Current.GoToAsync("..",parameter);
        }

        [RelayCommand]
        public async Task CancleEditAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
