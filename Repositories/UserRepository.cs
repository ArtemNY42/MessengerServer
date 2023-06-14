using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerServer.Models;

namespace MessengerServer.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            if(user.RegistrationDate == DateTime.MinValue) user.RegistrationDate = DateTime.Now;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {

            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public bool UserExists(Guid id)
        {
            return _dbContext.Users.Any(u => u.Id == id);
        }

        public bool UserValid(User user)
        {
            return _dbContext.Users.Any(u => user.Email == u.Email && user.Password == u.Password);
        }
    }
}