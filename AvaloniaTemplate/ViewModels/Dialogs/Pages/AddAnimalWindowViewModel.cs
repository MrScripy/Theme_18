using AvaloniaTemplate.Models;
using AvaloniaTemplate.Stores.Db;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AvaloniaTemplate.ViewModels.Dialogs.Pages
{
    public partial class AddAnimalWindowViewModel : DialogViewModelBase<Animal>
    {
        private readonly IRepository<AnimalType> _animalTypeRepository;
        public AnimalType[] AnimalTypes => _animalTypeRepository.Items.ToArray();

        private Animal _animal;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddAnimalCommand))]
        private string _name;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddAnimalCommand))]
        private string _latName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddAnimalCommand))]
        private AnimalType _selectedAnimalType;

        [RelayCommand(CanExecute = nameof(CanAddAnimal))]
        private void AddAnimal()
        {
            if (SelectedAnimalType == null) return;

            if (SelectedAnimalType.Name == "Amphibians")
                _animal = new Amphibian();
            else if (SelectedAnimalType.Name == "Birds")
                _animal = new Bird();
            else if (SelectedAnimalType.Name == "Mammals")
                _animal = new Mammal();

            _animal.Name = Name;
            _animal.LatName = LatName;
            _animal.AnimalType = _animalTypeRepository.Get(SelectedAnimalType.Id);

            Close(_animal);
        }

        private bool CanAddAnimal() =>
            !string.IsNullOrEmpty(Name)
            && !string.IsNullOrEmpty(LatName)
            && SelectedAnimalType != null;


        public AddAnimalWindowViewModel(IRepository<AnimalType> animalTypeRepository)
        {
            _animalTypeRepository = animalTypeRepository;
        }
    }
}
