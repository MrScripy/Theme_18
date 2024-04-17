using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaTemplate.Stores;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaTemplate.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase    
    {
        [ObservableProperty]
        private string _windowTitle = "Animals";
        
        private NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainWindowViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += NavigationStore_CurrentViewModelChanged;
        }

        private void NavigationStore_CurrentViewModelChanged() => OnPropertyChanged(nameof(CurrentViewModel));

        public override void Dispose()
        {
            _navigationStore.CurrentViewModelChanged -= NavigationStore_CurrentViewModelChanged;
            base.Dispose();
        }
    }
}
