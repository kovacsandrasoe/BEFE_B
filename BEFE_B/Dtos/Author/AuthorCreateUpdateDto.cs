using System.ComponentModel.DataAnnotations;

namespace BEFE_B.Dtos.Author
{
    public class AuthorCreateUpdateDto
    {
        [MinLength(5)]
        public string Name { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}
