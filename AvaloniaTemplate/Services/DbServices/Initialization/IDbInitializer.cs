using System.Threading.Tasks;

namespace AvaloniaTemplate.Services.DbServices.Initialization
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}
