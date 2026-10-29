using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        
        [NotMapped]
        [JsonIgnore]
        public virtual Author? Author { get; set; }
    }
}
