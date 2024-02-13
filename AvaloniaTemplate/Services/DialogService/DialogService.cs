using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using AvaloniaTemplate.ViewModels;
using AvaloniaTemplate.ViewModels.Dialogs;
using AvaloniaTemplate.Views;
using AvaloniaTemplate.Views.Dialogs;
using Splat;

namespace AvaloniaTemplate.Services.DialogService
{
    public class DialogService : IDialogService
    {
        private readonly MainWindow _parentWindow;

        public DialogService(MainWindow parentWindow)
        {
            _parentWindow = parentWindow;
        }

        public async Task<TResult> ShowDialogAsync<TResult>(string viewModelName)
        {
            var window = CreateView<TResult>(viewModelName);
            var viewModel = CreateViewModel<TResult>(viewModelName);
            Bind(window, viewModel);

            return await ShowDialogAsync(window);
        }

        public async Task ShowDialogAsync(string viewModelName)
            => await ShowDialogAsync<DialogResultBase>(viewModelName);

        public async Task ShowDialogAsync<TParam>(string viewModelName, TParam param)
            => await ShowDialogAsync<DialogResultBase, TParam>(viewModelName, param);

        public async Task<TResult> ShowDialogAsync<TResult, TParam>(string viewModelName, TParam param)
        {
            var window = CreateView<TResult>(viewModelName);
            var viewModel = CreateViewModel<TResult>(viewModelName);
            Bind(window, viewModel);

            switch (viewModel)
            {
                case ParamDialogViewModelBase<TResult, TParam> parameterizedDialogViewModelBase:
                    await parameterizedDialogViewModelBase.ActivateAsync(param);
                    break;
                default:
                    throw new InvalidOperationException(
                        $"{viewModel.GetType().FullName} doesn't support passing parameters!");
            }

            return await ShowDialogAsync(window);
        }

        private static void Bind(IDataContextProvider window, object viewModel) => window.DataContext = viewModel;

        private async Task<TResult> ShowDialogAsync<TResult>(DialogWindowBase<TResult> window)
        {
            var mainWindow = _parentWindow;
            var result = await window.ShowDialog<TResult>(mainWindow);

            if (window is IDisposable disposable)
            {
                disposable.Dispose();
            }

            return result;
        }

        // ViewModel static methods
        private static DialogViewModelBase<TResult> CreateViewModel<TResult>(string viewModelName)
        {
            var viewModelType = GetViewModelType(viewModelName);
            if (viewModelType == null)
                throw new InvalidOperationException($"View model {viewModelName} was not found!");

            return (DialogViewModelBase<TResult>)GetViewModel(viewModelType);
        }

        private static Type GetViewModelType(string viewModelName)
        {
            var viewModelAssembly = Assembly.GetAssembly(typeof(ViewModelBase));
            if (viewModelAssembly == null)
                throw new InvalidOperationException("Broken installation!");

            var viewModelTypes = viewModelAssembly.GetTypes();

            return viewModelTypes.SingleOrDefault(t => t.Name == viewModelName);
        }

        private static object? GetViewModel(Type type) => Locator.Current.GetService(type);

        // Views static methods
        private static DialogWindowBase<TResult>? CreateView<TResult>(string viewModelName)
        {
            Type viewType = GetViewType(viewModelName);
            if (viewType is null)
                throw new InvalidOperationException($"View for {viewModelName} was not found!");

            return (DialogWindowBase<TResult>?)GetView(viewType);
        }

        private static Type? GetViewType(string viewModelName)
        {
            Assembly viewsAssembly = Assembly.GetExecutingAssembly();
            Type[] viewTypes = viewsAssembly.GetTypes();
            var viewName = viewModelName.Replace("ViewModel", "");

            return viewTypes.SingleOrDefault(t => t.Name == viewName);
        }

        private static object? GetView(Type type) => Activator.CreateInstance(type);
    }
}
