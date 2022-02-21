namespace FootballManager.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using static Data.DataConstants;

    public class User
    {
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DefaultMaxLength20)]
        public string Username { get; init; }

        [Required]
        [MaxLength(UserMaxEmail)]
        public string Email { get; init; }

        [Required]
        [MaxLength(UserMaxHashedPassword)]
        public string Password { get; set; }

        public ICollection<UserPlayer> Players { get; set; } = new List<UserPlayer>();
    }
}

