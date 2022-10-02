namespace Authentification.Services.Dtos
{
    public class UserDto
    {
        public long Id { get; internal set; }

        public string FirstName { get; internal set; }

        public string LastName { get; internal set; }

        public string UserName { get; internal set; }
    }
}