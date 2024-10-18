using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ogani.BLL.ViewModels.AppUserViewModels;

namespace Ogani.BLL.Services.Contracts;

public interface IAccountService
{
    Task<bool> RegisterAsync(RegisterViewModel vm,ModelStateDictionary modelState);
    Task<bool> LoginAsync(LoginViewModel vm,ModelStateDictionary modelState);
    Task<bool> SignOutAsync();
    

}
