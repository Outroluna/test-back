using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using test_back.Data;
using test_back.Models;

namespace test_back.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext Context;

        public UserService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await Context.Users.FindAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            Context.Users.Add(user);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            Context.Entry(user).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user != null)
            {
                Context.Users.Remove(user);
                await Context.SaveChangesAsync();
            }
        }
    }
}
