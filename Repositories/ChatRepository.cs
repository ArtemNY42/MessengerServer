using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerServer.Models;

namespace MessengerServer.Repositories
{
    public class ChatRepository
    {
        private readonly AppDbContext _dbContext;

        public ChatRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Chat>> GetChatsAsync()
        {
            return await _dbContext.Chats.Include(c => c.Participants).ToListAsync();
        }

        public async Task<Chat> GetChatByIdAsync(Guid id)
        {
            return await _dbContext.Chats.Include(c => c.Participants).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Chat> CreateChatAsync(Chat chat)
        {
            _dbContext.Chats.Add(chat);
            await _dbContext.SaveChangesAsync();
            return chat;
        }

        public async Task UpdateChatAsync(Chat chat)
        {
            _dbContext.Entry(chat).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteChatAsync(Guid id)
        {
            var chat = await _dbContext.Chats.FindAsync(id);
            if (chat != null)
            {
                _dbContext.Chats.Remove(chat);
                await _dbContext.SaveChangesAsync();
            }
        }

        public bool ChatExists(Guid id)
        {
            return _dbContext.Chats.Any(c => c.Id == id);
        }
    }
}