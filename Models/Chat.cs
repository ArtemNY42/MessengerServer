using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MessengerServer.Models
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }

        [Required]
        public string Name { get; set; }

        // Define the relationship with the participants (Users)
        public ICollection<User>? Participants { get; set; }
    }
}