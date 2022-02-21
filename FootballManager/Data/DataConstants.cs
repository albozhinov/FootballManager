namespace FootballManager.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DataConstants
    {
        //User
        public const int IdMaxLength = 40;
        public const int DefaultMaxLength20 = 20;

        public const int UserMinUsername = 5;
        public const int UserMinPassword = 5;
        public const int UserMaxHashedPassword = 64;
        public const int UserMinEmail = 10;
        public const int UserMaxEmail = 60;
        public const string UserEmailRegularExpression = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        //Player        
        public const int PlayerMinFullName = 5;
        public const int PlayerMaxFullName = 80;
        public const int PlayerMaxImageUrl = 300;
        public const int PlayerMinPosition = 5;
        public const int PlayerMaxPosition = 20;
        public const byte PlayerMinSpeed = 0;
        public const byte PlayerMaxSpeed = 10;
        public const byte PlayerMinEndurance = 0;
        public const byte PlayerMaxEndurance = 10;
        public const int PlayerMaxDescription = 200;
    }
}


