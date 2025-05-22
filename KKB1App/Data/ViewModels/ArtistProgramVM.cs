using System.ComponentModel.DataAnnotations.Schema;

namespace KKB1App.Data.ViewModels
{
    public class ArtistProgramVM
    {
        public int ProgramId { get; set; }
        public string ProgramTitle { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Fee { get; set; }
        public string PaymentMode { get; set; }

        public string ArtistName { get; set; }
    }
}
