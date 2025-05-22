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

        public async Task<User?> GetUserByNameAsync(string userName)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
        
        public async Task AddUserAsync(User user)
        {
            if(user != null)
            {
                dbContext.Users.Add(user);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> UserAlreadyExistsAsync(string userName)
        {
            return await dbContext.Users.AnyAsync(u => u.UserName == userName);
        }
    }
}
