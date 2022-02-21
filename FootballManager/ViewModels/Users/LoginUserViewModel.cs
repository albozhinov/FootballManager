namespace FootballManager.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class LoginUserViewModel
    {
        [Required]
        [StringLength(DefaultMaxLength20, MinimumLength = UserMinUsername, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(DefaultMaxLength20, MinimumLength = UserMinPassword, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Password { get; set; }
    }
}
