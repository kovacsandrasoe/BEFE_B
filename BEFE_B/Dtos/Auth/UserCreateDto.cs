namespace BEFE_B.Dtos.Auth
{
    public class UserCreateDto
    {
        public required string Email { get; set; } = "";

        public required string Password { get; set; } = "";

        public required string FamilyName { get; set; } = "";

        public required string GivenName { get; set; } = "";
    }
}
