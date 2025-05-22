using KKB1App.Data;
using KKB1App.Data.Models;
using KKB1App.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KKB1App.Services
{
    public class ShowService
    {
        private readonly AppDbContext dbContext;

        public ShowService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddShowAsync(Show show)
        {
            bool dateTaken = await dbContext.Shows.AnyAsync(s=> s.DateTime == show.DateTime);

            if (!dateTaken)
            {
                dbContext.Shows.Add(show);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<ShowVM>> GetAllShowsAsync()
        {
            return await dbContext.Shows
                .Include(s => s.Program)
                .Select(s => new ShowVM
                {
                    ShowId = s.ShowId,
                    ProgramId = s.ProgramId,
                    ProgramTitle = s.Program.Title,
                    DateTime = s.DateTime,
                    TicketPrice = s.TicketPrice,
                    IsActive = s.IsActive
                })
                .ToListAsync();
        }
    }
}
