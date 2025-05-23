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

        /// <summary>
        /// Speichert oder bearbeitet ein programm in der Db
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        public async Task AddOrEditProgramAsync(Data.Models.Program program)
        {
            if (program != null)
            {
                if (program.ProgramId == 0)
                {
                    dbContext.Programs.Add(program);
                }
                else
                {
                    var programToEdit = await GetProgramByIdAsync(program.ProgramId);

                    if (programToEdit != null)
                    {
                        programToEdit.ArtistId = program.ArtistId;
                        programToEdit.Title = program.Title;
                        programToEdit.Description = program.Description;
                        programToEdit.EndDate = program.EndDate;
                        programToEdit.StartDate = program.StartDate;
                        programToEdit.Fee = program.Fee;
                        programToEdit.PaymentMode = program.PaymentMode;

                        dbContext.Programs.Update(programToEdit);
                    }
                }
                await dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Ruft alle programme aus der db ab includiert auch die Künstler
        /// </summary>
        /// <returns>List<Program></returns>
        public async Task<List<Data.Models.Program>> GetAllProgramsAsync()
        {
            return await dbContext.Programs
                .Include(p => p.Artist)
                .ToListAsync();
        }

        /// <summary>
        /// Ruft ein programm per id aus der db ab
        /// </summary>
        /// <param name="programId"></param>
        /// <returns>program oder null</returns>
        public async Task<Data.Models.Program?> GetProgramByIdAsync(int programId)
        {
            return await dbContext.Programs
                .Include(p => p.Artist)
                .FirstOrDefaultAsync(p => p.ProgramId == programId);
        }

        /// <summary>
        /// Entfernt ein programm aus der datenbank
        /// </summary>
        /// <param name="programId"></param>
        /// <returns>true erfolgreich, false-nicht erfolgreich</returns>
        public async Task<bool> RemoveProgramByIdAsync(int programId)
        {
            var programToRemove = await GetProgramByIdAsync(programId);

            if (programToRemove != null)
            {
                dbContext.Programs.Remove(programToRemove);
                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }


    }
}
