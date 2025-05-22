using System.ComponentModel.DataAnnotations;

namespace KKB1App.Data.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }

        [Required]
        public string ArtistName { get; set; }

        public ICollection<Program> Programs { get; set; } = new List<Program>();

    }
}
