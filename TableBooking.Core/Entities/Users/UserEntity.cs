using System;

namespace Core.Entities.Users
{
    public class UserEntity
    {
        public string PasswordHash { get; set; }
        public int Salt { get; set; }
        public int Id { get; set; }

        public string Username { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserEntity entity &&
                   PasswordHash == entity.PasswordHash &&
                   Salt == entity.Salt &&
                   Username == entity.Username;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PasswordHash, Salt, Username);
        }
    }
}