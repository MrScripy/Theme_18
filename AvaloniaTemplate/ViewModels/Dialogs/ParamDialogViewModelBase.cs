using System.Threading;
using System.Threading.Tasks;

namespace AvaloniaTemplate.ViewModels.Dialogs
{
    public abstract class ParamDialogViewModelBase<TResult, TParam> : DialogViewModelBase<TResult>
    {
        public abstract void Activate(TParam param);
        public abstract Task ActivateAsync(TParam param, CancellationToken token = default);
    }
}
