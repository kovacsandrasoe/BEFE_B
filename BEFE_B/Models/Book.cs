using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BEFE_B.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(250)]
        public string Title { get; set; } = string.Empty;

        public int Year { get; set; }

        public Guid AuthorId { get; set; }
    }
}
