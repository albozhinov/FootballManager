namespace FootballManager.Services.Contracts
{
    using FootballManager.ViewModels.Users;

    public interface IUserService
    {
        (bool isRegister, string errors) Register(RegisterUserViewModel model);

        (bool isRegister, string errors, string id) Login(LoginUserViewModel model);
    }
}
