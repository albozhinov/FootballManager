namespace FootballManager.Services
{
    using FootballManager.Data.Common;
    using FootballManager.Data.Models;
    using FootballManager.Services.Contracts;
    using FootballManager.ViewModels.Users;
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using static Data.DataConstants;
    public class UserService : IUserService
    {
        private readonly IValidatorService validatorService;
        private readonly IRepository repo;


        public UserService(IRepository _repository, IValidatorService _validatorService)
        {
            this.repo = _repository;
            this.validatorService = _validatorService;
        }

        public (bool isRegister, string errors, string id) Login(LoginUserViewModel model)
        {
            bool isLoged = true;
            string errorResult = null;
            string userId = null;

            (isLoged, errorResult) = validatorService.ValidateModel(model);

            if (!isLoged)
            {

                return (isLoged, errorResult, userId);
            }

             userId = repo.All<User>()
                            .Where(u => u.Username == model.Username.ToLower() &&
                            u.Password == CalculateHash(model.Password))
                            .Select(u => u.Id)
                            .FirstOrDefault();

            if (userId == null)
            {
                isLoged = false;
                errorResult = $"User with username {model.Username} is not registered.";

                return (isLoged, errorResult, userId);
            }


            return (isLoged, errorResult, userId);
        }

        public (bool isRegister, string errors) Register(RegisterUserViewModel model)
        {
            bool isValid = true;
            string errors = null;

            (isValid, errors) = validatorService.ValidateModel(model);
            (isValid, errors) = validatorService.NullOrWhiteSpacesCheck(model);
            (isValid, errors) = validatorService.PasswordAndConfirmPassCheck(model.Password, model.ConfirmPassword);

            if (!isValid)
            {
                return (isValid, errors);
            }

            var isUserOrEmailExists = repo.All<User>()
                                            .Where(u => u.Email == model.Email.ToLower() ||
                                                u.Username == model.Username.ToLower())
                                            .FirstOrDefault();

            if (isUserOrEmailExists != null)
            {
                isValid = false;
                errors = "User with this username or email already exists.";

                return (isValid, errors);
            }

            var user = new User()
            {
                Username = model.Username.ToLower(),
                Email = model.Email.ToLower(),
                Password = CalculateHash(model.Password),
            };


            try
            {
                repo.Add<User>(user);
                repo.SaveChanges();
                isValid = true;
            }
            catch (Exception)
            {
                errors = "Sorry but could not register user in database.";
            }

            return (isValid, errors);
        }

        private string CalculateHash(string password)
        {
            byte[] passworArray = Encoding.UTF8.GetBytes(password);

            using (SHA256 sha256 = SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(passworArray));
            }
        }
    }
}
