namespace Ogani.BLL.ViewModels.AppUserViewModels;

public class LoginViewModel
{
    public string EmailOrUserName { get; set; }
    public string Password { get; set; }
    public bool SaveMe { get; set; } = false;
}
