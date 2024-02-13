namespace AvaloniaTemplate.Services.NavigationService;

public interface IParamNavigationService<TParam>
{
    void Navigate(TParam param);
}

