using KKB1App.Data;
using KKB1App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KKB1App.Services
{
    public class TicketHolderService(AppDbContext dbContext)
    {
        private readonly AppDbContext dbContext = dbContext;


        public async Task<List<TicketHolder>> GetForShowAsync(int showId)
        {
            return await dbContext.TicketHolders
                .Where(h=> h.Tickets.Any(t=>
                    t.ShowId == showId))
                .Include(h=> h.Tickets)
                .ToListAsync();
        }
    }
}

