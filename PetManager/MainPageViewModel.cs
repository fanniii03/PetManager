//using Android.Media.TV;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManager
{
    [QueryProperty(nameof(EditedPet), "EditedPet")]
    public partial class MainPageViewModel : ObservableObject
    {
        IPetDatabase database;
        public ObservableCollection<Pet> Pets { get; set; }


        [ObservableProperty]
        Pet editedPet;

        private Pet selectedPet;
        public Pet SelectedPet
        {
            get { return selectedPet; }
            set
            {
                selectedPet = value;
                OnPropertyChanged();
                DeletePetCommand.NotifyCanExecuteChanged();
                EditSelectedPetCommand.NotifyCanExecuteChanged();
                ShowPetDetailsCommand.NotifyCanExecuteChanged();
            }
        }

        public bool CanEditOrDelete => SelectedPet != null;
        public bool IsFullViewEnabled
        {
            get
            {
                return Preferences.Default.Get("fullview", true);
            }
            set
            {
                Preferences.Default.Set("fullview",value);
                OnPropertyChanged();
            }
        }
        public MainPageViewModel(IPetDatabase database)
        {
            Pets = new ObservableCollection<Pet>();
            this.database = database;
        }
        public MainPageViewModel() : this(Ioc.Default.GetRequiredService<IPetDatabase>())
        {}
        public async Task InitializeAsync()
        {
            var petList = await database.GetPetsAsync();
            Pets.Clear();
            petList.ForEach(p => Pets.Add(p));
        }

        async partial void OnEditedPetChanged(Pet value)
        {
            if (value != null)
            {
                if (SelectedPet != null)
                {
                    Pets.Remove(SelectedPet);
                    SelectedPet = null;
                    await database.UpdatePetAsync(value);
                }
                else
                {
                    await database.CreatePetAsync(value);
                }
                Pets.Add(value);
            }
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
        public async Task ShowPetDetailsAsync()
        {
            if (SelectedPet != null)
            {
                var parameter = new ShellNavigationQueryParameters
                {
                    {"Potato",SelectedPet}
                };
                await Shell.Current.GoToAsync("petdetails", parameter);
            }
        }

        [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
        public async Task EditSelectedPet()
        {
            if (SelectedPet != null)
            {
                var parameter = new ShellNavigationQueryParameters
                {
                    {"Pet",SelectedPet }
                };
                await Shell.Current.GoToAsync("editpet", parameter);
            }
            else
            {
                WeakReferenceMessenger.Default
                    .Send("Select a pet to edit.");
            }
        }




        [RelayCommand(CanExecute = nameof(CanEditOrDelete))]
        public void DeletePet()
        {
            if (SelectedPet != null)
            {
                database.DeletePetAsync(SelectedPet);
                Pets.Remove(SelectedPet);
                SelectedPet = null;
            }
            else
            {
                WeakReferenceMessenger.Default
                    .Send("Select a pet a delete.");
            }
        }

        [RelayCommand]
        public async Task NewPet()
        {
            SelectedPet = null;
            var parameter = new ShellNavigationQueryParameters
            {
                {"Pet",new Pet(){BirthDate=DateTime.Today} }
            };
            await Shell.Current.GoToAsync("editpet", parameter);
        }
    }
}
