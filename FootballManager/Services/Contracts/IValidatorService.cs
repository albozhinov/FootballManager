namespace FootballManager.Services.Contracts
{
    using FootballManager.ViewModels.Users;

    public interface IValidatorService
    {
        public (bool isValid, string errors) NullOrWhiteSpacesCheck(RegisterUserViewModel model);

        public (bool isValid, string errors) ValidateModel(object model);

        public (bool isValid, string errors) PasswordAndConfirmPassCheck(string password, string confirmPassword);

    }
}
