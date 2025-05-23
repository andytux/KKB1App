using KKB1App.Data;
using KKB1App.Data.Models;
using KKB1App.Data.ViewModels;
using Microsoft.EntityFrameworkCore;
using static KKB1App.Data.Enums;

namespace KKB1App.Services
{
    public class StatisticService
    {
        private readonly AppDbContext dbContext;

        public StatisticService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Ruft alle programstatistics aus der datenbank
        /// </summary>
        /// <returns></returns>
        public async Task<List<ProgramStatisticsVM>> GetProgramStatisticsAsync()
        {
            var grouped = await dbContext.Shows
                .Include(s => s.Program).ThenInclude(p => p.Artist)
                .Include(s => s.Tickets)
                .ToListAsync();

            return grouped
                .GroupBy(s => new
                {
                    ProgramId = s.ProgramId,
                    ProgramTitle = s.Program.Title,
                    ArtistName = s.Program.Artist.ArtistName
                })
                .Select(g => new ProgramStatisticsVM
                {
                    ProgramTitle = g.Key.ProgramTitle,
                    ArtistName = g.Key.ArtistName,
                    TotalShows = g.Count(),
                    TotalTicketsSold = g.SelectMany(s => s.Tickets).Count(),
                    TotalRevenue = g.SelectMany(s => s.Tickets)
                                    .Sum(t => CalculateTicketPrice(t)),
                    FirstShowDate = g.Min(s => s.DateStartTime),
                    LastShowDate = g.Max(s => s.DateStartTime),
                })
                .OrderByDescending(x => x.TotalTicketsSold)
                .ToList();
        }

        /// <summary>
        /// Rechnet den Rabatt des preises falls ein discount ausgewählt wurde
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private decimal CalculateTicketPrice(Ticket t)
        {
            var price = t.Show.TicketPrice;
            var finalPrice = t.Discount switch
            {
                TicketDiscount.Student => price * 0.5m,
                TicketDiscount.Pupil => price * 0.5m,
                TicketDiscount.Senior => price * 0.5m,
                _ => price
            };

            return Math.Round(finalPrice, 2);
        }
    }
}
