using KKB1App.Data;
using KKB1App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KKB1App.Services
{
    public class UserService
    {
        private readonly AppDbContext dbContext;

        public UserService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Ruft einen user per namen aus der Datenbank ab
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User oder null</returns>
        public async Task<User?> GetUserByNameAsync(string userName)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
        
        /// <summary>
        /// Erstellt einen neuen benutzer in der db
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task AddUserAsync(User user)
        {
            if(user != null)
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Überprüft ob ein User mit diesem usernamen in der datenbank vorhanden ist
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>bool</returns>
        public async Task<bool> UserAlreadyExistsAsync(string userName)
        {
            return await dbContext.Users.AnyAsync(u => u.UserName == userName);
        }
    }
}
