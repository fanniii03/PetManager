using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetManager
{
    public partial class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<Pet> Pets { get; set; }

        [ObservableProperty]
        Pet selectedPet;

        [ObservableProperty]
        Pet editedPet;

        public MainPageViewModel()
        {
            Pets = new ObservableCollection<Pet>();
            Pets.Add(new Pet() { Name = "Samu", Species = "Cat", BirthDate = new DateTime(2021, 9, 28) });
            Pets.Add(new Pet() { Name = "Zoé", Species = "Francia bulldog palota pincsi", BirthDate = new DateTime(2014, 12, 14) });
        }

        public void EditSelectedPet()
        {
            EditedPet = SelectedPet.GetCopy();
        }

        public void SavePet()
        {
            if (SelectedPet != null)
            {
                Pets.Remove(SelectedPet);
            }
            Pets.Add(EditedPet);
            EditedPet = null;
        }

        public void CancelEdit()
        {
            EditedPet = null;
        }

        public void DeletePet()
        {
            Pets.Remove(SelectedPet);
            SelectedPet = null;
        }

        public void NewPet()
        {
            SelectedPet = null;
            EditedPet = new Pet()
            {
                Name = "New Name",
                BirthDate = DateTime.Today
            };
        }
    }
}
