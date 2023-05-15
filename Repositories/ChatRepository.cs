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

        public async Task<Chat> GetChatByIdAsync(int id)
        {
            return await _dbContext.Chats.Include(c => c.Participants).FirstOrDefaultAsync(c => c.ChatId == id);
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

        public async Task DeleteChatAsync(int id)
        {
            var chat = await _dbContext.Chats.FindAsync(id);
            if (chat != null)
            {
                _dbContext.Chats.Remove(chat);
                await _dbContext.SaveChangesAsync();
            }
        }

        public bool ChatExists(int id)
        {
            return _dbContext.Chats.Any(c => c.ChatId == id);
        }
    }
}