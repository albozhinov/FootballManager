namespace FootballManager.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class RegisterUserViewModel
    {
        [Required]
        [StringLength(DefaultMaxLength20, MinimumLength = UserMinUsername, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(UserMaxEmail, ErrorMessage = "{0} must be less than {1} characters.")]
        [RegularExpression(UserEmailRegularExpression, ErrorMessage = "Please write correct email address.")]
        public string Email { get; set; }

        [Required]
        [StringLength(DefaultMaxLength20, MinimumLength = UserMinPassword, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Password { get; set; }

        [Required]
        [StringLength(DefaultMaxLength20, MinimumLength = UserMinPassword, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string ConfirmPassword { get; set; }
    }
}
