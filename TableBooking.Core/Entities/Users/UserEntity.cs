namespace Core.Entities.Users
{
    public abstract class UserEntity
    {
        public string PasswordHash { get; set; }
        public int Salt { get; set; }
        public int Id { get; set; }

        public string Username { get; set; }
    }
}