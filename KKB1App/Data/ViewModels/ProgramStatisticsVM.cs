using System.ComponentModel.DataAnnotations.Schema;

namespace KKB1App.Data.ViewModels
{
    public class ProgramStatisticsVM
    {
        public string ProgramTitle { get; set; }
        public string ArtistName { get; set; }

        public int TotalShows { get; set; }
        public int TotalTicketsSold { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalRevenue { get; set; }

        public DateTime? FirstShowDate { get; set; }
        public DateTime? LastShowDate { get; set; }
    }
}
