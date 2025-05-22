using KKB1App.Data;
using KKB1App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KKB1App.Services
{
    public class ArtistService
    {
        private readonly AppDbContext dbContext;

        public ArtistService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Ruft alle Künstler aus der datenbank ab
        /// </summary>
        /// <returns>List<Artist></Artist></returns>
        public async Task<List<Artist>> GetAllArtistsAsync()
        {
            return await dbContext.Artists
                .Include(p=>p.Programs)
                .ToListAsync();
        }

        public async Task AddArtistAsync(Artist artist)
        {
            if(artist != null)
            {
                dbContext.Artists.Add(artist);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
