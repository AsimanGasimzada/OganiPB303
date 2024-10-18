using Ogani.BLL.UI.ViewModel;

namespace Ogani.BLL.UI.Services.Contracts;

public interface IHomeService
{
    Task<HomeViewModel> GetHomeViewModelAsync();
}
