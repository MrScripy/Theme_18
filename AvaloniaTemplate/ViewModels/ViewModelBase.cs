using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaTemplate.ViewModels;

public class ViewModelBase : ObservableObject, IDisposable
{
    public virtual void Dispose()
    {
     
    }
}
