namespace FootballManager.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;

    public class Player
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(PlayerMaxFullName)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(PlayerMaxImageUrl)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(PlayerMaxPosition)]
        public string Position { get; set; }

        [Required]
        [Range(PlayerMinSpeed, PlayerMaxSpeed)]
        public byte Speed { get; set; }

        [Required]
        [Range(PlayerMinEndurance, PlayerMaxEndurance)]
        public byte Endurance { get; set; }

        [Required]
        [MaxLength(PlayerMaxDescription)]
        public string Description { get; set; }

        public ICollection<UserPlayer> Users { get; set; } = new List<UserPlayer>();
    }
}
