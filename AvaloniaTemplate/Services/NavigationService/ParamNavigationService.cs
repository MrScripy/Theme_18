using System;
using AvaloniaTemplate.Stores;
using AvaloniaTemplate.ViewModels;

namespace AvaloniaTemplate.Services.NavigationService
{
    public class ParamNavigationService<TParam, TNavigationStore, TViewModel> : IParamNavigationService<TParam>
        where TParam : class
        where TNavigationStore : NavigationStore
        where TViewModel : ViewModelBase
    {
        private readonly TNavigationStore _navigationStore;
        private readonly Func<TParam, TViewModel> _createViewModel;

        public ParamNavigationService(TNavigationStore navigationStore, Func<TParam, TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate(TParam param)
        {
            _navigationStore.CurrentViewModel = _createViewModel(param);
        }
    }
}
