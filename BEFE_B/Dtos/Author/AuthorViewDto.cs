namespace BEFE_B.Dtos.Author
{
    public class AuthorViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public List<string> TitleOfBooks { get; set; } = [];

    }
}
