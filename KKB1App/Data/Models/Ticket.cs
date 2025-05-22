using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static KKB1App.Data.Enums;

namespace KKB1App.Data.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        public int ShowId { get; set; }

        [ForeignKey(nameof(ShowId))]
        public Show Show { get; set; }

        [Required]
        public string Row { get; set; }

        [Required]
        public int SeatNumber { get; set; }

        [Required]
        public TicketDiscount Discount { get; set; }

        [Required]
        public int TicketHolderId { get; set; }

        [ForeignKey(nameof(TicketHolderId))]
        public TicketHolder TicketHolder { get; set; }


    }
}
