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
            bool dateTaken = await dbContext.Shows
                .AnyAsync(s =>
                    s.DateStartTime < show.DateEndTime &&
                    s.DateEndTime > show.DateStartTime
                );

            if (!dateTaken)
            {
                dbContext.Shows.Add(show);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        //public async Task<List<ShowVM>> GetAllShowsAsync()
        //{
        //    return await dbContext.Shows
        //        .Include(s => s.Program)
        //        .Select(s => new ShowVM
        //        {
        //            ShowId = s.ShowId,
        //            ProgramId = s.ProgramId,
        //            ProgramTitle = s.Program.Title,
        //            DateStartTime = s.DateStartTime,
        //            DateEndTime = s.DateEndTime,
        //            TicketPrice = s.TicketPrice,
        //            IsActive = s.IsActive
        //        })
        //        .ToListAsync();
        //}

        public async Task<List<ShowVM>> GetAllShowsAsync()
        {
            var now = DateTime.Now;

            var shows = await dbContext.Shows
                .Include(s => s.Program)
                .ToListAsync();

            bool updated = false;

            foreach (var show in shows)
            {
                //updated isactiv zu false, falls der die show schon in der vergangenheit liegt
                if (show.DateEndTime < now && show.IsActive)
                {
                    show.IsActive = false;
                    updated = true;
                }
            }

            if (updated)
            {
                await dbContext.SaveChangesAsync();
            }

            return shows.Select(s => new ShowVM
            {
                ShowId = s.ShowId,
                ProgramId = s.ProgramId,
                ProgramTitle = s.Program.Title,
                DateStartTime = s.DateStartTime,
                DateEndTime = s.DateEndTime,
                TicketPrice = s.TicketPrice,
                IsActive = s.IsActive
            }).ToList();
        }

    }
}
