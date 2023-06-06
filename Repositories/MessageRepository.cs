using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerServer.Models;

namespace MessengerServer.Repositories
{
    public class MessageRepository
    {
        private readonly AppDbContext _dbContext;

        public MessageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await _dbContext.Messages.ToListAsync();
        }

        public async Task<Message> GetMessageByIdAsync(Guid id)
        {
            return await _dbContext.Messages.FindAsync(id);
        }

        public async Task<List<Message>> GetMessagesByUserIdAsync(Guid userId)
        {
            return await _dbContext.Messages.Where(m => m.Id == userId).ToListAsync();
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            _dbContext.Messages.Add(message);
            await _dbContext.SaveChangesAsync();
            return message;
        }

        public async Task UpdateMessageAsync(Message message)
        {
            _dbContext.Entry(message).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMessageAsync(Guid id)
        {
            var message = await _dbContext.Messages.FindAsync(id);
            if (message != null)
            {
                _dbContext.Messages.Remove(message);
                await _dbContext.SaveChangesAsync();
            }
        }

        public bool MessageExists(Guid id)
        {
            return _dbContext.Messages.Any(m => m.Id == id);
        }
    }
}