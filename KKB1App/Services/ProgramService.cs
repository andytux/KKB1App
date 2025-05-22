using KKB1App.Data;
using Microsoft.EntityFrameworkCore;

namespace KKB1App.Services
{
    public class ProgramService
    {
        private readonly AppDbContext dbContext;

        public ProgramService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddProgramAsync (Data.Models.Program program)
        {
            if (program != null)
            {
                dbContext.Programs.Add(program);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Data.Models.Program>> GetAllProgramsAsync()
        {
            return await dbContext.Programs
                .Include(p=> p.Artist)
                .ToListAsync();
        }


    }
}
