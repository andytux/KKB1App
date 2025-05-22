using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace KKB1App.Data.Models
{
    public class Show
    {
        [Key]
        public int ShowId { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int ProgramId { get; set; }

        [ForeignKey(nameof(ProgramId))]
        public Program Program { get; set; }

        [Required]
        public decimal TicketPrice { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
