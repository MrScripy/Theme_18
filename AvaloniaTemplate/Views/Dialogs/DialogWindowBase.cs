using System;
using Avalonia.Controls;
using AvaloniaTemplate.Args;
using AvaloniaTemplate.ViewModels.Dialogs;

namespace AvaloniaTemplate.Views.Dialogs
{
    public class DialogWindowBase<TResult> : Window
    {
        private Window _parentWindow => (Window)Owner;

        protected DialogViewModelBase<TResult>? ViewModel => (DialogViewModelBase<TResult>?)DataContext;

        protected DialogWindowBase()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            CanResize = false;
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object? sender, EventArgs ev)
        {
            if (ViewModel != null)
                ViewModel.CloseRequested += ViewModelOnCloseRequested;
        }

        private void ViewModelOnCloseRequested(object? sender, DialogResultEventArgs<TResult> ev)
        {
            if (ViewModel != null)
                ViewModel.CloseRequested -= ViewModelOnCloseRequested;
            DataContextChanged -= OnDataContextChanged;

            Close(ev.Result);
        }
    }

    public class DialogResultBase : DialogWindowBase<bool>
    {

    }
}
