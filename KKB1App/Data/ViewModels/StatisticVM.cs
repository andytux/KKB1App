namespace KKB1App.Data.ViewModels
{
    public class StatisticVM
    {
        public string ProgramTitle { get; set; }

        public string ArtistName { get; set; }

        public List<ShowInfoVM> Shows { get; set; } = new List<ShowInfoVM>();
    }
}
