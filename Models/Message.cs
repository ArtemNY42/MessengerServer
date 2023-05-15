using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessengerServer.Models
{
   public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime SentAt { get; set; }

        // Define the relationship with the User
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }

        // Define the relationship with the Chat
        [Required]
        public int ChatId { get; set; }
        public Chat? Chat { get; set; }
    }
}