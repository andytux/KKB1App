namespace KKB1App.Data.ViewModels
{
    public class ShowVM
    {
        public int ShowId { get; set; }

        public int ProgramId { get; set; }
        public DateTime DateStartTime { get; set; }
        public DateTime DateEndTime { get; set; }

        public decimal TicketPrice { get; set; }

        public bool IsActive { get; set; }

        public string ProgramTitle { get; set; }
    }
}
