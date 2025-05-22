using System.ComponentModel.DataAnnotations;

namespace KKB1App.Data.Models
{
    public class TicketHolder
    {
        [Key]
        public int TicketHolderId { get; set; }

        [Required]
        public string TicketHolderName { get; set; }

        [Required]
        public string Address { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
