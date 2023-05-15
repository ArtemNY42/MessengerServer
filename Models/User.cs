using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerServer.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        // Define the relationship with the Chats (Many-to-Many)
        public ICollection<Chat>? Chats { get; set; }

        // Add additional properties as needed
    }
}