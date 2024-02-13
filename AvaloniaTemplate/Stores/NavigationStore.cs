using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaTemplate.ViewModels;

namespace AvaloniaTemplate.Stores
{
    public class NavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                OnCurrentViewModelChanging();
            }
        }

        public event Action CurrentViewModelChanged;

        private void OnCurrentViewModelChanging()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }

}
