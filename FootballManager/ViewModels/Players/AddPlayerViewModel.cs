namespace FootballManager.ViewModels.Players
{
    using System.ComponentModel.DataAnnotations;
    using static Data.DataConstants;

    public class AddPlayerViewModel
    {
        [Required]
        [StringLength(DefaultMaxLength20, MinimumLength = UserMinUsername, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [StringLength(PlayerMaxPosition, MinimumLength = PlayerMinPosition, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [MaxLength(PlayerMaxPosition)]
        public string Position { get; set; }

        [Required]
        [Range(PlayerMinSpeed, PlayerMaxSpeed)]
        public byte Speed { get; set; }

        [Required]
        [Range(PlayerMinEndurance, PlayerMaxEndurance)]
        public byte Endurance { get; set; }

        [Required]
        [MaxLength(PlayerMaxDescription, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Description { get; set; }

        public string UserId { get; set; }
    }
}
