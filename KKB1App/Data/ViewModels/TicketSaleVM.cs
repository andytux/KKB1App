using System.ComponentModel.DataAnnotations;
using static KKB1App.Data.Enums;

namespace KKB1App.Data.ViewModels
{
    public class TicketSaleVM
    {
        public int ShowId { get; set; }

        [Required]
        public string Row {  get; set; }

        [Required]
        public int SeatNumber { get; set; }

        public TicketDiscount Discount { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
