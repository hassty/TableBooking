namespace Core.Dto.Users
{
    public class UserDto
    {
        public int Id { get; set; }

        public string PasswordHash { get; set; }

        public int Salt { get; set; }
        public string Username { get; set; }
    }
}