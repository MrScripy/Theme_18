using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTemplate.ViewModels.Dialogs.Pages
{
    public partial class FirstDialogWindowViewModel : DialogViewModelBase<bool>
    {
        [ObservableProperty]
        private string _text = "some text";

        [RelayCommand]
        public void Confirm()
        {
            Close(true);
        }

        [RelayCommand]
        public void Reject()
        {
            Close(false);
        }
    }
}
