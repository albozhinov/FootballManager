namespace FootballManager.Services
{
    using FootballManager.Services.Contracts;
    using FootballManager.ViewModels.Users;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class ValidatorService : IValidatorService
    {
        public (bool, string) NullOrWhiteSpacesCheck(RegisterUserViewModel model)
        {
            bool isValid = true;
            string error = null;

            if (string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Username) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                model.Email.Contains(' ') ||
                model.Username.Contains(' ') ||
                model.Password.Contains(' '))
            {
                error = "Please set correct value.";
                isValid = false;

                return (isValid, error);
            }

            return (isValid, error);
        }

        public (bool isValid, string errors) PasswordAndConfirmPassCheck(string password, string confirmPassword)
        {
            var isValid = true;
            string error = null;

            if (password != confirmPassword)
            {
                isValid = false;
                error = "Password and confirm password must be eqeual.";

                return (isValid, error);
            }

            return (isValid, error);
        }

        public (bool, string) ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var errorResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, context, errorResult, true);

            if (isValid)
            {
                return (isValid, null);
            }

            string error = String.Join(", ", errorResult.Select(e => e.ErrorMessage));

            return (isValid, error);
        }
    }
}
