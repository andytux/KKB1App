using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static KKB1App.Data.Enums;

namespace KKB1App.Data.Models
{
    public class Program
    {
        [Key]
        public int ProgramId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Fee { get; set; }
        public PaymentMode PaymentMode { get; set; }

        [Required]
        public int ArtistId { get; set; }

        [ForeignKey(nameof(ArtistId))]
        public Artist Artist { get; set; }

        public ICollection<Show> Shows { get; set; } = new List<Show>();

    }
}
