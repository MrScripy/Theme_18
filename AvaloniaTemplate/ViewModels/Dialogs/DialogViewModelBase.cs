using System;
using AvaloniaTemplate.Args;

namespace AvaloniaTemplate.ViewModels.Dialogs
{
    public class DialogViewModelBase<TResult> : ViewModelBase
    {
        public event EventHandler<DialogResultEventArgs<TResult>> CloseRequested;

        protected void Close() => Close(default);

        protected void Close(TResult result)
        {
            var args = new DialogResultEventArgs<TResult>(result);

            CloseRequested?.Invoke(this, args);
        }
    }

}
