using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessengerServer.Models;
using Microsoft.EntityFrameworkCore;

namespace MessengerServer
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        
    }
}