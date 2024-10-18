using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ogani.BLL.Exceptions;
using Ogani.BLL.Services.Contracts;
using Ogani.BLL.ViewModels.AppUserViewModels;
using Ogani.DAL.DataContext.Entities;

namespace Ogani.BLL.Services;

public class AccountManager : IAccountService
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AccountManager(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> LoginAsync(LoginViewModel vm, ModelStateDictionary modelState)
    {
        if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
            throw new InvalidInputException("User already signed");


        if (!modelState.IsValid)
            return false;
        var user = await _userManager.FindByEmailAsync(vm.EmailOrUserName);

        if (user is null)
            user = await _userManager.FindByNameAsync(vm.EmailOrUserName);

        if (user is null)
        {
            modelState.AddModelError("", "Email ve y apassword yanlisdir");
            return false;
        }

        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.SaveMe, true);
        if (!result.Succeeded)
        {
            modelState.AddModelError("", "Email ve y apassword yanlisdir");
            return false;
        }

        return true;

    }

    public async Task<bool> RegisterAsync(RegisterViewModel vm, ModelStateDictionary modelState)
    {
        if (_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? true)
            throw new InvalidInputException("User already signed");


        if (!modelState.IsValid)
            return false;
        var user = _mapper.Map<AppUser>(vm);
        var result = await _userManager.CreateAsync(user, vm.Password);
        if (!result.Succeeded)
        {
            modelState.AddModelError("", string.Join(", ", result.Errors.Select(x => x.Description)));
            return false;
        }
        return true;

    }

    public async Task<bool> SignOutAsync()
    {
        if (!_httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated ?? false)
            return false;

        await _signInManager.SignOutAsync();
        return true;
    }
}
