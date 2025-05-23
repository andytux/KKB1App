using KKB1App.Data;
using KKB1App.Data.Models;
using KKB1App.Data.ViewModels;
using Microsoft.AspNetCore.Components;
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

        /// <summary>
        /// Fügt ein programm in der datenbank hinzu oder editiert ein vorhandenes
        /// </summary>
        /// <param name="show"></param>
        /// <returns>bool</returns>
        public async Task<bool> AddOrEditShowAsync(Show show)
        {
            if (show.ShowId == 0)
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
            }
            else
            {
                var showToUpdate = await GetShowByIdAsync(show.ShowId);

                if(showToUpdate != null)
                {
                    showToUpdate.ProgramId = show.ProgramId;
                    showToUpdate.TicketPrice = show.TicketPrice;
                    showToUpdate.DateStartTime = show.DateStartTime;
                    showToUpdate.DateEndTime = show.DateEndTime;
                    showToUpdate.IsActive = show.IsActive;

                    dbContext.Shows.Update(showToUpdate);
                    await dbContext.SaveChangesAsync();

                    return true;
                    
                }

                return false;
            }

                return false;
        }

        /// <summary>
        /// Ruft alle shows aus der datenbank ab
        /// </summary>
        /// <returns>List<ShowVM></returns>
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

        /// <summary>
        /// Ruft alle shows aus der datenbank ab und setzt in vergangene shows auf inaktiv
        /// </summary>
        /// <returns>List<ShowVM></returns>
        public async Task<List<ShowVM>> GetAllShowsAsync()
        {
            var now = DateTime.Now;

            var shows = await dbContext.Shows
                .Include(s => s.Program)
                .ToListAsync();

            bool updated = false;

            foreach (var show in shows)
            {
                //setzt isactiv zu false, falls der die show schon in der vergangenheit liegt
                if (show.DateEndTime < now && show.IsActive)
                {
                    show.IsActive = false;
                    updated = true;
                }
            }
            
            //speichert die änderungen in der db
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

        /// <summary>
        /// Löscht eine show per id aus der datenbank
        /// </summary>
        /// <param name="showId"></param>
        /// <returns>bool</returns>
        public async Task<bool> RemoveShowAsync(int showId)
        {
            var showToRemove = await GetShowByIdAsync(showId);

            if (showToRemove != null)
            {
                dbContext.Shows.Remove(showToRemove);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;

        }

        /// <summary>
        /// Ruft eine show per id aus der datenbank ab (includiert auch Program und Artist)
        /// </summary>
        /// <param name="showId"></param>
        /// <returns>Show oder null</returns>
        public async Task<Show?> GetShowByIdAsync(int showId)
        {
            return await dbContext.Shows
                .Include(s => s.Program).ThenInclude(p => p.Artist)
                .FirstOrDefaultAsync(s => s.ShowId == showId);
        }

    }
}
