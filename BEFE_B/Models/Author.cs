using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BEFE_B.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [StringLength(250)]
        public string Name { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        [NotMapped]
        public virtual ICollection<Book>? Books { get; set; }
    }
}
