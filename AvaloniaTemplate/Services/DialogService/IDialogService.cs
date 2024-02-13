using System.Threading.Tasks;

namespace AvaloniaTemplate.Services.DialogService
{
    public interface IDialogService
    {
        Task<TResult> ShowDialogAsync<TResult>(string viewModelName);

        Task ShowDialogAsync(string viewModelName);

        Task ShowDialogAsync<TParam>(string viewModelName, TParam param);

        Task<TResult> ShowDialogAsync<TResult, TParam>(string viewModelName, TParam param);
    }
}
